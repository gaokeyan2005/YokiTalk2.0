using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class CallAcceptRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("log_id")]
        public long LogID
        {
            get;
            set;
        }
    }
}
