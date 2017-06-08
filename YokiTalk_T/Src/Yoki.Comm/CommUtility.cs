using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm
{
    public class CommUtility
    {
        public static T Login<T>(string serverPath, string account, string password)
        {
            Object.RoleType? roleType = null;

            if (typeof(T) == typeof(Response.TeacherLoginResponse))
            {
                roleType = Object.RoleType.Teacher;
            }
            else if (typeof(T) == typeof(Response.StudentLoginResponse))
            {
                roleType = Object.RoleType.Student;
            }

            T ro = default(T);
            if (roleType != null)
            {
                //login Yoki
                Requeset.LoginRequest lreq = new Requeset.LoginRequest();
                lreq.Account = account;
                lreq.Password = password;
                lreq.RoleType = roleType.GetValueOrDefault();
                
                string url = string.Format(serverPath + @"/{0}/{1}", "user", "login");
                try
                {
                    ro = Yoki.Comm.ServerComm.Request<T>(url, lreq);

                    if (typeof(T) == typeof(Response.TeacherLoginResponse))
                    {
                        Response.TeacherLoginResponse roTeacher = (ro as Response.TeacherLoginResponse);
                        if (roTeacher != null && roTeacher.Data != null)
                        {
                            Requeset.RequestBase.SetToken(roTeacher.Data.Token.ToString().Replace("-", string.Empty));
                        }
                    }
                    else if (typeof(T) == typeof(Response.StudentLoginResponse))
                    {
                        Response.StudentLoginResponse roStudent = (ro as Response.StudentLoginResponse);
                        if (roStudent != null && roStudent.Data != null)
                        {
                            Requeset.RequestBase.SetToken(roStudent.Data.Token.ToString().Replace("-", string.Empty));
                        }
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ro;
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="serverPath"></param>
        /// <returns></returns>
        public static Response.ResponseBase Logout(string serverPath)
        {
            Response.ResponseBase ro = null;
            Requeset.RequestBase rq = new Requeset.RequestBase();
            string url = string.Format(serverPath + @"/{0}/{1}", "user", "logout");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.GetEvaluateProtoResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        public static Response.ResponseBase ChangePassword(string serverPath, string newpwd, string oldpwd)
        {
            Response.ResponseBase ro = null;
            Requeset.ChangePwdRequest rq = new Requeset.ChangePwdRequest()
            {
                New = newpwd,
                Old = oldpwd,
            };
            string url = string.Format(serverPath + @"/{0}/{1}", "user", "changepwd");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        /// <summary>
        /// 教师评价学生项目列表
        /// </summary>
        /// <param name="serverPath"></param>
        /// <returns></returns>
        public static Response.GetEvaluateProtoResponse GetEvaluateProto(string serverPath)
        {
            Response.GetEvaluateProtoResponse ro = null;
            Requeset.RequestBase rq = new Requeset.RequestBase();
            string url = string.Format(serverPath + @"/{0}/{1}/{2}", "lesson", "comment", "items");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.GetEvaluateProtoResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// api修改教师状态
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="teacherID"></param>
        /// <param name="teacherStatus"></param>
        /// <returns></returns>
        public static Response.ResponseBase ChangeTeacherStatus(string serverPath, long teacherID, Yoki.Comm.Object.TeacherStatus teacherStatus)
        {
            Response.ResponseBase ro = null;
            Requeset.ChangeTeacherStatusRequest rq = new Requeset.ChangeTeacherStatusRequest()
            {
                TeacherID = teacherID,
                TeacherStatus = teacherStatus,
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "teacher", "changestatus");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }


        /// <summary>
        /// 检查学生状态
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="stuID"></param>
        /// <returns></returns>
        public static Response.CheckStudentStatusResponse CheckStudentStatus(string serverPath, long stuID)
        {
            Response.CheckStudentStatusResponse ro = null;
            Requeset.CheckStudentStatusRequest rq = new Requeset.CheckStudentStatusRequest()
            {
                StuID = stuID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "student", "checkstatus");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.CheckStudentStatusResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// 检测老师状态
        //status 0 空闲 1 准备上课，2 上课中，3暂停
        //class_status  0 收到请求 1 获取课程成功 ,2 分配课程成功（权限够），3 成功分配教师，4成功通知云信，5教师收到通知，6老师接受，7老师拒绝 8 开始上课 9学生取消呼叫 10 下课
        //call_log_id  呼叫记录ID
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public static Response.CheckTeacherStatusResponse CheckTeacherStatus(string serverPath, long teacherID)
        {
            Response.CheckTeacherStatusResponse ro = null;
            Requeset.CheckTeacherStatusRequest rq = new Requeset.CheckTeacherStatusRequest()
            {
                TeacherID = teacherID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "teacher", "checkstatus");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.CheckTeacherStatusResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// 呼叫通知到教师
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static Response.ResponseBase CallNotice(string serverPath, long logID)
        {
            Response.ResponseBase ro = null;
            Requeset.CallNoticeRequest rq = new Requeset.CallNoticeRequest()
            {
                LogID = logID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "call", "notice");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        /// <summary>
        /// 老师接受呼叫
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static Response.ResponseBase CallAccept(string serverPath, long logID)
        {
            Response.ResponseBase ro = null;
            Requeset.CallAcceptRequest rq = new Requeset.CallAcceptRequest()
            {
                LogID = logID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "call", "accept");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        /// <summary>
        /// 老师拒绝
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID"></param>
        /// <returns></returns>
        public static Response.ResponseBase CallRefuse(string serverPath, long logID)
        {
            Response.ResponseBase ro = null;
            Requeset.CallRefuseRequest rq = new Requeset.CallRefuseRequest()
            {
                LogID = logID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "call", "refuse");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// 获取课程详情
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="topicID">课程ID</param>
        /// <param name="textbookID">课程ID</param>
        /// <returns></returns>
        public static Response.GetTopicDetailResponse GetTopicDetail(string serverPath, int topicID, int textbookID)
        {
            Response.GetTopicDetailResponse ro = null;
            Requeset.GetTopicDetailRequest rq = new Requeset.GetTopicDetailRequest()
            {
                TopicID = topicID,
                TextbookID = textbookID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "student", "topic");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.GetTopicDetailResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        /// <summary>
        /// 根据呼叫ID查找课程信息
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID">呼叫ID</param>
        /// <returns></returns>
        public static Response.CallLogForTopicResponse CallLogForTopic(string serverPath, long logID)
        {
            Response.CallLogForTopicResponse ro = null;
            Requeset.CallLogForTopicRequest rq = new Requeset.CallLogForTopicRequest()
            {
                LogID = logID,
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "calllog", "topic");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.CallLogForTopicResponse>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        /// <summary>
        /// 开始上课
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="studentID">学生ID</param>
        /// <param name="topicID">课程ID</param>
        /// <param name="textbookID">教材ID</param>
        /// <param name="logID">呼叫ID</param>
        /// <returns></returns>
        public static Response.ResponseBase StartClass(string serverPath, long studentID, int topicID, int textbookID, long logID)
        {
            Response.ResponseBase ro = null;
            Requeset.StartClassRequest rq = new Requeset.StartClassRequest()
            {
                StudentID = studentID,
                TopicID = topicID,
                TextbookID = textbookID,
                LogID = logID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "teacher", "startclass");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// 下课接口
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID">呼叫纪录ID</param>
        /// <returns></returns>
        public static Response.ResponseBase EndClass(string serverPath, long logID)
        {
            Response.ResponseBase ro = null;
            Requeset.EndClassRequest rq = new Requeset.EndClassRequest()
            {
                LogID = logID
            };

            string url = string.Format(serverPath + @"/{0}/{1}", "teacher", "endclass");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }
        /// <summary>
        /// 教师评价学生
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="logID"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public static Response.ResponseBase EvaluateStudent(string serverPath, long logID, Dictionary<int, int> scores)
        {
            Response.ResponseBase ro = null;
            Requeset.EvaluateStudentRequest rq = new Requeset.EvaluateStudentRequest()
            {
                LogID = logID,
                Scores = scores,
            };

            string url = string.Format(serverPath + @"/{0}/{1}/{2}", "lesson", "comment", "toStudent");
            try
            {
                ro = Yoki.Comm.ServerComm.Request<Response.ResponseBase>(url, rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ro;
        }

        public static System.Drawing.Image Download(string uri, System.Drawing.Image holder = null)
        {
            if (string.IsNullOrEmpty(uri) || !Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                return holder;
            }

            System.Drawing.Image bitmap = null;
            var webClient = new System.Net.WebClient();
            try
            {
                byte[] data = webClient.DownloadData(uri);
                using (var stream = new System.IO.MemoryStream(data))
                {
                    bitmap = System.Drawing.Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
            }

            return bitmap;
        }

        public static void Download(string uri, Action<System.Drawing.Image> handle, System.Drawing.Image holder = null)
        {
            if (string.IsNullOrEmpty(uri) || !Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                handle(holder);
            }

            System.Threading.ThreadPool.QueueUserWorkItem(
                delegate
                {
                    System.Drawing.Image bitmap = null;
                    var webClient = new System.Net.WebClient();
                    try
                    {
                        byte[] data = webClient.DownloadData(uri);
                        using (var stream = new System.IO.MemoryStream(data))
                        {
                            bitmap = System.Drawing.Image.FromStream(stream);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    
                    handle(bitmap);
                });
        }

    }
}
