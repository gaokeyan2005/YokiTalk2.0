using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class CheckTeacherStatusRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("teacher_id")]
        public long TeacherID
        {
            get;
            set;
        }
    }
}
