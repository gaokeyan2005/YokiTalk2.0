using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Requeset
{
    public class EvaluateStudentRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("call_log_id")]
        public long LogID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("scores")]
        public Dictionary<int, int> Scores
        {
            get;
            set;
        }
    }

}
