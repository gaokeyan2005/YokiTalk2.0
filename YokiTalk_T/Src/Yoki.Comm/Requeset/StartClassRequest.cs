using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Requeset
{
    public class StartClassRequest : RequestBase
    {
        [Newtonsoft.Json.JsonProperty("student_id")]
        public long StudentID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("topic_id")]
        public long TopicID
        {
            get;
            set;
        }
        
        [Newtonsoft.Json.JsonProperty("textbook_id")]
        public long TextbookID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("log_id")]
        public long LogID
        {
            get;
            set;
        }
    }
}
