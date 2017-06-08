using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Business.Message
{
    public class PushMsg : Yoki.Comm.Response.ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("data")]
        public ClassAccpetedInfo Data
        {
            get;
            set;
        }
    }
    public class ClassAccpetedInfo
    {
        [Newtonsoft.Json.JsonProperty("teacher_id")]
        public long TeacherID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("type")]
        public Yoki.Comm.Object.ClassType Type
        {
            get;
            set;
        }
    }


}
