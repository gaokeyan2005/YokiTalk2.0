using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.IM
{
    internal class NIMManager
    {
        private static string Appkey = "623b4d864e4b3a9570f67ada2847ae37";

        public event Core.NoneArgsHandle OnOffline;
        public event Core.NoneArgsHandle OnKickout;
        
        public event Core.DataEventHandle<NIM.SysMessage.NIMSysMsgEventArgs> OnReceiveSysMsg;
        
        private NIMManager()
        {
            InitSDK();
        }
        
        private static NIMManager _instance = null;
        public static NIMManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NIMManager();
                }
                return _instance;
            }
        }
        
        void InitSDK()
        {
            
            var config = ConfigReader.GetServerConfig();
            if (!NIM.ClientAPI.Init("YoKiTalk",  null, config))
            {
                throw new Exception("NIM init failed!");
            }
            if (!NIM.VChatAPI.Init())
            {
                throw new Exception("NIM VChatAPI init failed!");
            }

            //sdk init 完成后注册全局回调函数
            NIM.ClientAPI.RegDisconnectedCb(() =>
            {
                if (this.OnOffline != null)
                {
                    this.OnOffline();
                }
            });

            NIM.ClientAPI.RegKickoutCb((r) =>
            {
                if (this.OnKickout != null)
                {
                    this.OnKickout();
                }
            });
            
            NIM.ClientAPI.RegMultiSpotLoginNotifyCb((r) =>
            {
                if (this.OnKickout != null)
                {
                    this.OnKickout();
                }
            });

            NIM.SysMessage.SysMsgAPI.ReceiveSysMsgHandler += (s, args) =>
            {
                // receive sysmsg logic.
                if (this.OnReceiveSysMsg != null)
                {
                    this.OnReceiveSysMsg(args);
                }
            };
        }

        public void ClearupSDK()
        {
            NIM.VChatAPI.Cleanup();
            NIM.ClientAPI.Cleanup();
        }

        public bool Login(string userName, string password)
        {
            bool result = false;

            Exception ex = null;

            //password = NIM.ToolsAPI.GetMd5(password);
            System.Threading.AutoResetEvent are = new System.Threading.AutoResetEvent(false);
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {

                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    NIM.ClientAPI.Login(Appkey, userName, password, (r) =>
                    {
                        // on login step call back.
                        if (r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLinking || 
                            r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLink ||
                            r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogining ||
                            r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogin)
                        {
                            if (r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogin && r.Code == NIM.ResponseCode.kNIMResSuccess)
                            {
                                // on login successful.
                                result = true;
                                are.Set();
                            }
                            else if (
                                        (r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLinking ||
                                        r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLink ||
                                        r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogining ||
                                        r.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogin) 
                                        &&
                                        (r.Code == NIM.ResponseCode.kNIMResUidPassError ||
                                        r.Code == NIM.ResponseCode.kNIMResTimeoutError ||
                                        r.Code == NIM.ResponseCode.kNIMResConnectionError
                                        )
                                    )
                            {
                                //logout when login failed.
                                NIM.ClientAPI.Logout(NIM.NIMLogoutType.kNIMLogoutChangeAccout, (rout) =>
                                {
                                    // on logout.
                                    if (r.Code == NIM.ResponseCode.kNIMResUidPassError)
                                    {
                                        ex = new LoginMismatchException();
                                    }
                                    if (r.Code == NIM.ResponseCode.kNIMResTimeoutError)
                                    {
                                        ex = new LoginTimeoutException();
                                    }
                                    if (r.Code == NIM.ResponseCode.kNIMResConnectionError)
                                    {
                                        ex = new NetworkException();
                                    }
                                    are.Set();
                                });
                            }
                        }
                    });
                });
            }
            else
            {
                ex = new LoginNullException();
                are.Set();
            }
            are.WaitOne();
            if (ex != null)
            {
                throw ex;
            }

            return result;
        }

        public void Relogin()
        {
            NIM.ClientAPI.Relogin();
        }
        
        public void SendSysMsg(long receiveId, long myId, string msg)
        {
            NIM.SysMessage.NIMSysMessageContent content = new NIM.SysMessage.NIMSysMessageContent();
            content.MsgType = NIM.SysMessage.NIMSysMsgType.kNIMSysMsgTypeCustomP2PMsg;
            content.SupportOffline = NIM.NIMMessageSettingStatus.kNIMMessageStatusUndefine;
            content.Timetag = DateTime.Now.Ticks;
            content.ReceiverId = receiveId .ToString();
            content.SenderId = myId.ToString();
            content.Attachment = msg;
            NIM.SysMessage.SysMsgAPI.SendCustomMessage(content);

            NIM.SysMessage.SysMsgAPI.SendSysMsgHandler += (o, e) =>
            {
                NIM.MessageArcEventArgs args = e;
            };
        }

    }
}
