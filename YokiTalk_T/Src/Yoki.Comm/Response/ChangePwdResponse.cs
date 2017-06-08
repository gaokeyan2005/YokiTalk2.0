using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{


    public class ChangePwdResponse : ResponseBase<ChangePwdResponseInfo>
    {
        public override ChangePwdResponseInfo Data
        {
            get;
            set;
        }
    }


    public class ChangePwdResponseInfo
    {
        [Newtonsoft.Json.JsonProperty("token")]
        public Guid Token
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("created_time")]
        public string CreatedTime
        {
            get;
            set;
        }
    }
}
