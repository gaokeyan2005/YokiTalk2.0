using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Requeset
{
    public class EndClassRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("log_id")]
        public long LogID
        {
            get;
            set;
        }
    }
}
