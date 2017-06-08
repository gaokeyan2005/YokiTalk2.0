using NIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yoki.Comm;

namespace Yoki.IM
{

    internal class MultimediaManager
    {
        public static NIM.NIMVChatSessionStatus vchat_handlers;
        private MultimediaManager()
        {
        }

        private static MultimediaManager _instance = null;
        public static MultimediaManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MultimediaManager();
                    //注册音频接收数据回调
                    NIM.DeviceAPI.SetAudioReceiveDataCb(_instance.AudioReceiveHandler);
                    //注册音频采集数据回调
                    NIM.DeviceAPI.SetAudioCaptureDataCb(_instance.AudioCaptureHandler);
                    //注册视频接收数据回调
                    NIM.DeviceAPI.SetVideoReceiveDataCb(_instance.VideoReceiveHandler);
                    //注册视频采集数据回调
                    NIM.DeviceAPI.SetVideoCaptureDataCb(_instance.VideoCaptureHandler);
                }
                return _instance;
            }
        }

        private ReceivceVChatHandle onVChatAck = null;

        public ReceivceVChatHandle OnVChatAck
        {
            get
            {
                return this.onVChatAck;
            }
            set
            {
                if (value != null)
                {
                    RealeaseHandle();
                    this.onVChatAck = value;
                    InitHandle();
                }
            }
        }


        private VDevicesState State = VDevicesState.Stoped;

        //hardware
        public event Core.NoneArgsHandle OnCameraStoped;



        //msg
        public event Core.MessageEventHandle OnFrontMessageReceived;

        //status
        public event Core.NoneArgsHandle OnVChatConnected;
        public event Core.NoneArgsHandle OnVChatEnded;

        //data

        //public event Core.MessageEventHandler<AudioDataInfo> OnAudioCaptured;
        //public event Core.MessageEventHandler<AudioDataInfo> OnAudioReceived;


        public event Core.DataEventHandle<Graphic.VideoFrame> OnVideoCaptured;
        public event Core.DataEventHandle<Graphic.VideoFrame> OnVideoReceived;


        private void InitHandle()
        {

            //创建通话结果
            vchat_handlers.onSessionStartRes = (channel_id, code) =>
            {
                if (code != 200)
                {
                    SendFrontMessage("Start Video session failed.");
                }
            };
            //通话邀请
            vchat_handlers.onSessionInviteNotify = (channel_id, uid, mode, time) =>
            {
                VChatHandleEventArgs args = new VChatHandleEventArgs();
                args.ChannelId = channel_id;
                args.Uid = uid;
                args.Type = mode == (int)NIM.NIMVideoChatMode.kNIMVideoChatModeAudio ? VChatType.Audio : VChatType.Video;

                if (this.State == VDevicesState.Running)
                {
                    new System.Threading.Thread(new System.Threading.ParameterizedThreadStart((o) =>
                    {
                        EndSession();

                        System.Threading.Thread.Sleep(2000);
                        NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
                        if (this.onVChatAck == null)
                        {
                            throw new Exception("OnVChatAck null exception");
                        }
                        bool isRecevie = OnVChatAck(args);
                        NIM.VChatAPI.CalleeAck(channel_id, isRecevie, info);
                    })).Start();
                }
                else
                {
                    new System.Threading.Thread(new System.Threading.ParameterizedThreadStart((o) =>
                    {
                        NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
                        if (this.onVChatAck == null)
                        {
                            throw new Exception("OnVChatAck null exception");
                        }
                        bool isRecevie = OnVChatAck(args);
                        NIM.VChatAPI.CalleeAck(channel_id, isRecevie, info);
                    })).Start();
                }
            };
            //确认通话，接受拒绝结果
            vchat_handlers.onSessionCalleeAckRes = (channel_id, code) =>
            {
                if (this.OnVChatConnected != null)
                {
                    this.OnVChatConnected();
                }
            };
            //确认通话，接受拒绝通知
            vchat_handlers.onSessionCalleeAckNotify = (channel_id, code, mode, accept) =>
            {
                if (accept)
                {
                    SendFrontMessage("Video call accpeted.");
                }
                else
                {
                    SendFrontMessage("Video call refused.");
                }
            };
            //NIMVChatControlType 结果
            vchat_handlers.onSessionControlRes = (channel_id, code, type) =>
            {
            };
            //NIMVChatControlType 通知
            vchat_handlers.onSessionControlNotify = (channel_id, uid, type) =>
            {
            };
            //通话中链接状态通知
            vchat_handlers.onSessionConnectNotify = (channel_id, code, record_addr, record_file) =>
            {
                if (code == 200)
                {
                    StartDevices();
                }
                else
                {
                    NIM.VChatAPI.End();
                }
            };
            //通话中成员状态
            vchat_handlers.onSessionPeopleStatus = (channel_id, uid, status) =>
            {
                //status = 0 user is online, status = 1 user is offline.
                if (status == 1)
                {
                    EndSession();
                }
            };
            //通话中网络状态
            vchat_handlers.onSessionNetStatus = (channel_id, status) =>
            {
                switch ((NIMVideoChatSessionNetStat)status)
                {
                    case NIMVideoChatSessionNetStat.kNIMVideoChatSessionNetStatVeryGood:
                        SendFrontMessage("Network status is very good.");
                        LogUtil.WriteLogOperation(Comm.Object.LogType.Net, "Network status is very good.");
                        break;
                    case NIMVideoChatSessionNetStat.kNIMVideoChatSessionNetStatGood:
                        SendFrontMessage("Network status is better.");
                        LogUtil.WriteLogOperation(Comm.Object.LogType.Net, "Network status is better.");
                        break;
                    case NIMVideoChatSessionNetStat.kNIMVideoChatSessionNetStatBad:
                        SendFrontMessage("Network status is poor.");
                        LogUtil.WriteLogOperation(Comm.Object.LogType.Net, "Network status is poor.");
                        break;
                    case NIMVideoChatSessionNetStat.kNIMVideoChatSessionNetStatVeryBad:
                        SendFrontMessage("Network status is very poor.");
                        LogUtil.WriteLogOperation(Comm.Object.LogType.Net, "Network status is very poor.");
                        break;
                    default:
                        SendFrontMessage("Network status null exception.");
                        LogUtil.WriteLogOperation(Comm.Object.LogType.Net, "Network status null exception.");
                        break;
                }
            };
            //通话挂断结果
            vchat_handlers.onSessionHangupRes = (channel_id, code) =>
            {
                EndSession();
            };
            //通话被挂断通知
            vchat_handlers.onSessionHangupNotify = (channel_id, code) =>
            {
                EndSession();
            };
            //通话接听挂断同步通知
            vchat_handlers.onSessionSyncAckNotify = (channel_id,code ,uid, mode, accept, time, client) =>
            {

            };
            //注册音视频会话交互回调
            NIM.VChatAPI.SetSessionStatusCb(vchat_handlers);
            
        }


        private void SendFrontMessage(string msg)
        {
            if (this.OnFrontMessageReceived != null)
            {
                this.OnFrontMessageReceived(msg);
            }
        }


        private void RealeaseHandle()
        {
            // NIM.NIMVChatSessionStatus vchat_handlers;

            vchat_handlers.onSessionStartRes = null;
            vchat_handlers.onSessionInviteNotify = null;
            vchat_handlers.onSessionCalleeAckRes = null;
            vchat_handlers.onSessionCalleeAckNotify = null;
            vchat_handlers.onSessionControlRes = null;
            vchat_handlers.onSessionControlNotify = null;
            vchat_handlers.onSessionConnectNotify = null;
            vchat_handlers.onSessionPeopleStatus = null;
            vchat_handlers.onSessionNetStatus = null;
            vchat_handlers.onSessionHangupRes = null;
            vchat_handlers.onSessionHangupNotify = null;
            vchat_handlers.onSessionSyncAckNotify = null;
            //注册音视频会话交互回调
            NIM.VChatAPI.SetSessionStatusCb(vchat_handlers);
        }

        public class AudioDataRecArgs: EventArgs
        {
            public byte[] Data
            {
                get;
                set;
            }
        }

        //public event EventHandler<AudioDataRecArgs> OnAudioDataReced;
        void AudioReceiveHandler(UInt64 time, IntPtr data, UInt32 size, Int32 rate)
        {
          
        }

        void AudioCaptureHandler(UInt64 time, IntPtr data, UInt32 size, Int32 rate)
        {
           
        }
        

        void VideoReceiveHandler(ulong time, IntPtr data, uint size, uint width, uint height, string json_extension, IntPtr user_data)
        {
            if (OnVideoReceived != null)
            {
                Graphic.VideoFrame frame = new Graphic.VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
                OnVideoReceived(frame);
            }
        }


        void VideoCaptureHandler(ulong time, IntPtr data, uint size, uint width, uint height, string json_extension, IntPtr user_data)
        {
            if (this.OnVideoCaptured != null)
            {
                Graphic.VideoFrame frame = new Graphic.VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
                OnVideoCaptured(frame);
            }
        }
        
        public void StartDevices()
        {
            StartCamera();
            StartSpeaker();
            StartMicphone();
            this.State = VDevicesState.Running;
        }

        public void EndDevices()
        {
            EndCamera();
            EndSpeaker();
            EndMicphone();
            this.State = VDevicesState.Stoped;
        }
        
        public void EndCamera()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
            if (this.OnCameraStoped != null)
            {
                this.OnCameraStoped();
            }
        }

        public void StartCamera()
        {
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo, "", 0, startDeviceHandler);//开启摄像头
        }


        public void EndMicphone()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
        }

        public void StartMicphone()
        {
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0, startDeviceHandler);//开启麦克风
        }


        public void EndSpeaker()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
        }

        public void StartSpeaker()
        {
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0, startDeviceHandler);//开启扬声器播放对方语音
        }

        public void EndSession()
        {
            EndDevices();
            NIM.VChatAPI.End();
            if (this.OnVChatEnded != null)
            {
                OnVChatEnded();
            }
        }
        
        NIM.DeviceAPI.StartDeviceResultHandler startDeviceHandler = (type, ret) =>
        {

        };
    }


}
