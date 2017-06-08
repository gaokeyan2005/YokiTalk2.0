using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Comm.Response
{


    public class GetEvaluateProtoResponse : ResponseBase<EvaluateProtoCategory[]>
    {
        public override EvaluateProtoCategory[] Data
        {
            get;
            set;
        }
    }
    

    public class EvaluateProtoCategory
    {
        [Newtonsoft.Json.JsonProperty("score_item_id")]
        public short ID
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("en")]
        public string Name
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("item_count")]
        public short Count
        {
            get;
            set;
        }
        [Newtonsoft.Json.JsonProperty("items")]
        public Dictionary<string, EvaluateContent> Items
        {
            get;
            set;
        }


    }
    //public class EvaluateProtoItems
    //{
    //    [Newtonsoft.Json.JsonProperty("1")]
    //    public EvaluateProtoItem One
    //    {
    //        get;
    //        set;
    //    }
    //    [Newtonsoft.Json.JsonProperty("2")]
    //    public EvaluateProtoItem Two
    //    {
    //        get;
    //        set;
    //    }
    //    [Newtonsoft.Json.JsonProperty("3")]
    //    public EvaluateProtoItem Three
    //    {
    //        get;
    //        set;
    //    }
    //    [Newtonsoft.Json.JsonProperty("4")]
    //    public EvaluateProtoItem Four
    //    {
    //        get;
    //        set;
    //    }
    //    [Newtonsoft.Json.JsonProperty("5")]
    //    public EvaluateProtoItem Five
    //    {
    //        get;
    //        set;
    //    }
    //}

   
    public class EvaluateContent
    {
        [Newtonsoft.Json.JsonProperty("en")]
        public string Text
        {
            get;
            set;
        }
    }



}
