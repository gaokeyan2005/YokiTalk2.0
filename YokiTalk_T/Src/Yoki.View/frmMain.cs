using NIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yoki.Comm;

namespace Yoki.View
{
    public partial class frmMain : Fink.Windows.Forms.FormEx
    {
        private static int PageLocal = 1;//当前点击课件的页数
        private static int PageAll = 0;//课件总页数
        private System.Windows.Forms.Timer timeVideo;//视频定时器
        System.Threading.AutoResetEvent hurryCheckStatusARE = new System.Threading.AutoResetEvent(false);
        public frmMain()
            : base(Fink.Windows.Forms.FromStyle.Metro)
        {
            this.KeyPreview = true;
            InitializeComponent();

            this.MinimumSize = new Size(1000, 600);
            this.MaximumSize = new Size(1000, 600);
            //this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.TitleOffset = 64;
            this.Text += Business.AccountController.GetEnvironment();
            timeVideo = new Timer();
            timeVideo.Enabled = true;
            timeVideo.Interval = 1000;
            timeVideo.Tick += new System.EventHandler(timeVideo_Tick);
            //用户UI控件属性设置方法
            InitUI();
            //初始化回调
            InitCallBack();
            //获取教师评价学生项目列表
            GetEvaluateProto();

            this.classRoom.ImageViewer.MouseClick += ImageViewer_MouseClick;
            this.ucReload.ImageClicked += UcReload_ImageClicked;
            this.ucReload.BringToFront();
            
            this.classRoom.TabControl.Click += TabControl_Click;
            this.Load += (o, e) =>
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o1) =>
                {
                    CheckTeacherStatus();
                    while (true)
                    {
                        Yoki.Comm.Object.TeacherStatus teacherStatus = CheckTeacherStatusLoop();
                        this.userHeader.ToolTipText = teacherStatus.ToString();
                        switch (teacherStatus)
                        {
                            case Comm.Object.TeacherStatus.Idle:
                                //Green
                                this.userHeader.NotifyColor = Color.FromArgb(255, 56, 180, 75);
                                break;
                            case Comm.Object.TeacherStatus.WaitingForClass:
                                //Yellow
                                this.userHeader.NotifyColor = Color.FromArgb(255, 254, 206, 48);
                                break;
                            case Comm.Object.TeacherStatus.InClass:
                                //Blue
                                this.userHeader.NotifyColor = Color.FromArgb(255, 16, 174, 239);
                                break;
                            case Comm.Object.TeacherStatus.NeedEvaluate:
                                //red
                                this.userHeader.NotifyColor = Color.FromArgb(255, 251, 99, 98);
                                if (CheckAndEvaluate())
                                {
                                    Business.AccountController.Instance.Data.ClassInfo = null;
                                    this.classRoom.ImageViewer.Links = null;
                                    this.classRoom.LabPage.Text = "";
                                    PageLocal = 1;
                                    ToWaitingRoom();
                                    HurryCheckStatus();
                                    timeVideo.Enabled = false;
                                }
                                break;
                            case Comm.Object.TeacherStatus.Pause:
                                //Yellow
                                this.userHeader.NotifyColor = Color.FromArgb(255, 254, 206, 48);
                                break;
                            case Comm.Object.TeacherStatus.Locked:
                                //Gray
                                this.userHeader.NotifyColor = Color.FromArgb(255, 230, 230, 230);
                                break;
                            default:
                                break;
                        }
                        //判断上课时间是否已经超过17分钟，如果超过系统自动结束上课
                        if (Business.AccountController.Instance.Data.ClassInfo != null)
                        {
                            if (Business.AccountController.Instance.Data.ClassInfo.StartClassTime != DateTime.MinValue &&
                            (DateTime.Now - Business.AccountController.Instance.Data.ClassInfo.StartClassTime).TotalMinutes >= 17)
                            {
                                LogUtil.WriteLogOperation(Comm.Object.LogType.End, "Class time, the system automatically ends class");
                                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o2) =>
                                {
                                    if (Business.AccountController.Instance.Data.ClassInfo != null)
                                    {
                                        if (Business.AccountController.Instance.Data.ClassInfo.LogID > 0)
                                        {
                                            LogUtil.WriteLogOperation(Comm.Object.LogType.End, "Class time, the system automatically ends class(triggering the class interface)");
                                            Business.AccountController.Instance.EndClass(this, Business.AccountController.Instance.Data.ClassInfo.LogID);
                                        }
                                    }
                                    Yoki.IM.Manager.Instance.EndSession();
                                }));
                            }
                        }
                        //设定线程阻止时间30秒，每30秒执行一次
                        hurryCheckStatusARE.WaitOne(30 * 1000);
                    }
                }));
            };
        }
        /// <summary>
        /// 上课时长定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void timeVideo_Tick(object sender, EventArgs e)
        {
            // update time tag
            if (Business.AccountController.Instance.Data.ClassInfo != null)
            {
                if (Business.AccountController.Instance.Data.ClassInfo.StartClassTime != DateTime.MinValue)
                {
                    TimeSpan startTimeSpan = (DateTime.Now - Business.AccountController.Instance.Data.ClassInfo.StartClassTime);
                    classRoom.VideoPanel.IsTimeWaring = startTimeSpan.TotalMinutes >= 14;// 设置警告时间范围，大于14分钟后显示红色
                    classRoom.VideoPanel.TimeTag = startTimeSpan.ToString(@"mm\:ss");
                }
            }
        }
        private void TabControl_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            //MessageBox.Show(this.classRoom.TabControl.SelectedTab.ToString());
            if (this.classRoom.TabControl.SelectedTab.ToString().IndexOf("Student info")>-1)
            {
                this.ucReload.SendToBack();
                classRoom.LabPage.Visible = false;
            }
            else
            {
                this.ucReload.BringToFront();
                classRoom.LabPage.Visible = true;
            }
        }
        /// <summary>
        /// 点击刷新加载课件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcReload_ImageClicked(object sender, EventArgs e)
        {
            if (Business.AccountController.Instance.Data.ClassInfo != null)
            {
                timeVideo.Enabled = true;
                this.classRoom.Title = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Title;
                this.classRoom.ImageViewer.Links = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links;
                PageAll = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links.Count();
                this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
            }
        }

        /// <summary>
        /// 处理ImageViewer上一页下一页点击触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if (ServerComm.RLocations[1].Contains(e.Location))
            {
                if (PageLocal > 1)
                {
                    PageLocal--;
                    this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                }
            }
            else if (ServerComm.RLocations[2].Contains(e.Location))
            {
                if (PageLocal < PageAll)
                {
                    PageLocal++;
                    this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                }
            }
        }

        /// <summary>
        /// 线程事件设置为终止状态
        /// </summary>
        private void HurryCheckStatus()
        {
            hurryCheckStatusARE.Set();
        }
        /// <summary>
        /// 进入上课界面
        /// </summary>
        private void ToClassRoom()
        {
#if IMTEST
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.classRoom.Visible = true;
                this.waitingRoom.Visible = false;
            });
