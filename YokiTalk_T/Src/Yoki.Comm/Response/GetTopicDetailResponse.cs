using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{
    public class GetTopicDetailResponse : ResponseBase<GetTopicDetailInfo>
    {
        public override GetTopicDetailInfo Data
        {
            get;
            set;
        }
    }


    public class GetTopicDetailInfo
    {
        [Newtonsoft.Json.JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("links")]
        public string[] Links
        {
            get;
            set;
        }
    }
}
