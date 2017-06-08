using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoki.Comm;

namespace Yoki.View.Business
{
    public class AccountController
    {
        //private static string _serverPath = "http://api.chaojiwaijiao.com/v1";//正式环境
        //private static string _serverPath = "http://api-dev.yk.qxeden.com/v1";//测试环境
        //private static string _serverPath = "http://api-new.chaojiwaijiao.com/v1";
        private static string _serverPath = ConfigurationManager.AppSettings["apiaddress"].ToString();
        /// <summary>
        /// 根据接口地址判断程序运行环境
        /// </summary>
        /// <returns>返回空表示正式环境</returns>
        public static string GetEnvironment()
        {
            if (_serverPath.IndexOf("api.chaojiwaijiao") > -1)
            {
                return "";
            }
            else
            {
                if (_serverPath.IndexOf("api-new.chaojiwaijiao") > -1)
                {
                    return "[新服务器环境]";
                }
                else
                {
                    return "[Testing environment]";
                }
            }
        }
        private AccountController()
        {

        }

        private static AccountController _instance = null;
        public static AccountController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountController();
                }
                return _instance;
            }
        }

        private AccountInfo data = null;
        public AccountInfo Data
        {
            get
            {
                if (this.data == null)
                {
                    this.data = new AccountInfo();
                }
                return this.data;
            }
        }

        public void ClearData()
        {
            this.Data.UserID = -1;
        }

        public KeyValuePair<int, string> Login(string account, string password)
        {
            //通过登录接口获取用户信息
            Yoki.Comm.Response.TeacherLoginResponse ro = Yoki.Comm.CommUtility.Login<Yoki.Comm.Response.TeacherLoginResponse>(_serverPath, account, password);
            KeyValuePair<int, string> result = new KeyValuePair<int, string>(ro.Code, ro.Message);
            if (ro != null && ro.Code == 0 && ro.Data != null)
            {
                this.Data.UserID = ro.Data.UserID;
                this.Data.NickName = ro.Data.NickName;
                if (!string.IsNullOrEmpty(ro.Data.Avatar) && Uri.IsWellFormedUriString(ro.Data.Avatar, UriKind.Absolute))
                {
                    Yoki.Comm.CommUtility.Download(ro.Data.Avatar, (img) =>
                    {
                        this.Data.Avatar = img;
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public Comm.Response.ResponseBase Logout()
        {
            try
            {
                //退出登录
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.Logout(_serverPath);
                return ro;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Comm.Response.ResponseBase ChangePassword(string newpwd, string oldpwd)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.ChangePassword(_serverPath, newpwd, oldpwd);
                return ro;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Yoki.Comm.Response.EvaluateProtoCategory[] GetEvaluateProto()
        {
            try
            {
                Yoki.Comm.Response.GetEvaluateProtoResponse ro = Yoki.Comm.CommUtility.GetEvaluateProto(_serverPath);

                return ro == null ? null : ro.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// api修改教师状态
        /// </summary>
        /// <param name="form"></param>
        /// <param name="teacherID">老师id</param>
        /// <param name="teacherStatus">教师状态:status 0 空闲 1 准备上课，2 上课中，3暂停</param>
        /// <returns></returns>
        public Comm.Response.ResponseBase ChangeTeacherStatus(System.Windows.Forms.Form form, long teacherID, Yoki.Comm.Object.TeacherStatus teacherStatus)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.ChangeTeacherStatus(_serverPath, teacherID, teacherStatus);

                return ro;
            }
            catch (Exception)
            {
                FormUtil.ShowTooltip(form, "Change teacher status failed.");
                return null;
            }
        }

        /// <summary>
        /// 检查学生状态
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <returns></returns>
        public Yoki.Comm.Object.StudentStatus CheckStudentStatus(long stuID)
        {
            try
            {
                Yoki.Comm.Response.CheckStudentStatusResponse ro = Yoki.Comm.CommUtility.CheckStudentStatus(_serverPath, stuID);

                if (ro != null && ro.Code == 0 && ro.Data != null)
                {
                    return ro.Data.Status;
                }
                return Yoki.Comm.Object.StudentStatus.Unknown;
            }
            catch (Exception)
            {
                return Yoki.Comm.Object.StudentStatus.Unknown;
            }
        }
        /// <summary>
        /// 检测老师状态
        //status 0 空闲 1 准备上课，2 上课中，3暂停
        //class_status  0 收到请求 1 获取课程成功 ,2 分配课程成功（权限够），3 成功分配教师，4成功通知云信，5教师收到通知，6老师接受，7老师拒绝 8 开始上课 9学生取消呼叫 10 下课
        //call_log_id  呼叫记录ID
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public Yoki.Comm.Response.CheckTeacherStatusInfo CheckTeacherStatus(long teacherID)
        {
            try
            {
                Yoki.Comm.Response.CheckTeacherStatusResponse ro = Yoki.Comm.CommUtility.CheckTeacherStatus(_serverPath, teacherID);

                return ro == null ? null : ro.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 呼叫通知到教师
        /// </summary>
        /// <param name="logID">呼叫id</param>
        public void CallNotice(long logID)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.CallNotice(_serverPath, logID);
            }
            catch (Exception)
            {
            }
        }

        public void CallAccept(long logID)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.CallAccept(_serverPath, logID);
            }
            catch (Exception)
            {
            }
        }

        public void CallRefuse(long logID)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.CallRefuse(_serverPath, logID);
            }
            catch (Exception)
            {
            }
        }

        public Yoki.Comm.Response.GetTopicDetailInfo GetTopicDetail(int topicID, int textbookID)
        {
            try
            {
                Yoki.Comm.Response.GetTopicDetailResponse ro = Yoki.Comm.CommUtility.GetTopicDetail(_serverPath, topicID, textbookID);
                return ro.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据呼叫ID查找课程信息
        /// </summary>
        /// <param name="logID">呼叫ID</param>
        /// <returns></returns>
        public Yoki.Comm.Response.CallLogForTopicInfo CallLogForTopic(long logID)
        {
            try
            {
                Yoki.Comm.Response.CallLogForTopicResponse ro = Yoki.Comm.CommUtility.CallLogForTopic(_serverPath, logID);
                return ro == null ? null : ro.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void StartClass(System.Windows.Forms.Form form, long studentID, int topicID, int specialID, long logID)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.StartClass(_serverPath, studentID, topicID, specialID, logID);
            }
            catch (Exception)
            {
                FormUtil.ShowTooltip(form, "Start class failed.");
            }
        }
        /// <summary>
        /// 下课接口
        /// </summary>
        /// <param name="form"></param>
        /// <param name="logID">学生ID</param>
        public void EndClass(System.Windows.Forms.Form form, long logID)
        {
            try
            {
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.EndClass(_serverPath, logID);
            }
            catch (Exception)
            {
                FormUtil.ShowTooltip(form, "End class failed.");
            }
        }
        /// <summary>
        /// 教师评价学生
        /// </summary>
        /// <param name="logID">呼叫ID</param>
        /// <param name="scores">评价分数集合</param>
        public void EvaluateStudent(long logID, Dictionary<int, int> scores)
        {
            try
            {
                string strScores = "";
                //遍历键/值
                foreach (KeyValuePair<int, int> key in scores)
                {
                    string strKey = key.Key.ToString();//key
                    string strValue=key.Value.ToString();//value
                    strScores += strKey + "/" + strValue + ",";
                }
                strScores = "{" + strScores.Substring(0, strScores.Length - 1) + "}";
                LogUtil.WriteLogOperation(Comm.Object.LogType.Evaluate, "Teachers evaluate students. logID:" + logID+ ",scores:"+ strScores);
                Yoki.Comm.Response.ResponseBase ro = Yoki.Comm.CommUtility.EvaluateStudent(_serverPath, logID, scores);
            }
            catch (Exception)
            {
            }
        }
    }

    public class AccountInfo
    {
        public long UserID
        {
            get;
            set;
        }

        public string NickName
        {
            get;
            set;
        }

        public Yoki.Core.DataEventHandle<Image> OnAvatarChanged;
        private Image avatar = null;
        public Image Avatar
        {
            get
            {
                return this.avatar;
            }
            set
            {
                this.avatar = value;
                if (this.OnAvatarChanged != null)
                {
                    this.OnAvatarChanged(this.avatar);
                }
            }
        }

        public Yoki.Core.DataEventHandle<Yoki.Comm.Response.EvaluateProtoCategory[]> OnEvaluateProtoChanged;
        private Yoki.Comm.Response.EvaluateProtoCategory[] evaluateProto = null;
        public Yoki.Comm.Response.EvaluateProtoCategory[] EvaluateProto
        {
            get
            {
                return this.evaluateProto;
            }
            set
            {
                this.evaluateProto = value;
                if (this.OnEvaluateProtoChanged != null)
                {
                    this.OnEvaluateProtoChanged(this.evaluateProto);
                }
            }
        }

        public ClassInfo ClassInfo
        {
            get;
            set;
        }

    }

    public class ClassInfo
    {
        public Yoki.Core.DataEventHandle<System.Drawing.Image> OnAvatarChanged;
        public long LogID
        {
            get;
            set;
        }

        public int TopicID
        {
            get;
            set;
        }
        public int TextbookID
        {
            get;
            set;
        }

        public long StuID
        {
            get;
            set;
        }

        private string stuAvatar = string.Empty;
        public string StuAvatar
        {
            get
            {
                return this.stuAvatar;
            }
            set
            {
                if (this.stuAvatar != value)
                {
                    this.stuAvatar = value;
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o1) =>
                    {
                        Yoki.Comm.CommUtility.Download(this.stuAvatar, (image) =>
                        {
                            this.StuAvatarImage = image;
                        });
                    }), ResourceHelper.EmptyHeader);
                }
            }
        }

        private System.Drawing.Image stuAvatarImage = ResourceHelper.EmptyHeader;
        public System.Drawing.Image StuAvatarImage
        {
            get
            {
                return this.stuAvatarImage;
            }
            set
            {
                this.stuAvatarImage = value;
                if (this.OnAvatarChanged != null)
                {
                    this.OnAvatarChanged(this.stuAvatarImage);
                }
            }
        }

        public string StuName
        {
            get;
            set;
        }

        public short StuAge
        {
            get;
            set;
        }

        public Yoki.Core.Gender StuGender
        {
            get;
            set;
        }

        public string StuGrade
        {
            get;
            set;
        }


        public Yoki.Comm.Response.GetTopicDetailInfo TopicInfo
        {
            get;
            set;
        }

        private DateTime startClassTime = DateTime.MinValue;
        public DateTime StartClassTime
        {
            get
            {
                return this.startClassTime;
            }
            set
            {
                this.startClassTime = value;
            }
        }
    }
}
