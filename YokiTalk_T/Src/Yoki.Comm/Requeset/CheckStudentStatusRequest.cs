using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class CheckStudentStatusRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("student_id")]
        public long StuID
        {
            get;
            set;
        }
    }
}
