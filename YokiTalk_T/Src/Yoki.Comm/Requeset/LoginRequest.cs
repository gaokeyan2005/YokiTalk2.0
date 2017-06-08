using Yoki.Comm.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Requeset
{
    public class LoginRequest: RequestBase
    {
        [Newtonsoft.Json.JsonProperty("account")]
        public string Account
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("password")]
        public string Password
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("type")]
        public RoleType RoleType
        {
            get;
            set;
        }


    }
}
