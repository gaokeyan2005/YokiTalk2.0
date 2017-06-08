using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Response
{
    public abstract class ResponseBase<T>: ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("data")]
        public abstract T Data
        {
            get;
            set;
        }
    }
    
    public class ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("code")]
        public int Code
        {

            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("message")]
        public string Message
        {
            get;
            set;
        }
    }

}
