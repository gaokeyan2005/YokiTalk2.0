using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{
    public class CheckTeacherStatusResponse : ResponseBase<CheckTeacherStatusInfo>
    {
        public override CheckTeacherStatusInfo Data
        {
            get;
            set;
        }
    }
    
    public class CheckTeacherStatusInfo
    {
        [Newtonsoft.Json.JsonProperty("status")]
        public Object.TeacherStatus TeacherStatus
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("class_status")]
        public Object.ClassStatus ClassStatus
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("call_log_id")]
        public long LogID
        {
            get;
            set;
        }
    }

}
