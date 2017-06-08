using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class GetTopicDetailRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("topic_id")]
        public int TopicID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("textbook_id")]
        public int TextbookID
        {
            get;
            set;
        }
    }
}
