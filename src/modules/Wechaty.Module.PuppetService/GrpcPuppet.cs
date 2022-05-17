using System;
using System.Threading;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wechaty.Grpc.Client;
using Wechaty.Module.Puppet;
using Wechaty.Module.Schemas;

namespace Wechaty.Module.PuppetService
{
    public partial class GrpcPuppet : WechatyPuppet
    {
        protected PuppetOptions options { get; }
        protected ILogger<WechatyPuppet> logger { get; }

        protected WechatyPuppetClient _grpcClient;

        public GrpcPuppet(PuppetOptions _options, ILogger<WechatyPuppet> _logger, ILoggerFactory loggerFactory)
            : base(_options, _logger, loggerFactory)
        {
            options = _options;
            options.Name = options.Name ?? "Default";

            logger = _logger;

            _grpcClient = new WechatyPuppetClient(new GrpcPuppetOption()
            {
                ENDPOINT = _options.Endpoint,
                Token = _options.Token,
                Name = _options.Name,
            }, _logger);
        }


        #region GRPC 连接

        /// <summary>
        /// 关闭Grpc连接
        /// </summary>
        /// <returns></returns>
        protected async Task StopGrpcClient() => await _grpcClient.StopAsync();

        /// <summary>
        /// 双向数据流事件处理
        /// </summary>
        protected async Task StartGrpcStreamAsync()
        {
            try
            {
                // var eventStream = _grpcClient.Event(new EventRequest());
                var eventStream =  _grpcClient.EventStreamAsync();

                while (await eventStream.ResponseStream.MoveNext())
                {
                    OnGrpcStreamEvent(eventStream.ResponseStream.Current);
                }
            }
            catch (Exception ex)
            {
                //_grpcClient.Stop(st, call);
                await _grpcClient.StopAsync();

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


        protected int GRPCReconnectionCount = 3;
        public override async Task StartGrpc()
        {
            try
            {
                if (options.Token == "")
                {
                    throw new ArgumentException("wechaty-puppet-hostie: token not found. See: <https://github.com/wechaty/wechaty-puppet-hostie#1-wechaty_puppet_hostie_token>");
                }

                await _grpcClient.StartAsync();
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
