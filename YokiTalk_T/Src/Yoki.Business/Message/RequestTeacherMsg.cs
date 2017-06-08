using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Business.Message
{
    public class RequestTeacherMsg : Yoki.Comm.Response.ResponseBase<RequesetTeacherInfo>
    {
        public override RequesetTeacherInfo Data
        {
            get;
            set;
        }
    }

    public class RequesetTeacherInfo
    {
        [Newtonsoft.Json.JsonProperty("log_id")]
        public long LogID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("topic_id")]
        public int TopicID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("textbook_id")]
        public int TextbookID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("student_id")]
        public long StuID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("student_name")]
        public string StuName
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("student_avatar")]
        public string StuAvatar
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("student_gender")]
        public Yoki.Core.Gender StuGender
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("student_age")]
        public short StuAge
        {
            get;
            set;
        }
    }

}
