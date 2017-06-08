using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Response
{
    // base response for call method using 
    public abstract class LoginResponse<T>: ResponseBase<T>
    {
    }
    //split role  teacher and student
    public class TeacherLoginResponse: LoginResponse<TeacherLoginResponseInfo>
    {
        public override TeacherLoginResponseInfo Data
        {
            get;
            set;
        }
    }

    public class StudentLoginResponse : LoginResponse<StudentLoginResponseInfo>
    {
        public override StudentLoginResponseInfo Data
        {
            get;
            set;
        }
    }

    public class TeacherLoginResponseInfo : BaseLoginResponseInfo
    {

    }
    public class StudentLoginResponseInfo : BaseLoginResponseInfo
    {

    }



    public abstract class BaseLoginResponseInfo
    {
        [Newtonsoft.Json.JsonProperty("user_id")]
        public long UserID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("account")]
        public string Account
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("nick")]
        public string NickName
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("real_name")]
        public string RealName
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("status")]
        public Object.LoginStatus Status
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("is_forbidden")]
        public bool IsForbidden
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("is_actived")]
        public bool IsActived
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("avatar")]
        public string Avatar
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("area_id")]
        public int AreaID
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("address")]
        public string Address
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("area_id_last")]
        public int AreaIDLast
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("address_last")]
        public string AddressLash
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("tel")]
        public string Telphone
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("phone")]
        public string Cellphone
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("email")]
        public string EMail
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("vip_level")]
        public short VIPLevel
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("total_amount")]
        public decimal TotalAmount
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("amount")]
        public decimal Amount
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("login_ip")]
        public string LoginIP
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("login_time")]
        [Newtonsoft.Json.JsonConverter(typeof(Convert.LinuxDateTimeConverter))]
        public DateTime LoginTime
        {
            get;
            set;
        }
        
        [Newtonsoft.Json.JsonProperty("regist_ip")]
        public string RegistIP
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("regist_time")]
        [Newtonsoft.Json.JsonConverter(typeof(Convert.LinuxDateTimeConverter))]
        public DateTime RegistTime
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("gender")]
        public Core.Gender Gender
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("zipcode")]
        public string ZipCode
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("birthday")]
        [Newtonsoft.Json.JsonConverter(typeof(Convert.NullableDateTimeConverter))]
        public DateTime? Birthday
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("type")]
        public Object.RoleType RoleType
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_int0")]
        public int ExtendInteger0
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_int1")]
        public int ExtendInteger1
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_int2")]
        public int ExtendInteger2
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_int3")]
        public int ExtendInteger3
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_varchar0")]
        public string ExtendString0
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_varchar1")]
        public string ExtendString1
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_varchar2")]
        public string ExtendString2
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("extend_varchar3")]
        public string ExtendString3
        {
            get;
            set;
        } 
        
        [Newtonsoft.Json.JsonProperty("token")]
        public Guid Token
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("is_easemo")]
        public bool IsEasemo
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("is_netease")]
        public bool IsNetease
        {
            get;
            set;
        }
    }
    
    
}
