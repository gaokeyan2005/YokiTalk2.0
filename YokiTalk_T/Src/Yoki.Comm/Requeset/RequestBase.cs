using Yoki.Comm.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Requeset
{
    public class RequestBase
    {
        private static string _appToken = string.Empty;
        public static void SetToken(string token)
        {
            _appToken = token;
        }

        private DeviceType deviceType = DeviceType.PC;
        [Newtonsoft.Json.JsonProperty("device_type")]
        public DeviceType DeviceType
        {
            get { return this.deviceType; }
            set { this.deviceType = value; }
        }

        private string osVersion = System.Environment.OSVersion.VersionString;
        [Newtonsoft.Json.JsonProperty("os_version")]
        public string OSVersion
        {
            get { return this.osVersion; }
            set { this.osVersion = value; }
        }

        private string appVersion = "1.0.0";
        [Newtonsoft.Json.JsonProperty("app_version")]
        public string AppVersion
        {
            get { return this.appVersion; }
            set { this.appVersion = value; }
        }
        
        private string appPackage = "Yoki Tercher Client";
        [Newtonsoft.Json.JsonProperty("app_package")]
        public string AppPackage
        {
            get { return this.appPackage; }
            set { this.appPackage = value; }
        }

        private string channelID = "0";
        [Newtonsoft.Json.JsonProperty("channel_id")]
        public string ChannelID
        {
            get { return this.channelID; }
            set { this.channelID = value; }
        }

        private string token = RequestBase._appToken;
        [Newtonsoft.Json.JsonProperty("token")]
        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }
        
        private static string APP_KEY = "1740042b644a09fda16400ab8fd059f9";
        [Newtonsoft.Json.JsonProperty("sign")]
        public string Sign
        {
            get {
                //byte[] encrypt = ParamsHelper.MD5Encrypt(
                //    string.Format("{0}_{1}_{2}_{3}",
                //    this.DeviceType,
                //    this.OSVersion,
                //    this.Timestamp,
                //    APP_KEY));

                //return BitConverter.ToString(encrypt).Replace("-", string.Empty);
                string strValue = string.Format("{0}_{1}_{2}_{3}",(int)this.DeviceType, this.OSVersion, this.Timestamp, APP_KEY);
                return ParamsHelper.md5(strValue, 32);
            }
            //set { this.sign = value; }
        }

        private string lang = "en";
        [Newtonsoft.Json.JsonProperty("lang")]
        public string Lang
        {
            get { return this.lang; }
            set { this.lang = value; }
        }

        [Newtonsoft.Json.JsonProperty("timestamp")]
        public long Timestamp
        {
            get { return Convert.GetLinuxStamp(DateTime.Now); }
        }
    }
}
