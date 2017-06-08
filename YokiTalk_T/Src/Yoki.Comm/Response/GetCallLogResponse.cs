using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{


    public class GetCallLogResponse : ResponseBase<GetCallLogInfo>
    {
        public override GetCallLogInfo Data
        {
            get;
            set;
        }
    }


    public class GetCallLogInfo
    {
        [Newtonsoft.Json.JsonProperty("topic_id")]
        public int TopicID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("status")]
        public Object.ClassStatus Status
        {
            get;
            set;
        }
    }
}
