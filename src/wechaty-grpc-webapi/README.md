

## 接口测试

### Contact
- [x] [GET] `/api/Contact/ContactAlia` 目前Padlocal协议返回的数据是空，不知道其协议是否能返回数据
- [x] [PUT] `/api/Contact/ContactAlias`
- [x] [GET] `/api/Contact/ContactAvatar`
- [ ] [PUT] `/api/Contact/ContactAvatar`
- [x] [GET] `/api/Contact/ContactList`
- [x] [PUT] `/api/Contact/ContactSelfName`
- [x] [GET] `/api/Contact/ContactSelfQRCode`
- [x] [PUT] `/api/Contact/ContactSelfSignature`


### FriendShip
- [ ] [PUT] `/api/FriendShip/FriendshipAccept`
- [ ] [PUT] `/api/FriendShip/FriendshipAdd`
- [ ] [GET] `/api/FriendShip/FriendshipSearchPhone`
- [ ] [GET] `/api/FriendShip/FriendshipSearchWeixin`

### Message
- [ ] [GET] `​/api​/Message​/MessageContact` --> *` ERR PuppetServiceImpl grpcError() messageContact() rejection: not implement`*
- [ ] [GET] `/api/Message/MessageFile` *rpc.Core.RpcException: Status(StatusCode="Internal", Detail="[tid:85a66eef] [353ms] download file failed，需要特殊处理下*
- [ ] [GET] `/api/Message/MessageImage`
- [x] [GET] `/api/Message/MessageImageStream`
- [x] [GET] `/api/Message/MessageMiniProgram`
- [ ] [put] `/api/Message/MessageRecall`  
    *__异常：`request has been cancelled for reason: SERVER_ERROR: 2 UNKNOWN: [tid:70f1ff83] wechat bad request error`<strong>__*
- [x] [PUT] `/api/Message/MessageSendContact`
- [ ] [PUT] `/api/Message/MessageSendFile`
- [x] [PUT] `/api/Message/MessageSendMiniProgram`
- [x] [PUT] `/api/Message/MessageSendText`
- [x] [PUT] `/api/Message/MessageSendUrl`
- [x] [PUT] `/api/Message/MessageUrl`

### Room
- [x] [PUT] `/api/Room/GetRoomPayload`
- [x] [PUT] `/api/Room/RoomAdd`
- [x] [GET] `/api/Room/RoomAnnounce`
- [x] [PUT] `/api/Room/RoomAnnounce` 如果当前账号不是群主或者群管理员的话，修改公告会报错`request has been cancelled for reason: SERVER_ERROR: 2`
- [x] [GET] `/api/Room/RoomAvatar`
- [ ] [POST] `/api/Room/RoomCreate` --> *`ERR PuppetServiceImpl grpcError() roomCreate() rejection: roomId is required`*
- [x] [DELETE] `/api/Room/RoomDel` 当前用户作为管理员，不能移除其他管理员，只能移除普通成员
- [ ] [PUT] `/api/Room/RoomInvitationAccept`
- [x] [GET] `/api/Room/RoomList`
- [x] [GET] `/api/Room/RoomMemberList`
- [ ] [GET] `/api/Room/RoomQRCode` --> `ERR PuppetServiceImpl grpcError() roomQRCode() rejection: bufferToQrcode(buf) fail!`
- [x] [PUT] `/api/Room/RoomQuit`
- [ ] [GET] `/api/Room/RoomTopic` 这个其实也是更新操作，暂时先停用该接口
- [x] [PUT] `/api/Room/RoomTopic`

### Tag
- [x] [POST] `/api/Tag/TagContactAdd`
- [x] [DELETE] `/api/Tag/TagContactDelete/{tagId}`
- [x] [GET] `/api/Tag/TagContactList/{contactId}`
- [x] [GET] `/api/Tag/TagContactList`
- [x] [PUT] `/api/Tag/TagContactRemove`