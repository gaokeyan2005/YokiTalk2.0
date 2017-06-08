using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{
    public class CheckStudentStatusResponse : ResponseBase<CheckStudentStatusResponseInfo>
    {
        public override CheckStudentStatusResponseInfo Data
        {
            get;
            set;
        }
    }
    
    public class CheckStudentStatusResponseInfo
    {
        [Newtonsoft.Json.JsonProperty("student_status")]
        public Object.StudentStatus Status
        {
            get;
            set;
        }
    }

}