#else
            if (Business.AccountController.Instance.Data != null && Business.AccountController.Instance.Data.ClassInfo != null)
            {
                this.classRoom.RemoteUserInfo = new Yoki.Controls.RemoteUserInfo()
                {
                    Name = Business.AccountController.Instance.Data.ClassInfo.StuName,
                    Age = Business.AccountController.Instance.Data.ClassInfo.StuAge,
                    Gender = Business.AccountController.Instance.Data.ClassInfo.StuGender,
                };

                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.classRoom.Visible = true;
                    this.waitingRoom.Visible = false;
                });
            }

#endif
        }
        /// <summary>
        /// 进入等待呼叫界面
        /// </summary>
        private void ToWaitingRoom()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.waitingRoom.Visible = true;
                this.classRoom.Visible = false;
            });
        }
        /// <summary>
        /// 检测老师状态
        //status 0 空闲 1 准备上课，2 上课中，3暂停
        //class_status  0 收到请求 1 获取课程成功 ,2 分配课程成功（权限够），3 成功分配教师，4成功通知云信，5教师收到通知，6老师接受，7老师拒绝 8 开始上课 9学生取消呼叫 10 下课
        //call_log_id  呼叫记录ID
        /// </summary>
        /// <returns></returns>
        private Yoki.Comm.Object.TeacherStatus CheckTeacherStatusLoop()
        {
            Yoki.Comm.Response.CheckTeacherStatusInfo info = Business.AccountController.Instance.CheckTeacherStatus(Business.AccountController.Instance.Data.UserID);

            return info == null ? Yoki.Comm.Object.TeacherStatus.Unknown : info.TeacherStatus;
        }
        /// <summary>
        /// 检测老师状态
        /// </summary>
        private void CheckTeacherStatus()
        {
            ToWaitingRoom();
            Yoki.Comm.Response.CheckTeacherStatusInfo info = Business.AccountController.Instance.CheckTeacherStatus(Business.AccountController.Instance.Data.UserID);

            if (info == null)
                return;

            switch (info.TeacherStatus)
            {
                case Comm.Object.TeacherStatus.WaitingForClass:
                case Comm.Object.TeacherStatus.InClass:
                    {
                        Control.CheckForIllegalCrossThreadCalls = false;
                        //empty logid
                        if (info.LogID <= 0)
                            return;
                        //根据呼叫ID查找课程信息
                        Yoki.Comm.Response.CallLogForTopicInfo topInfo =  Business.AccountController.Instance.CallLogForTopic(info.LogID);

                        //empty topic
                        if (topInfo == null)
                            return;

                        Business.AccountController.Instance.Data.ClassInfo = new Business.ClassInfo()
                        {
                            LogID = info.LogID,
                            TopicID = topInfo.TopicID,
                            TextbookID = topInfo.TextbookID,
                            StuID = topInfo.StuID,
                            StuAvatar = topInfo.StuAvatar,
                            StuName = topInfo.StuName,
                            StuAge = topInfo.StuAge,
                            StuGender = topInfo.StuGender,
                            StuGrade = "Grade 1",
                            TopicInfo = new Comm.Response.GetTopicDetailInfo()
                            {
                                Title = topInfo.Title,
                                Links = topInfo.Links,
                            },
                            StartClassTime = DateTime.Now.AddSeconds(0 - topInfo.ClassTime),
                        };

                        // goto class status
                        this.classRoom.Title = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Title;
                        this.classRoom.ImageViewer.Links = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links;
                        PageAll = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links.Count();
                        this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                        timeVideo.Enabled = true;

                        if (info.TeacherStatus == Comm.Object.TeacherStatus.InClass)
                        {
                            ToClassRoom();
                        }
                        //send sysMsg
                        Yoki.Business.Message.PushMsg push = new Yoki.Business.Message.PushMsg()
                        {
                            Code = 20003,
                            Message = string.Empty,
                            Data = new Yoki.Business.Message.ClassAccpetedInfo()
                            {
                                TeacherID = Business.AccountController.Instance.Data.UserID,
                                Type = Yoki.Comm.Object.ClassType.Video
                            }
                        };
                        Yoki.IM.Manager.Instance.SendSysMsg(Business.AccountController.Instance.Data.ClassInfo.StuID, Business.AccountController.Instance.Data.UserID, Yoki.Comm.ParamsHelper.Serializes(push));
                        
                    }
                    break;
                case Comm.Object.TeacherStatus.NeedEvaluate:
                    {
                        this.CheckAndEvaluate();
                        HurryCheckStatus();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取教师评价学生项目列表
        /// </summary>
        private void GetEvaluateProto()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
            {
                //获取教师评价学生项目列表
                Business.AccountController.Instance.Data.EvaluateProto = Business.AccountController.Instance.GetEvaluateProto();
            }));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //课件加载图片的左右按钮执行事件
            if (keyData == Keys.Right)
            {
                if (PageLocal < PageAll)
                {
                    PageLocal++;
                    this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                }
                this.classRoom.ImageViewer.NextPage();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                if (PageLocal > 0)
                {
                    PageLocal--;
                    this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                }
                this.classRoom.ImageViewer.LastPage();
                return true;
               
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        /// <summary>
        /// 用户UI控件属性设置方法
        /// </summary>
        private void InitUI()
        {
            this.CaptionFont = FontUtil.DefaultBoldFont;
            this.Font = FontUtil.DefaultFont;

            this.userHeader.Font = FontUtil.DefaultBoldFont;

            this.classRoom.Font = FontUtil.DefaultFont;
            this.classRoom.BoldFont = FontUtil.DefaultBoldFont;
            this.classRoom.MicroFont = FontUtil.DefaultMicroFont;

            this.waitingRoom.Location = new Point(140, 35);
            this.waitingRoom.Size = new Size(832, 514);
            this.waitingRoom.Font = FontUtil.DefaultMicroBoldFont;

            this.userHeader.HeaderImage = Business.AccountController.Instance.Data.Avatar == null?
                 ResourceHelper.EmptyHeader
                 :
                 Business.AccountController.Instance.Data.Avatar;
            Business.AccountController.Instance.Data.OnAvatarChanged += (o) =>
            {
                this.userHeader.HeaderImage = o == null? ResourceHelper.EmptyHeader: o;
            };
            //Business.AccountController.Instance.Data.Avatar;
            this.userHeader.HeaderTitle = Business.AccountController.Instance.Data.NickName;

            Yoki.Controls.BlockMenuItem item = new Yoki.Controls.BlockMenuItem();
            item.Icon = ResourceHelper.Settings;
            item.Name = "SETTINGS";
            item.Font = FontUtil.DefaultFont;

            this.blockMenu1.StaticItems.Add(item);
            
            //等待上课状态下，开关按钮改变事件         
            this.waitingRoom.OnSwitchChanged += (isWaiting) =>
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o1) =>
                {
                    if (CheckAndEvaluate() == false)
                    {
                        //api修改教师状态
                        Business.AccountController.Instance.ChangeTeacherStatus(this, Business.AccountController.Instance.Data.UserID, isWaiting ? Comm.Object.TeacherStatus.Idle : Comm.Object.TeacherStatus.Pause);
                        //线程事件设置为终止状态
                        HurryCheckStatus();
                    }
                    else
                    {
                        if (this.waitingRoom.IsWaiting == true)
                        {
                            this.waitingRoom.ChangeStatus(false);
                        }
                        else
                        {
                            this.waitingRoom.ChangeStatus(true);
                        }
                    }
                }));
               
            };
            
            //上课状态下，图像、话筒、音量按钮改变事件
            this.classRoom.VideoPanel.VideoControlPanel.SwitchCheckedChanged += (o, e) =>
            {
                this.classRoom.ImageList = this.imageListVideoControl;
                Fink.Windows.Forms.SwitchCapsuleCheckedArgs args = e as Fink.Windows.Forms.SwitchCapsuleCheckedArgs;
                switch (args.SourceIndex)
                {
                    case 0:
                        Yoki.IM.Manager.Instance.CameraEnable = !args.IsChecked;
                        break;
                    case 1:
                        Yoki.IM.Manager.Instance.MicphoneEnable = !args.IsChecked;
                        break;
                    case 2:
                        Yoki.IM.Manager.Instance.SpeakerEnable = !args.IsChecked;
                        break;
                    default:
                        break;
                }
            };

            //上课状态下，结束课程按钮的点击事件
            this.classRoom.BtnEndClass.Click += (o, e) =>
            {
                //点击结束课程“END”执行的事件
                DialogResult dr = DialogResult.None;
                using (Dialog.frmNotify dialog = new Dialog.frmNotify(this, "Are you sure to end this class?", ResourceHelper.CloseWaring))
                {
                    dialog.SuspendLayout();
                    dialog.Font = FontUtil.HeavyTitleFont;
                    dialog.Size = new Size(380, 160);
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    dialog.ResumeLayout(false);
                    dr = dialog.ShowDialog(this);
                }

                if (dr == DialogResult.OK)
                {
                    //手动点击结束课程写入日志
                    LogUtil.WriteLogOperation(Comm.Object.LogType.End, "Manual click Finish!");

                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o1) =>
                    {
                        if (Business.AccountController.Instance.Data.ClassInfo != null)
                        {
                            if (Business.AccountController.Instance.Data.ClassInfo.LogID > 0)
                            {
                                //根据学生信息是否获取成功来判断上课是否已经接入成功。
                                //接入上课成功执行调用下课接口
                                LogUtil.WriteLogOperation(Comm.Object.LogType.End, "StudentID:" + Business.AccountController.Instance.Data.ClassInfo.StuID.ToString() + ",Manual click Finish!");
                                Business.AccountController.Instance.EndClass(this, Business.AccountController.Instance.Data.ClassInfo.LogID);
                            }
                        }
                        //结束视频通话的调用
                        Yoki.IM.Manager.Instance.EndSession();
                        //线程事件设置为终止状态
                        HurryCheckStatus();
                        timeVideo.Enabled = false;
                    }));
                }
            };

            //视频监视报告
            Yoki.IM.QualityWatcher.Instance.OnWatchReport += (args) =>
            {
                this.classRoom.VideoQuality = new Yoki.Controls.VideoQuality()
                {
                    BlockCount = Convert.ToInt32(15 * 60 * 1000 / Yoki.IM.QualityWatcher.Durition),
                    FPSCollection = args.Quality.FpsVideoQueue.ToArray(),
                };
            };

        }
        
        // CLOSE ALL ON FORM CLOSING.
        protected override void OnClosing(CancelEventArgs e)
        {
            //退出登录
            LogUtil.WriteLogOperation(Comm.Object.LogType.Close, "Close the system, log off.");
            Business.AccountController.Instance.Logout();
            FormUtil.CloseTooltip(this);
        }

        /// <summary>
        /// 初始化回调
        /// </summary>
        private void InitCallBack()
        {
            //接收系统消息
            Yoki.IM.Manager.Instance.OnReceiveSysMsg += (o) =>
            {
                 LogUtil.WriteLine("{" + DateTime.Now.ToString() + "}" + "[OnReceiveSysMsg] e.Message = " + o.Message.Content.Attachment);
                //receive sys msg.
                Yoki.Comm.Response.ResponseBase receiveMsg = Yoki.Comm.ParamsHelper.Deserialize<Yoki.Comm.Response.ResponseBase>(o.Message.Content.Attachment);

                if (receiveMsg == null)
                {
                    return;
                }

                switch (receiveMsg.Code)
                {
                    case 20001:
                    {
                        Yoki.Business.Message.RequestTeacherMsg receiveMsgT = Yoki.Comm.ParamsHelper.Deserialize<Yoki.Business.Message.RequestTeacherMsg>(o.Message.Content.Attachment);
                        ProcSysMsg20001(receiveMsgT);
                    } break;
                    default:
                    {
                        
                    } break;
                }

                //case accept

                //case refuse
            };
            //通话邀请回应
            Yoki.IM.Manager.Instance.OnVChatAck = (args) =>
            {
                
                LogUtil.WriteLine("{" + DateTime.Now.ToString() + "}" + "[OnVChatAck] call user =  " + args.Uid);
#if IMTEST
                return true;
#else
                if (Business.AccountController.Instance.Data != null &&
                Business.AccountController.Instance.Data.ClassInfo != null &&
                Business.AccountController.Instance.Data.ClassInfo.StuID.ToString() == args.Uid)
                {
                    return true;
                }
                return false;
#endif
            };

            //视频连接成功触发事件
            Yoki.IM.Manager.Instance.OnVChatConnected += () =>
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                if (Business.AccountController.Instance.Data.ClassInfo != null)
                {
                    if (Business.AccountController.Instance.Data.ClassInfo.StuID > 0 &&
                        Business.AccountController.Instance.Data.ClassInfo.TopicID > 0 &&
                        Business.AccountController.Instance.Data.ClassInfo.TextbookID > 0 &&
                        Business.AccountController.Instance.Data.ClassInfo.LogID > 0)
                    {
                        Business.AccountController.Instance.StartClass(
                            this,
                            Business.AccountController.Instance.Data.ClassInfo.StuID,
                            Business.AccountController.Instance.Data.ClassInfo.TopicID,
                            Business.AccountController.Instance.Data.ClassInfo.TextbookID,
                            Business.AccountController.Instance.Data.ClassInfo.LogID);


                        LogUtil.WriteLine("{" + DateTime.Now.ToString() + "}" + "[OnVChatConnected] StartClass ClassInfo = " + Business.AccountController.Instance.Data.ClassInfo.Print());


                        if (Business.AccountController.Instance.Data.ClassInfo.StartClassTime == DateTime.MinValue)
                        {
                            Business.AccountController.Instance.Data.ClassInfo.StartClassTime = DateTime.Now;
                        }
                    }
                }
                ToClassRoom();
                HurryCheckStatus();

                if (Business.AccountController.Instance.Data.ClassInfo != null)
                {
                    if (Business.AccountController.Instance.Data.ClassInfo.TopicInfo != null)
                    {
                        this.classRoom.Title = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Title;
                        this.classRoom.ImageViewer.Links = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links;
                        PageAll = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Links.Count();
                        this.classRoom.LabPage.Text = PageLocal.ToString() + " / " + PageAll.ToString();
                    }
                }

                Yoki.IM.QualityWatcher.Instance.StartWatch();

            };

            //老师端视频加载事件处理过程
            Yoki.IM.Manager.Instance.OnVideoCaptured += (frame) =>
            {
                // update time tag
                if (Business.AccountController.Instance.Data.ClassInfo != null)
                {
                    if (Business.AccountController.Instance.Data.ClassInfo.StartClassTime != DateTime.MinValue)
                    {
                        TimeSpan startTimeSpan = (DateTime.Now - Business.AccountController.Instance.Data.ClassInfo.StartClassTime);
                        this.classRoom.VideoPanel.IsTimeWaring = startTimeSpan.TotalMinutes >= 14;// 14;
                        this.classRoom.VideoPanel.TimeTag = startTimeSpan.ToString(@"mm\:ss");
                    }
                }
                RenderVideo(this.classRoom.VideoPanel.CapturedVideoBox, frame);
            };

            //学生端视频加载事件处理过程
            Yoki.IM.Manager.Instance.OnVideoReceived += (frame) =>
            {                
                // update time tag
                if (Business.AccountController.Instance.Data.ClassInfo != null)
                {
                    if (Business.AccountController.Instance.Data.ClassInfo.StartClassTime != DateTime.MinValue)
                    {
                        Yoki.Controls.RemoteUserInfo stuInfo = new Yoki.Controls.RemoteUserInfo();
                        stuInfo.Age = Business.AccountController.Instance.Data.ClassInfo.StuAge;
                        stuInfo.Name = Business.AccountController.Instance.Data.ClassInfo.StuName;
                        stuInfo.Gender = Business.AccountController.Instance.Data.ClassInfo.StuGender;
                        this.classRoom.VideoPanel.ReceivedVideoBox.RemoteUserInfo = stuInfo;
                    }
                }
                
                RenderVideo(this.classRoom.VideoPanel.ReceivedVideoBox, frame);
                Yoki.IM.QualityWatcher.Instance.VideoTickTock();
            };
            //视频结束加载发生的事件
            Yoki.IM.Manager.Instance.OnVChatEnded += () =>
            {
#if IMTEST
                this.classRoom.VideoPanel.Status = Yoki.Controls.VideoBoxStatus.NoVideo;

                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
                {
                    System.Threading.Thread.Sleep(2 * 1000);
                    this.classRoom.VideoPanel.Status = Yoki.Controls.VideoBoxStatus.NoVideo;
                }));

                if (CheckAndEvaluate())
                {
                    Business.AccountController.Instance.Data.ClassInfo = null;
                    this.classRoom.ImageViewer.Links = null;
                    this.classRoom.LabPage.Text = "";
                    PageLocal = 1;
                    ToWaitingRoom();
                    HurryCheckStatus();
                    timeVideo.Enabled = false;
                }

                Yoki.IM.QualityWatcher.Instance.EndWatch();
