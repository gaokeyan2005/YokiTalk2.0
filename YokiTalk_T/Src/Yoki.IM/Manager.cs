using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.IM
{
    public class Manager
    {
        public event Core.NoneArgsHandle OnOffline;
        public event Core.NoneArgsHandle OnKickout;
        public event Core.DataEventHandle<NIM.SysMessage.NIMSysMsgEventArgs> OnReceiveSysMsg;
        
        //hardware
        public event Core.NoneArgsHandle OnCameraStoped;
        //msg
        public event Core.MessageEventHandle OnFrontMessageReceived;
        //status
        public event Core.NoneArgsHandle OnVChatConnected;
        //data
        public event Core.DataEventHandle<Graphic.VideoFrame> OnVideoCaptured;
        public event Core.DataEventHandle<Graphic.VideoFrame> OnVideoReceived;

        public event Core.NoneArgsHandle OnVChatEnded;

        private Manager()
        {
            NIMManager.Instance.OnOffline += () =>
            {
                if (this.OnOffline != null)
                {
                    this.OnOffline();
                }
            };
            NIMManager.Instance.OnKickout += () =>
            {
                if (this.OnKickout != null)
                {
                    this.OnKickout();
                }
            };
            NIMManager.Instance.OnReceiveSysMsg += (e) =>
            {
                if (this.OnReceiveSysMsg != null)
                {
                    this.OnReceiveSysMsg(e);
                }
            };

            MultimediaManager.Instance.OnCameraStoped += () =>
            {
                if (this.OnCameraStoped != null)
                {
                    this.OnCameraStoped();
                }
            };
            MultimediaManager.Instance.OnFrontMessageReceived += (e) =>
            {
                if (this.OnFrontMessageReceived != null)
                {
                    this.OnFrontMessageReceived(e);
                }
            };
            MultimediaManager.Instance.OnVChatConnected += () =>
            {
                if (this.OnVChatConnected != null)
                {
                    this.OnVChatConnected();
                }
            };
            MultimediaManager.Instance.OnVideoCaptured += (frame) =>
            {
                if (this.OnVideoCaptured != null)
                {
                    this.OnVideoCaptured(frame);
                }
            };
            MultimediaManager.Instance.OnVideoReceived += (frame) =>
            {
                if (this.OnVideoReceived != null)
                {
                    this.OnVideoReceived(frame);
                }
            };

            MultimediaManager.Instance.OnVChatEnded += () =>
            {
                if (this.OnVChatEnded != null)
                {
                    this.OnVChatEnded();
                }
            };
        }

        private static Manager _instance = null;
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Manager();
                }
                return _instance;
            }
        }

        public ReceivceVChatHandle OnVChatAck
        {
            get
            {
                return MultimediaManager.Instance.OnVChatAck;
            }
            set
            {
                MultimediaManager.Instance.OnVChatAck = value;
            }
        }

        public void SendSysMsg(long receiveId, long myId, string msg)
        {
            NIMManager.Instance.SendSysMsg(receiveId, myId, msg);
        }


        public bool IMLogin(string userName, string password)
        {
            return NIMManager.Instance.Login(userName, password);
        }


        private bool cameraEnable = false;
        public bool CameraEnable
        {
            get
            {
                return this.cameraEnable;
            }
            set
            {
                this.cameraEnable = value;
                if (this.cameraEnable)
                {
                    MultimediaManager.Instance.StartCamera();
                }
                else
                {
                    MultimediaManager.Instance.EndCamera();
                }
            }
        }

        private bool micphoneEnable = false;
        public bool MicphoneEnable
        {
            get
            {
                return this.micphoneEnable;
            }
            set
            {
                this.micphoneEnable = value;
                if (this.micphoneEnable)
                {
                    MultimediaManager.Instance.StartMicphone();
                }
                else
                {
                    MultimediaManager.Instance.EndMicphone();
                }
            }
        }


        private bool speakerEnable = false;
        public bool SpeakerEnable
        {
            get
            {
                return this.speakerEnable;
            }
            set
            {
                this.speakerEnable = value;
                if (this.speakerEnable)
                {
                    MultimediaManager.Instance.StartSpeaker();
                }
                else
                {
                    MultimediaManager.Instance.EndSpeaker();
                }
            }
        }

        public void JoinRoom()
        {
            bool reslut = NIM.VChatAPI.JoinRoom(NIM.NIMVideoChatMode.kNIMVideoChatModeVideo, "572", null, new NIM.nim_vchat_opt2_cb_func((e1, e2, e3, e4) =>
            {

            }), IntPtr.Zero);
        }




        public void EndSession()
        {
            MultimediaManager.Instance.EndSession();
        }


    }
}
