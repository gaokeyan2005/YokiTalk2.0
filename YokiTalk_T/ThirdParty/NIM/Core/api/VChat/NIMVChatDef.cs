/** @file NIMVChatDef.cs
  * @brief NIM VChat提供的音视频接口定义，
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @date 2015/12/8
  */
#if !UNITY
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NIM
{
    /** @enum NIMVideoChatSessionType 音视频通话状态通知类型 */
    public enum NIMVideoChatSessionType
    {
        kNIMVideoChatSessionTypeStartRes = 1,		/**< 创建通话结果 */
        kNIMVideoChatSessionTypeInviteNotify = 2,		/**< 通话邀请 */
        kNIMVideoChatSessionTypeCalleeAckRes = 3,		/**< 确认通话，接受拒绝结果 */
        kNIMVideoChatSessionTypeCalleeAckNotify = 4,		/**< 确认通话，接受拒绝通知 */
        kNIMVideoChatSessionTypeControlRes = 5,		/**< NIMVChatControlType 结果 */
        kNIMVideoChatSessionTypeControlNotify = 6,		/**< NIMVChatControlType 通知 */
        kNIMVideoChatSessionTypeConnect = 7,		/**< 通话中链接状态通知 */
        kNIMVideoChatSessionTypePeopleStatus = 8,		/**< 通话中成员状态 */
        kNIMVideoChatSessionTypeNetStatus = 9,		/**< 通话中网络状态 */
        kNIMVideoChatSessionTypeHangupRes = 10,		/**< 通话挂断结果 */
        kNIMVideoChatSessionTypeHangupNotify = 11,		/**< 通话被挂断通知 */
        kNIMVideoChatSessionTypeSyncAckNotify = 12,     /**< 通话接听挂断同步通知 */
        kNIMVideoChatSessionTypeMp4Notify = 13,     /**< 通知MP4录制状态，包括开始录制和结束录制 */
        kNIMVideoChatSessionTypeInfoNotify = 14,        /**< 通知实时音视频数据状态 */
        kNIMVideoChatSessionTypeVolumeNotify = 15,      /**< 通知实时音频发送和混音的音量状态 */
    };

    /// <summary>
    /// 音视频通话控制类型
    /// </summary>
    public enum NIMVChatControlType
    {
        /// <summary>
        /// 打开音频
        /// </summary>
        kNIMTagControlOpenAudio = 1,
        /// <summary>
        /// 关闭音频
        /// </summary>
        kNIMTagControlCloseAudio = 2,
        /// <summary>
        /// 打开视频
        /// </summary>
        kNIMTagControlOpenVideo = 3,
        /// <summary>
        /// 关闭视频
        /// </summary>
        kNIMTagControlCloseVideo = 4,
        /// <summary>
        /// 请求从音频切换到视频
        /// </summary>
        kNIMTagControlAudioToVideo = 5,
        /// <summary>
        /// 同意从音频切换到视频
        /// </summary>
        kNIMTagControlAgreeAudioToVideo = 6,
        /// <summary>
        /// 拒绝从音频切换到视频
        /// </summary>
        kNIMTagControlRejectAudioToVideo = 7,
        /// <summary>
        /// 从视频切换到音频
        /// </summary>
        kNIMTagControlVideoToAudio = 8,
        /// <summary>
        /// 占线
        /// </summary>
        kNIMTagControlBusyLine = 9,
        /// <summary>
        /// 告诉对方自己的摄像头不可用
        /// </summary>
        kNIMTagControlCamaraNotAvailable = 10,
        /// <summary>
        /// 告诉对方自已处于后台
        /// </summary>
        kNIMTagControlEnterBackground = 11,
        /// <summary>
        /// 告诉发送方自己已经收到请求了（用于通知发送方开始播放提示音）
        /// </summary>
        kNIMTagControlReceiveStartNotifyFeedback = 12,
        /// <summary>
        /// 告诉发送方自己开始录制 
        /// </summary>
        kNIMTagControlMp4StartRecord = 13,
        /// <summary>
        /// 告诉发送方自己结束录制
        /// </summary>
        kNIMTagControlMp4StopRecord = 14,
    };

    /// <summary>
    /// 音视频通话类型
    /// </summary>
    public enum NIMVideoChatMode
    {
        /// <summary>
        /// 语音通话模式
        /// </summary>
        kNIMVideoChatModeAudio = 1,
        /// <summary>
        /// 视频通话模式
        /// </summary>
        kNIMVideoChatModeVideo = 2,
    };

    /// <summary>
    /// 音视频通话成员变化类型
    /// </summary>
    public enum NIMVideoChatSessionStatus
    {
        /// <summary>
        /// 成员进入
        /// </summary>
        kNIMVideoChatSessionStatusJoined = 0,
        /// <summary>
        /// 成员退出
        /// </summary>
        kNIMVideoChatSessionStatusLeaved = 1,
    };

    /// <summary>
    /// 音视频通话网络变化类型
    /// </summary>
    public enum NIMVideoChatSessionNetStat
    {
        /// <summary>
        /// 网络状态很好
        /// </summary>
        kNIMVideoChatSessionNetStatVeryGood = 0,
        /// <summary>
        /// 网络状态较好
        /// </summary>
        kNIMVideoChatSessionNetStatGood = 1,
        /// <summary>
        /// 网络状态较差
        /// </summary>
        kNIMVideoChatSessionNetStatBad = 2,
        /// <summary>
        /// 网络状态很差
        /// </summary>
        kNIMVideoChatSessionNetStatVeryBad = 3,
    };

    /// <summary>
    /// 视频通话分辨率，最终长宽比不保证
    /// </summary>
    public enum NIMVChatVideoQuality
    {
        /// <summary>
        /// 视频默认分辨率 480x320
        /// </summary>
        kNIMVChatVideoQualityNormal = 0,
        ///<summary>
        ///视频低分辨率176x144
        /// </summary>       
        kNIMVChatVideoQualityLow = 1,
        ///<summary>
        ///视频中分辨率 352x288
        /// </summary>      
        kNIMVChatVideoQualityMedium = 2,
        ///<summary>
        ///视频高分辨率 480x320
        /// </summary>    
        kNIMVChatVideoQualityHigh = 3,
        ///<summary>
        ///视频超高分辨率 640x480
        /// </summary>       
        kNIMVChatVideoQualitySuper = 4,
        ///<summary>
        ///用于桌面分享级别的分辨率1280x720，需要使用高清摄像头并指定对应的分辨率，或者自定义通道传输 
        /// </summary>    
        kNIMVChatVideoQuality720p = 5,
    };

    /// <summary>
    /// NIMVChatVideoFrameRate 视频通话帧率，实际帧率因画面采集频率和机器性能限制可能达不到期望值
    /// </summary>
    public enum NIMVChatVideoFrameRate
    {
        /// <summary>
        /// 视频通话帧率默认值,最大取每秒15帧
        /// </summary>
        kNIMVChatVideoFrameRateNormal = 0,
        /// <summary>
        /// 视频通话帧率 最大取每秒5帧
        /// </summary>
        kNIMVChatVideoFrameRate5 = 1,
        /// <summary>
        /// 视频通话帧率 最大取每秒10帧
        /// </summary>
        kNIMVChatVideoFrameRate10 = 2,
        /// <summary>
        /// 视频通话帧率 最大取每秒15帧
        /// </summary>
        kNIMVChatVideoFrameRate15 = 3,
        /// <summary>
        /// 视频通话帧率 最大取每秒20帧
        /// </summary>
        kNIMVChatVideoFrameRate20 = 4,
        /// <summary>
        /// 视频通话帧率 最大取每秒25帧
        /// </summary>
        kNIMVChatVideoFrameRate25 = 5,
    };

    /// <summary>
    /// 音视频服务器连接状态类型
    /// </summary>
    public enum NIMVChatConnectErrorCode
    {
        /// <summary>
        /// 断开连接
        /// </summary>
        kNIMVChatConnectDisconn = 0,
        /// <summary>
        /// 启动失败
        /// </summary>
        kNIMVChatConnectStartFail = 1,
        /// <summary>
        /// 超时
        /// </summary>
        kNIMVChatConnectTimeout = 101,
        /// <summary>
        /// 会议模式错误
        /// </summary>
        kNIMVChatConnectMeetingModeError = 102,
        /// <summary>
        /// 非rtmp用户加入rtmp频道
        /// </summary>	  
        kNIMVChatConnectRtmpModeError = 103,
        /// <summary>
        /// 超过频道最多rtmp人数限制
        /// </summary>
        kNIMVChatConnectRtmpNodesError = 104,
        /// <summary>
        /// 已经存在一个主播
        /// </summary>     
        kNIMVChatConnectRtmpHostError = 105,
        /// <summary>
        /// 成功
        /// </summary>
        kNIMVChatConnectSuccess = 200,
        /// <summary>
        /// 错误参数
        /// </summary>
        kNIMVChatConnectInvalidParam = 400,
        /// <summary>
        /// 密码加密错误
        /// </summary>
        kNIMVChatConnectDesKey = 401,
        /// <summary>
        /// 错误请求
        /// </summary>
        kNIMVChatConnectInvalidRequst = 417,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        kNIMVChatConnectServerUnknown = 500,
        /// <summary>
        /// 退出
        /// </summary>
        kNIMVChatConnectLogout = 1001,
        /// <summary>
        /// 发起失败
        /// </summary>
        kNIMVChatChannelStartFail = 11000,
        /// <summary>
        /// 断开连接
        /// </summary>
        kNIMVChatChannelDisconnected = 11001,
        /// <summary>
        /// 本人SDK版本太低不兼容
        /// </summary>
        kNIMVChatVersionSelfLow = 11002,
        /// <summary>
        /// 对方SDK版本太低不兼容
        /// </summary>
        kNIMVChatVersionRemoteLow = 11003,
    };

    /// <summary>
    /// NIMVChatMp4RecordCode mp4录制状态
    /// </summary>
    public enum NIMVChatMp4RecordCode
    {
        /// <summary>
        /// MP4结束
        /// </summary>
        kNIMVChatMp4RecordClose = 0,
        /// <summary>
        /// MP4结束，视频画面大小变化
        /// </summary>	       
        kNIMVChatMp4RecordVideoSizeError = 1,
        /// <summary>
        /// MP4结束，磁盘空间不足
        /// </summary>
        kNIMVChatMp4RecordOutDiskSpace = 2,
        /// <summary>
        /// MP4文件创建
        /// </summary>    
        kNIMVChatMp4RecordCreate = 200,
        /// <summary>
        /// MP4文件已经存在
        /// </summary>
        kNIMVChatMp4RecordExsit = 400,
        /// <summary>
        /// MP4文件创建失败
        /// </summary>   
        kNIMVChatMp4RecordCreateError = 403,
        /// <summary>
        /// 通话不存在
        /// </summary>
        kNIMVChatMp4RecordInvalid = 404,
    };



    /// <summary>
    /// 发起和接受通话时的参数
    /// </summary>
    public class NIMVChatInfo : NimUtility.NimJsonObject<NIMVChatInfo>
    {
        /// <summary>
        /// 成员id列表，主动发起非空
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "uids", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> Uids { get; set; }

        /// <summary>
        /// 是否用自定义音频数据（PCM）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_audio")]
        public int CustomAudio { get; set; }

        /// <summary>
        /// 是否用自定义视频数据（i420）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_video")]
        public int CustomVideo { get; set; }

        public NIMVChatInfo()
        {
            CustomAudio = 0;
            CustomVideo = 0;
            Uids = null;
        }
    }

    public class NIMVChatSessionInfo : NimUtility.NimJsonObject<NIMVChatSessionInfo>
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Status { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "record_addr", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RecordAddr { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "record_file", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RecordFile { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Type { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "time", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long Time { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "accept", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Accept { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "client", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Client { get; set; }

        public NIMVChatSessionInfo()
        {
            Uid = null;
            Status = 0;
            RecordAddr = null;
            RecordFile = null;
            Type = 0;
            Time = 0;
            Accept = 0;
            Client = 0;
        }
    }

    /// <summary>
    /// 音量状态
    /// </summary>
    public class NIMVchatAudioVolumeState : NimUtility.NimJsonObject<NIMVchatAudioVolumeState>
    {
        public class AudioVolume
        {
            public class State
            {
                [Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public int Status { get; set; }
            }

            public class ReceiverState : State
            {
                [Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public string Uid { get; set; }
            }

            /// <summary>
            /// 自己的状态
            /// </summary>
            [Newtonsoft.Json.JsonProperty(PropertyName = "self", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public State Self { get; set; }

            /// <summary>
            /// 接收方状态
            /// </summary>
            [Newtonsoft.Json.JsonProperty(PropertyName = "receiver", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public ReceiverState[] Receivers { get; set; }
        }

        /// <summary>
        /// 音量状态
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "audio_volume", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public AudioVolume Volume { get; set; }
    }

    /// <summary>
    /// 实时状态 
    /// </summary>
    public class NIMVChatRealtimeState : NimUtility.NimJsonObject<NIMVChatRealtimeState>
    {
        public class StateInfo
        {
            public class Video : Audio
            {
                [Newtonsoft.Json.JsonProperty(PropertyName = "width", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public int Width { get; set; }

                [Newtonsoft.Json.JsonProperty(PropertyName = "height", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public int Height { get; set; }
            }

            public class Audio
            {
                [Newtonsoft.Json.JsonProperty(PropertyName = "fps", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public int FPS { get; set; }

                [Newtonsoft.Json.JsonProperty(PropertyName = "KBps", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
                public int KBps { get; set; }
            }

            /// <summary>
            /// 视频信息
            /// </summary>
            [Newtonsoft.Json.JsonProperty(PropertyName = "video", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public Video VideoState { get; set; }

            /// <summary>
            /// 语音信息
            /// </summary>
            [Newtonsoft.Json.JsonProperty(PropertyName = "audio", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public Audio AudioState { get; set; }
        }

        /// <summary>
        /// 状态信息
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "static_info", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public StateInfo Info { get; set; }
    }

    /// <summary>
    /// 调用接口回调
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionHandler(long channel_id, int code);

    /// <summary>
    /// 收到邀请
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="time">毫秒级 时间戳</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionInviteNotify(long channel_id, string uid, int mode, long time);

    /// <summary>
    /// 确认通话，接受拒绝通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="accept">结果</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionCalleeAckNotify(long channel_id, string uid, int mode, bool accept);

    /// <summary>
    /// 控制操作结果
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果</param>
    /// <param name="type">操作类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionControlRes(long channel_id, int code, int type);

    /// <summary>
    /// 控制操作通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="type">操作类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionControlNotify(long channel_id, string uid, int type);

    /// <summary>
    /// 通话中链接状态通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="record_addr">录制地址（服务器开启录制时有效）</param>
    /// <param name="record_file">录制文件名（服务器开启录制时有效）</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionConnectNotify(long channel_id, int code, string record_addr, string record_file);

    /// <summary>
    /// 通话中成员状态
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="status">状态</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionPeopleStatus(long channel_id, string uid, int status);

    /// <summary>
    /// 通话中网络状态
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="status">状态</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionNetStatus(long channel_id, int status);

    /// <summary>
    /// 其他端接听挂断后的同步通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="accept">结果</param>
    /// <param name="time">毫秒级 时间戳</param>
    /// <param name="client">客户端类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionSyncAckNotify(long channel_id, int code, string uid, int mode, bool accept, long time, int client);

    /// <summary>
    /// 音量状态通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="state">音量状态信息</param>
    public delegate void onSessionVolumeNotify(long channel_id, int code, NIMVchatAudioVolumeState state);

    /// <summary>
    /// 视频实时状态信息通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="state">实时状态信息</param>
    public delegate void onSessionRealtimeInfoNotify(long channel_id, int code, NIMVChatRealtimeState state);
}
#endif