#else
                this.classRoom.VideoPanel.Status = Yoki.Controls.VideoBoxStatus.NoVideo;

                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
                {
                    System.Threading.Thread.Sleep(2 * 1000);
                    this.classRoom.VideoPanel.Status = Yoki.Controls.VideoBoxStatus.NoVideo;
                }));
                
                if (CheckAndEvaluate())
                {
                    Business.AccountController.Instance.Data.ClassInfo = null;
                    this.classRoom.ImageViewer.Links = null;
                    ToWaitingRoom();
                    HurryCheckStatus();
                }
                Yoki.IM.QualityWatcher.Instance.EndWatch();
#endif
            };
        }

        private void ProcSysMsg20001(Yoki.Business.Message.RequestTeacherMsg receiveMsg)
        {
            //获得上课相关信息
            Business.AccountController.Instance.Data.ClassInfo = new Business.ClassInfo()
            {
                StuID = receiveMsg.Data.StuID,
                StuAvatar = receiveMsg.Data.StuAvatar,
                StuName = receiveMsg.Data.StuName,
                StuAge = receiveMsg.Data.StuAge,
                StuGender = receiveMsg.Data.StuGender,
                StuGrade = "Grade 2",
                TopicID = receiveMsg.Data.TopicID,
                TextbookID = receiveMsg.Data.TextbookID,
                LogID = receiveMsg.Data.LogID,
                TopicInfo = Business.AccountController.Instance.GetTopicDetail(
                receiveMsg.Data.TopicID,
                receiveMsg.Data.TextbookID),
            };

            // call server，呼叫通知到教师
            Business.AccountController.Instance.CallNotice(receiveMsg.Data.LogID);

            // check user status，检查学生状态
            Yoki.Comm.Object.StudentStatus ss = Business.AccountController.Instance.CheckStudentStatus(Business.AccountController.Instance.Data.ClassInfo.StuID);

            if (ss != Comm.Object.StudentStatus.WaitingForClass)
            {
                //学生不在等待上课状态直接返回
                return;
            }

            //show dialog 获得上课学生信息
            Yoki.Business.StudentClassInfo scInfo = new Yoki.Business.StudentClassInfo()
            {
                HeaderImage = Business.AccountController.Instance.Data.ClassInfo.StuAvatarImage == null ? ResourceHelper.EmptyHeader : Business.AccountController.Instance.Data.ClassInfo.StuAvatarImage,
                UserName = Business.AccountController.Instance.Data.ClassInfo.StuName,
                Gender = Business.AccountController.Instance.Data.ClassInfo.StuGender,
                Age = Business.AccountController.Instance.Data.ClassInfo.StuAge,
                Grade = Business.AccountController.Instance.Data.ClassInfo.StuGrade,
            };
            

            //Active window and play sound.
            this.BeginInvoke((MethodInvoker)delegate
            {
                Fink.Windows.Forms.FormEx.ActivateWindow(this.Handle);
            });

            System.IO.Stream soundStream = ResourceHelper.RingNewOrder;
            System.Media.SoundPlayer sPlayer = new System.Media.SoundPlayer(soundStream);
            
             System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
             {
                 //使用新线程循环播放.wav 文件
                 sPlayer.PlayLooping();
             }));
                        
            DialogResult dr = DialogResult.None;
            System.Threading.AutoResetEvent are = new System.Threading.AutoResetEvent(false);
            this.BeginInvoke((MethodInvoker)delegate
            {
                using (Dialog.frmSessionCalleeAckNotify dialog = new Dialog.frmSessionCalleeAckNotify(this))
                {
                    //调用呼叫上课接收弹出框
                    dialog.StuClassInfo = scInfo;
                    dialog.StartPosition = FormStartPosition.CenterParent;
                    Business.AccountController.Instance.Data.ClassInfo.OnAvatarChanged += (image) =>
                    {
                        dialog.HeaderImage = image;
                    };
                    dr = dialog.ShowDialogWithCounter(this, 30);
                    
                    Business.AccountController.Instance.Data.ClassInfo.OnAvatarChanged = null;
                }
                are.Set();
            });
            are.WaitOne();


            if (sPlayer != null)
            {
                //停止播放呼叫音乐
                sPlayer.Stop();
                sPlayer.Dispose();
            }

            if (dr == DialogResult.OK)
            {
                //呼叫连接执行手动同意接入写入日志
                LogUtil.WriteLogOperation(Comm.Object.LogType.Answer, "Call connection performs manual consent access!");

                Business.AccountController.Instance.CallAccept(receiveMsg.Data.LogID);

                Yoki.Business.Message.PushMsg push = new Yoki.Business.Message.PushMsg()
                {
                    Code = 20002,
                    Message = string.Empty,
                    Data = new Yoki.Business.Message.ClassAccpetedInfo()
                    {
                        TeacherID = Business.AccountController.Instance.Data.UserID,
                        Type = Yoki.Comm.Object.ClassType.Video
                    }
                };
                Yoki.IM.Manager.Instance.SendSysMsg(Business.AccountController.Instance.Data.ClassInfo.StuID, Business.AccountController.Instance.Data.UserID, Yoki.Comm.ParamsHelper.Serializes(push));

            }
            else if (dr == DialogResult.Cancel)
            {
                //呼叫连接执行手动拒绝写入日志
                LogUtil.WriteLogOperation(Comm.Object.LogType.Answer, "Call connection to perform manual rejection!");
                this.waitingRoom.ChangeStatus(false);
                Business.AccountController.Instance.CallRefuse(receiveMsg.Data.LogID);
            }
            else if (dr == DialogResult.No)
            {
                //呼叫连接时间执行完成自动拒绝写入日志
                LogUtil.WriteLogOperation(Comm.Object.LogType.Answer, "30s Call connection time execution completed automatically rejected!");
                this.waitingRoom.ChangeStatus(false);
                Business.AccountController.Instance.CallRefuse(receiveMsg.Data.LogID);
            }
        }

        /// <summary>
        /// 渲染视频
        /// </summary>
        /// <param name="vBox"></param>
        /// <param name="frame"></param>
        private void RenderVideo(Yoki.Controls.VideoBox vBox, Yoki.IM.Graphic.VideoFrame frame)
        {
            vBox.Status = Yoki.Controls.VideoBoxStatus.Video;
            vBox.IsBeginShowVideo = true;
            vBox.CacheImage = Yoki.IM.Graphic.VideoGraphic.ShowVideoFrame(vBox.Graphics,
            vBox.Size,
            frame,
            vBox.OverlayerImage,
            vBox.OverlayerRectangle);
        }

        private bool isEvaluating = false;
        /// <summary>
        /// 检测老师是否需要进行评价
        /// </summary>
        /// <returns></returns>
        private bool CheckAndEvaluate()
        {
            bool isEvaluated = false;
            if (isEvaluating)
            {
                return isEvaluated;
            }
            isEvaluating = true;

        RECHECK:
            //检测老师状态
            Yoki.Comm.Response.CheckTeacherStatusInfo info = Business.AccountController.Instance.CheckTeacherStatus(Business.AccountController.Instance.Data.UserID);
            if(info==null)
            {
                goto RECHECK;
                ////不需要进行评价，返回false
                //isEvaluating = false;
                //return isEvaluated;
            }
            if (info.TeacherStatus !=  Comm.Object.TeacherStatus.NeedEvaluate)
            {
                //不需要进行评价，返回false
                isEvaluating = false;
                return isEvaluated;
            }
            //根据呼叫ID查找课程信息
            Yoki.Comm.Response.CallLogForTopicInfo topInfo = Business.AccountController.Instance.CallLogForTopic(info.LogID);

            Business.AccountController.Instance.Data.ClassInfo = new Business.ClassInfo()
            {
                StuID = topInfo.StuID,
                StuAvatar = topInfo.StuAvatar,
                StuName = topInfo.StuName,
                StuAge = topInfo.StuAge,
                StuGender = topInfo.StuGender,
                StuGrade = "Grade 2",
                TopicID = topInfo.TopicID,
                TextbookID = topInfo.TextbookID,
                LogID = topInfo.LogID,
                TopicInfo = new Comm.Response.GetTopicDetailInfo()
                {
                    Title = topInfo.Title,
                    Links = topInfo.Links,
                }
            };

            Yoki.Business.StudentClassInfo scInfo = new Yoki.Business.StudentClassInfo()
            {
                HeaderImage = Business.AccountController.Instance.Data.ClassInfo.StuAvatarImage == null ? 
                                        ResourceHelper.EmptyHeader:
                                        Business.AccountController.Instance.Data.ClassInfo.StuAvatarImage,
                UserName = Business.AccountController.Instance.Data.ClassInfo.StuName,
                Gender = Business.AccountController.Instance.Data.ClassInfo.StuGender,
                Age = Business.AccountController.Instance.Data.ClassInfo.StuAge,
                Grade = Business.AccountController.Instance.Data.ClassInfo.StuGrade,
                ClassTitle = Business.AccountController.Instance.Data.ClassInfo.TopicInfo.Title,
            };

            isEvaluated = true;
            Evaluate(info.LogID, scInfo);
            goto RECHECK;
        }
        /// <summary>
        /// 加载评价界面进行学生评价操作
        /// </summary>
        /// <param name="logID"></param>
        /// <param name="scInfo"></param>
        public void Evaluate(long logID, Yoki.Business.StudentClassInfo scInfo)
        {
            Dictionary<int, int> scores = null;
            System.Threading.AutoResetEvent are = new System.Threading.AutoResetEvent(false);
            this.BeginInvoke((MethodInvoker)delegate
            {
                using (Dialog.frmEvaluateNotify frmEvaluate = new Dialog.frmEvaluateNotify(this, scInfo))
                {
                    //加载教师评价学生界面
                    LogUtil.WriteLogOperation(Comm.Object.LogType.Evaluate, "Loading teacher evaluation student interface.logID:" + logID);
                    Business.AccountController.Instance.Data.ClassInfo.OnAvatarChanged += (image) =>
                    {
                        frmEvaluate.HeaderImage = image == null ? ResourceHelper.EmptyHeader: image;
                    };
                    frmEvaluate.SuspendLayout();
                    frmEvaluate.StartPosition = FormStartPosition.CenterParent;
                    frmEvaluate.ClientSize = new System.Drawing.Size(600, 600);
                    frmEvaluate.ThemeType = Fink.Windows.Forms.ThemeType.Dark;
                    frmEvaluate.ResumeLayout(false);
                    DialogResult dr = frmEvaluate.ShowDialog(this);
                    scores = frmEvaluate.Scores;
                };
                                
                if (Business.AccountController.Instance.Data.ClassInfo != null)
                {
                    Business.AccountController.Instance.Data.ClassInfo.OnAvatarChanged = null;
                }

                are.Set();
            });
            are.WaitOne();
            //教师评价学生提交
            Business.AccountController.Instance.EvaluateStudent(logID, scores);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                TestFunction();
            }

        }

        //bool isShown = false;
        private void TestFunction()
        {
            //new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            //{

            //    FormUtil.ShowTooltip(this, isShown ? "123" : "12312321321312312313123123");
            //    isShown = !isShown;
            //})).Start();
            ToClassRoom();
            Yoki.IM.Manager.Instance.JoinRoom();

        }
    }
}

