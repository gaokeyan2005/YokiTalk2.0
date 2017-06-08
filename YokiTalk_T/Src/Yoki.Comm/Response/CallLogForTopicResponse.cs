using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{


    public class CallLogForTopicResponse : ResponseBase<CallLogForTopicInfo>
    {
        public override CallLogForTopicInfo Data
        {
            get;
            set;
        }
    }


    public class CallLogForTopicInfo : GetTopicDetailInfo
    {
        [Newtonsoft.Json.JsonProperty("log_id")]
        public long LogID
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


        [Newtonsoft.Json.JsonProperty("id")]
        public int TopicID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("special_id")]
        public int TextbookID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("student_nick")]
        public string StuName
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

        [Newtonsoft.Json.JsonProperty("student_gender")]
        public Yoki.Core.Gender StuGender
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

        [Newtonsoft.Json.JsonProperty("class_time")]
        public int ClassTime
        {
            get;
            set;
        }

    }
}
