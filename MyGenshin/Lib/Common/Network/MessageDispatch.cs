//WARNING: DON'T EDIT THIS FILE!!!
using Common;

namespace Network
{
    public class MessageDispatch<T> : Singleton<MessageDispatch<T>>
    {
        public void Dispatch(T sender, SkillBridge.Message.NetMessageResponse message)
        {
            if (message.userRegister != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.userRegister); }
            if (message.userLogin != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.userLogin); }
            if (message.createChar != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.createChar); }
            if (message.gameEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.gameEnter); }
            if (message.gameLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.gameLeave); }
            if (message.mapCharacterEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapCharacterEnter); }
            if (message.mapCharacterLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapCharacterLeave); }
            if (message.mapEntitySync != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapEntitySync); }
            if (message.itemBuyResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemBuyResponse); }
            if (message.itemSellResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemSellResponse); }
            if (message.statusNotify != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.statusNotify); }
            if (message.equipResponse!= null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.equipResponse); }
            if (message.questAcceptResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questAcceptResponse); }
            if (message.questListResponse!= null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questListResponse); }
            if (message.questSubmitResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questSubmitResponse); }
            if (message.messageReplyResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageReplyResponse); }
            if (message.messageListResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageListResponse); }
            if (message.messageSendResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageSendResponse); }
            if (message.messageTargetInfoResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageTargetInfoResponse); }
            if (message.friendAdd != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendAdd); }
            if (message.friendList!= null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendList); }
            if (message.friendRemove != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendRemove); }
            if (message.messageReceive != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageReceive); }
            if (message.teamInviteRequest!=null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteRequest); }
            if (message.teamInviteResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteResponse); }
            if (message.teamInfo != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInfo); }
            if (message.teamLeader != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamLeader); }
            if (message.teamLeave!= null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamLeave); }
            if (message.Update!=null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.Update); }
        }

        public void Dispatch(T sender, SkillBridge.Message.NetMessageRequest message)
        {
            if (message.userRegister != null) { MessageDistributer<T>.Instance.RaiseEvent(sender,message.userRegister); }
            if (message.userLogin != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.userLogin); }
            if (message.createChar != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.createChar); }
            if (message.gameEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.gameEnter); }
            if (message.gameLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.gameLeave); }
            if (message.mapCharacterEnter != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapCharacterEnter); }
            if (message.mapEntitySync != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapEntitySync); }
            if (message.mapTeleport != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.mapTeleport); }
            if (message.itemBuyRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemBuyRequest); }
            if (message.itemSellRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.itemSellRequest); }
            if (message.equipRequest!= null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.equipRequest); }
            if (message.questAcceptRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questAcceptRequest); }
            if (message.questListRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questListRequest); }
            if (message.questSubmitRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.questSubmitRequest); }
            if (message.messageReplyRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageReplyRequest); }
            if (message.messageListRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageListRequest); }
            if (message.messageSendRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageSendRequest); }
            if (message.messageTargetInfoRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.messageTargetInfoRequest); }
            if (message.friendList != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendList); }
            if (message.friendRemove != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.friendRemove); }
            if (message.teamInviteRequest != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteRequest); }
            if (message.teamInviteResponse != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInviteResponse); }
            if (message.teamInfo != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamInfo); }
            if (message.teamLeader != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamLeader); }
            if (message.teamLeave != null) { MessageDistributer<T>.Instance.RaiseEvent(sender, message.teamLeave); }
        }
    }
}