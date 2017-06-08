using System;
#if UNITY
using UnityEngine;
using MonoPInvokeCallbackAttribute = AOT.MonoPInvokeCallbackAttribute;
#else
using MonoPInvokeCallbackAttribute = NIM.MonoPInvokeCallbackAttribute;
#endif


namespace NIMChatRoom
{
    //如果错误码为kResRoomLocalNeedRequestAgain，聊天室重连机制结束，则需要向IM服务器重新请求进入该聊天室权限
    public delegate void ChatRoomLoginDelegate(NIMChatRoomLoginStep loginStep, NIM.ResponseCode errorCode, ChatRoomInfo roomInfo, MemberInfo memberInfo);

    public delegate void ExitChatRoomDelegate(long roomId, NIM.ResponseCode errorCode, NIMChatRoomExitReason reason);

    public delegate void QueryMembersResultDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo[] members);

    public delegate void QueryMessageHistoryResultDelegate(long roomId, NIM.ResponseCode errorCode, Message[] messages);

    public delegate void SetMemberPropertyDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo info);

    public delegate void CloseRoomDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void RemoveMemberDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void GetRoomInfoDelegate(long roomId, NIM.ResponseCode errorCode, ChatRoomInfo info);

    public delegate void TempMuteMemberDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo info);

    public delegate void ChatRoomQueueListDelegate(long room_id, NIM.ResponseCode error_code, string result, string json_extension, IntPtr user_data);

    public delegate void ChatRoomQueueDropDelegate(long room_id, NIM.ResponseCode error_code);

    public delegate void ChatRoomQueuePollDelegate(long room_id, int error_code, string result, string json_extension, IntPtr user_data);

    public delegate void ChatRoomQueueOfferDelegate(long room_id, int error_code, string json_extension, IntPtr user_data);

    internal static class CallbackBridge
    {
        public static readonly NimChatroomGetMembersCbFunc QueryMembersCallback = OnQueryMembersCompleted;

        [MonoPInvokeCallback(typeof(NimChatroomGetMembersCbFunc))]
        private static void OnQueryMembersCompleted(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            MemberInfo[] members = null;
            if (errorCode == (int)NIM.ResponseCode.kNIMResSuccess)
            {
                members = NimUtility.Json.JsonParser.Deserialize<MemberInfo[]>(result);
            }
            NimUtility.DelegateConverter.InvokeOnce<QueryMembersResultDelegate>(userData, roomId, (NIM.ResponseCode)errorCode, members);
        }

        public static readonly NimChatroomGetMsgCbFunc QueryMessageLogCallback = OnQueryMessageLogCompleted;

        [MonoPInvokeCallback(typeof(NimChatroomGetMsgCbFunc))]
        private static void OnQueryMessageLogCompleted(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            Message[] messages = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
            {
                messages = NimUtility.Json.JsonParser.Deserialize<Message[]>(result);
            }
            NimUtility.DelegateConverter.InvokeOnce<QueryMessageHistoryResultDelegate>(userData, roomId, code, messages);
        }

        public static readonly NimChatroomSetMemberAttributeCbFunc SetMemberPropertyCallback = OnSetMemberProperty;

        [MonoPInvokeCallback(typeof(NimChatroomSetMemberAttributeCbFunc))]
        private static void OnSetMemberProperty(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            MemberInfo mi = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
                mi = NimUtility.Json.JsonParser.Deserialize<MemberInfo>(result);
            NimUtility.DelegateConverter.InvokeOnce<SetMemberPropertyDelegate>(userData, roomId, code, mi);
        }

        public static readonly NimChatroomCloseCbFunc RoomClosedCallback = OnChatRoomClosed;

        [MonoPInvokeCallback(typeof(NimChatroomCloseCbFunc))]
        private static void OnChatRoomClosed(long roomId, int errorCode, string jsonExtension, IntPtr userData)
        {
            NimUtility.DelegateConverter.InvokeOnce<CloseRoomDelegate>(userData, roomId, (NIM.ResponseCode)errorCode);
        }

        public static readonly NimChatroomGetInfoCbFunc GetRoomInfoCallback = OnGetRoomInfo;

        [MonoPInvokeCallback(typeof(NimChatroomGetInfoCbFunc))]
        private static void OnGetRoomInfo(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            ChatRoomInfo roomInfo = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
                roomInfo = NimUtility.Json.JsonParser.Deserialize<ChatRoomInfo>(result);
            NimUtility.DelegateConverter.InvokeOnce<GetRoomInfoDelegate>(userData, roomId, code, roomInfo);
        }

        public static readonly NimChatroomKickMemberCbFunc KickoutMemberCallback = OnKickoutOthers;

        [MonoPInvokeCallback(typeof(NimChatroomKickMemberCbFunc))]
        private static void OnKickoutOthers(long roomId, int errorCode, string jsonExtension, IntPtr userData)
        {
            NimUtility.DelegateConverter.InvokeOnce<RemoveMemberDelegate>(userData, roomId, (NIM.ResponseCode)errorCode);
        }

        public static readonly nim_chatroom_temp_mute_member_cb_func TempMuteMemberCallback = OnTemporaryMuteMember;

        [MonoPInvokeCallback(typeof(nim_chatroom_temp_mute_member_cb_func))]
        private static void OnTemporaryMuteMember(long roomId, int resCode, string result, string jsonExt, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var info = MemberInfo.Deserialize(result);
                NimUtility.DelegateConverter.InvokeOnce<TempMuteMemberDelegate>(userData, roomId, (NIM.ResponseCode)resCode, info);
            }
        }


        public static readonly nim_chatroom_queue_list_cb_func ChatroomQueueListCallback = OnQueryMICList;

        [MonoPInvokeCallback(typeof(nim_chatroom_queue_list_cb_func))]
        private static void OnQueryMICList(long room_id, NIM.ResponseCode error_code, string result, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueListDelegate>(user_data, room_id, error_code, result, json_extension);
            }
        }

        public static readonly nim_chatroom_queue_drop_cb_func ChatroomQueueDropCallback = OnDropMICQueue;

        [MonoPInvokeCallback(typeof(nim_chatroom_queue_drop_cb_func))]
        private static void OnDropMICQueue(long room_id, NIM.ResponseCode error_code, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueDropDelegate>(user_data, room_id, error_code, json_extension);
            }
        }

        public static readonly nim_chatroom_queue_poll_cb_func ChatroomQueuePollCallback = OnPopMICQueue;

        [MonoPInvokeCallback(typeof(nim_chatroom_queue_poll_cb_func))]
        private static void OnPopMICQueue(long room_id, NIM.ResponseCode error_code, string result, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueuePollDelegate>(user_data, room_id, error_code, result, json_extension);
            }
        }

        public static readonly nim_chatroom_queue_offer_cb_func ChatroomQueueOfferCallback = OnQueueOffer;

        [MonoPInvokeCallback(typeof(nim_chatroom_queue_offer_cb_func))]
        private static void OnQueueOffer(long room_id, NIM.ResponseCode error_code, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueOfferDelegate>(user_data, room_id, error_code, json_extension);
            }
        }
    }
}
