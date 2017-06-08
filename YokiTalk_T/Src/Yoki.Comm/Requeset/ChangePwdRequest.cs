using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class ChangePwdRequest: RequestBase
    {
        [Newtonsoft.Json.JsonProperty("old_password")]
        public string Old
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("new_password")]
        public string New
        {
            get;
            set;
        }
    }
}
