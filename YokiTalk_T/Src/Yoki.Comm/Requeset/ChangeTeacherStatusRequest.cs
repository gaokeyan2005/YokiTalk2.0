using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Requeset
{
    public class ChangeTeacherStatusRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("teacher_id")]
        public long TeacherID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("status")]
        public Yoki.Comm.Object.TeacherStatus TeacherStatus
        {
            get;
            set;
        }
    }
}
