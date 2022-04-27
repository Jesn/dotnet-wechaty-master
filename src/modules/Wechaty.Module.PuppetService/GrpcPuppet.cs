using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wechaty.GateWay;
using Wechaty.Grpc.PuppetService;
using Wechaty.Grpc.PuppetService.Contact;
using Wechaty.Grpc.PuppetService.FriendShip;
using Wechaty.Grpc.PuppetService.Message;
using Wechaty.Grpc.PuppetService.Room;
using Wechaty.Grpc.PuppetService.Tag;
using Wechaty.Module.Puppet;
using Wechaty.Module.Puppet.Schemas;
using static Wechaty.Puppet;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet : WechatyPuppet
    {

        public PuppetOptions options { get; }
        protected ILogger<WechatyPuppet> logger { get; }

        protected IGateWayService _gateWayService;
        protected IWechatyPuppetService _wechatyPuppetService;
        protected IContactService _contactService;
        protected IFriendShipService _friendShipService;
        protected IMessageService _messageService;
        protected IRoomService _roomService;
        protected ITagService _tagService;


        protected ContainerBuilder builder = new ContainerBuilder();
        protected IContainer container;


        public GrpcPuppet(PuppetOptions _options, ILogger<WechatyPuppet> _logger, ILoggerFactory loggerFactory)
            : base(_options, _logger, loggerFactory)
        {
            options = _options;
            logger = _logger;


            //grpcPuppetService = new GrpcPuppetService(grpcClient);

            //var builder = new ContainerBuilder();
            builder.RegisterType<GateWayService>().As<IGateWayService>().SingleInstance();
            builder.RegisterType<WechatyPuppetService>().As<IWechatyPuppetService>();
            builder.RegisterType<ContactService>().As<IContactService>();
            builder.RegisterType<FriendShipService>().As<IFriendShipService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<RoomService>().As<IRoomService>();
            builder.RegisterType<TagService>().As<ITagService>();

            container = builder.Build();
            //var resovler= new AutofacDependencyResolver(container);



            _gateWayService = container.Resolve<IGateWayService>();
            //_wechatyPuppetService = container.Resolve<IWechatyPuppetService>();


        }




        #region GRPC 连接
        protected const string CHATIE_ENDPOINT = "https://api.chatie.io/v0/hosties/";
        //private PuppetClient _grpcClient = null;
        //private GrpcChannel channel = null;

        /// <summary>
        /// This channel argument controls the amount of time (in milliseconds) the sender of the keepalive ping waits for an acknowledgement.
        /// If it does not receive an acknowledgment within this time, it will close the connection.
        /// </summary>
        private readonly int _keepAliveTimeoutMs = 30000;

        /// <summary>
        /// GRPC  重连次数，超过该次数则放弃重连
        /// </summary>
        private int GRPCReconnectionCount = 3;



        /// <summary>
        /// 关闭Grpc连接
        /// </summary>
        /// <returns></returns>
        protected async Task StopGrpcClient() => await _gateWayService.StopAsync();

        /// <summary>
        /// 双向数据流事件处理
        /// </summary>
        protected async Task StartGrpcStreamAsync()
        {
            try
            {
                // var eventStream = _grpcClient.Event(new EventRequest());
                var eventStream = _wechatyPuppetService.EventStream();

                while (await eventStream.ResponseStream.MoveNext())
                {
                    OnGrpcStreamEvent(eventStream.ResponseStream.Current);
                }
            }
            catch (Exception ex)
            {

                StopRequest st = new StopRequest()
                {

                };

                CallOptions call = new CallOptions()
                {

                };

                //_grpcClient.Stop(st, call);
                await _gateWayService.StopAsync();

                var eventResetPayload = new EventResetPayload()
                {
                    Data = ex.StackTrace
                };
                Emit(eventResetPayload);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected void OnGrpcStreamEvent(EventResponse @event)
        {
            try
            {
                var eventType = @event.Type;
                var payload = @event.Payload;

                logger.LogInformation($"dateTime:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {eventType},PayLoad:{payload}");

                if (eventType != EventType.Heartbeat)
                {
                    var eventHeartbeatPayload = new EventHeartbeatPayload()
                    {
                        Data = $"onGrpcStreamEvent({eventType})"
                    };
                    //await _localEventBus.PublishAsync(eventHeartbeatPayload);
                    Emit(eventHeartbeatPayload);
                }

                switch (eventType)
                {
                    case EventType.Unspecified:
                        Logger.LogError("onGrpcStreamEvent() got an EventType.EVENT_TYPE_UNSPECIFIED ?");
                        break;
                    case EventType.Heartbeat:
                        Emit(JsonConvert.DeserializeObject<EventHeartbeatPayload>(payload));
                        break;
                    case EventType.Message:
                        Emit(JsonConvert.DeserializeObject<EventMessagePayload>(payload));
                        break;
                    case EventType.Dong:
                        Emit(JsonConvert.DeserializeObject<EventDongPayload>(payload));
                        break;
                    case EventType.Error:
                        Emit(JsonConvert.DeserializeObject<EventErrorPayload>(payload));
                        break;
                    case EventType.Friendship:
                        Emit(JsonConvert.DeserializeObject<EventFriendshipPayload>(payload));
                        break;
                    case EventType.RoomInvite:
                        Emit(JsonConvert.DeserializeObject<EventRoomInvitePayload>(payload));
                        break;
                    case EventType.RoomJoin:
                        Emit(JsonConvert.DeserializeObject<EventRoomJoinPayload>(payload));
                        break;
                    case EventType.RoomLeave:
                        Emit(JsonConvert.DeserializeObject<EventRoomLeavePayload>(payload));
                        break;
                    case EventType.RoomTopic:
                        Emit(JsonConvert.DeserializeObject<EventRoomTopicPayload>(payload));
                        break;
                    case EventType.Scan:
                        Emit(JsonConvert.DeserializeObject<EventScanPayload>(payload));
                        break;
                    case EventType.Ready:
                        Emit(JsonConvert.DeserializeObject<EventReadyPayload>(payload));
                        break;
                    case EventType.Reset:
                        //log.warn('PuppetHostie', 'onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?')
                        // the `reset` event should be dealed not send out
                        Emit(JsonConvert.DeserializeObject<EventResetPayload>(payload));
                        Logger.LogWarning("onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?");
                        break;
                    case EventType.Login:
                        var loginPayload = JsonConvert.DeserializeObject<EventLoginPayload>(payload);
                        SelfId = loginPayload.ContactId;
                        break;
                    case EventType.Logout:
                        SelfId = string.Empty;
                        Emit(JsonConvert.DeserializeObject<EventLogoutPayload>(payload));
                        break;
                    default:
                        logger.LogWarning($"'eventType {eventType} unsupported! (code should not reach here)");

                        //throw new BusinessException($"'eventType {_event.Type.ToString()} unsupported! (code should not reach here)");
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "OnGrpcStreamEvent exception");
            }

        }
        protected void StopGrpcStream()
        {
            //_localEventBus.UnsubscribeAll(typeof(LocalEventBus));
        }

        #endregion

        #region 实现抽象类
        public override WechatyPuppet ToImplement => this;

        public override async Task StartGrpc()
        {
            try
            {
                if (options.Token == "")
                {
                    throw new ArgumentException("wechaty-puppet-hostie: token not found. See: <https://github.com/wechaty/wechaty-puppet-hostie#1-wechaty_puppet_hostie_token>");
                }

                //await StartGrpcClient();

                //await _grpcClient.StartAsync(new StartRequest());

                await _gateWayService.StartAsync(options);

                _wechatyPuppetService = container.Resolve<IWechatyPuppetService>();
                _contactService = container.Resolve<IContactService>();
                _friendShipService = container.Resolve<IFriendShipService>();
                _messageService = container.Resolve<IMessageService>();
                _roomService = container.Resolve<IRoomService>();
                _tagService = container.Resolve<ITagService>();

                _ = StartGrpcStreamAsync();

               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"StartGrpcClient() exception,Grpc Retry Surplus Count {GRPCReconnectionCount}");
                if (GRPCReconnectionCount == 0)
                {
                    throw new Exception(ex.StackTrace);
                }
                GRPCReconnectionCount -= 1;
                Thread.Sleep(3000);
                await StopGrpcClient();
                Thread.Sleep(2000);
                await StartGrpc();
            }
        }


        public override void Ding(string? data)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
