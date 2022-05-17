using System;
using System.Threading.Tasks;
using github.wechaty.grpc.puppet;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static Wechaty.Puppet;

namespace Wechaty.Grpc.Client
{
    public partial class WechatyPuppetClient
    {
        private PuppetClient _grpcClient;
        private readonly ILogger _logger;

        internal GrpcPuppetOption _option;
        public WechatyPuppetClient(GrpcPuppetOption option, ILogger logger)
        {
            _option = option;
            _logger = logger;
        }


        public async Task StartAsync()
        {
            _grpcClient = DiscoverPupptClient.InitGrpcClient(_option);
            await _grpcClient.StartAsync(new StartRequest());
        }

        public async Task StopAsync() => await _grpcClient.StopAsync(new StopRequest());


        /// <summary>
        /// 双向数据流事件处理
        /// </summary>
        public AsyncServerStreamingCall<EventResponse> EventStreamAsync()
        {
            try
            {
                var eventStream = _grpcClient.Event(new EventRequest());
                return eventStream;

                //while (await eventStream.ResponseStream.MoveNext())
                //{
                //    //OnGrpcStreamEvent(eventStream.ResponseStream.Current);
                //    return eventStream.ResponseStream.Current;
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="event"></param>
        ///// <returns></returns>
        //protected void OnGrpcStreamEvent(EventResponse @event)
        //{
        //    try
        //    {
        //        var eventType = @event.Type;
        //        var payload = @event.Payload;


        //        if (eventType != EventType.Heartbeat)
        //        {
        //            var eventHeartbeatPayload = new EventHeartbeatPayload()
        //            {
        //                Data = $"onGrpcStreamEvent({eventType})"
        //            };
        //            //await _localEventBus.PublishAsync(eventHeartbeatPayload);
        //            Emit(eventHeartbeatPayload);
        //        }

        //        switch (eventType)
        //        {
        //            case EventType.Unspecified:
        //                Logger.LogError("onGrpcStreamEvent() got an EventType.EVENT_TYPE_UNSPECIFIED ?");
        //                break;
        //            case EventType.Heartbeat:
        //                Emit(JsonConvert.DeserializeObject<EventHeartbeatPayload>(payload));
        //                break;
        //            case EventType.Message:
        //                Emit(JsonConvert.DeserializeObject<EventMessagePayload>(payload));
        //                break;
        //            case EventType.Dong:
        //                Emit(JsonConvert.DeserializeObject<EventDongPayload>(payload));
        //                break;
        //            case EventType.Error:
        //                Emit(JsonConvert.DeserializeObject<EventErrorPayload>(payload));
        //                break;
        //            case EventType.Friendship:
        //                Emit(JsonConvert.DeserializeObject<EventFriendshipPayload>(payload));
        //                break;
        //            case EventType.RoomInvite:
        //                Emit(JsonConvert.DeserializeObject<EventRoomInvitePayload>(payload));
        //                break;
        //            case EventType.RoomJoin:
        //                Emit(JsonConvert.DeserializeObject<EventRoomJoinPayload>(payload));
        //                break;
        //            case EventType.RoomLeave:
        //                Emit(JsonConvert.DeserializeObject<EventRoomLeavePayload>(payload));
        //                break;
        //            case EventType.RoomTopic:
        //                Emit(JsonConvert.DeserializeObject<EventRoomTopicPayload>(payload));
        //                break;
        //            case EventType.Scan:
        //                Emit(JsonConvert.DeserializeObject<EventScanPayload>(payload));
        //                break;
        //            case EventType.Ready:
        //                Emit(JsonConvert.DeserializeObject<EventReadyPayload>(payload));
        //                break;
        //            case EventType.Reset:
        //                //log.warn('PuppetHostie', 'onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?')
        //                // the `reset` event should be dealed not send out
        //                Emit(JsonConvert.DeserializeObject<EventResetPayload>(payload));
        //                Logger.LogWarning("onGrpcStreamEvent() got an EventType.EVENT_TYPE_RESET ?");
        //                break;
        //            case EventType.Login:
        //                var loginPayload = JsonConvert.DeserializeObject<EventLoginPayload>(payload);
        //                SelfId = loginPayload.ContactId;
        //                break;
        //            case EventType.Logout:
        //                SelfId = string.Empty;
        //                Emit(JsonConvert.DeserializeObject<EventLogoutPayload>(payload));
        //                break;
        //            default:
        //                logger.LogWarning($"'eventType {eventType} unsupported! (code should not reach here)");

        //                //throw new BusinessException($"'eventType {_event.Type.ToString()} unsupported! (code should not reach here)");
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex, "OnGrpcStreamEvent exception");
        //    }

        //}




    }
}
