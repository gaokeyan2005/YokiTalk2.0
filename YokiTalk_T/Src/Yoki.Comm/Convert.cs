using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Yoki.Comm
{
    internal class Convert
    {
        internal static  DateTime GetDateTime(long timeStamp)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(timeStamp);
        }
        
        internal static long GetLinuxStamp(DateTime time)
        {
            return (long)(time - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
        }


        public static String BytesToHex(byte[] src)
        {
            StringBuilder sb = new StringBuilder();
            if (src == null || src.Length <= 0)
            {
                return null;
            }
            for (int i = 0; i < src.Length; i++)
            {
                string a = System.Convert.ToString(src[i], 16);
                sb.Append(a);
            }
            return sb.ToString();
        }

        #region JSON CONVERT
        internal class LinuxDateTimeConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(long))
                {
                    return true;
                }
                return false;
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                long timeStamp = System.Convert.ToInt64(reader.Value);
                return GetDateTime(timeStamp);
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value.GetType() == typeof(DateTime))
                {
                    DateTime dt = System.Convert.ToDateTime(value);
                    long linuxTicks = GetLinuxStamp(dt);
                    writer.WriteValue(linuxTicks);
                }
            }
        }


        public class NullableDateTimeConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                DateTime? dt = null;

                if (!string.IsNullOrEmpty(reader.Value.ToString()))
                {
                    try
                    {
                        dt = System.Convert.ToDateTime(reader.Value.ToString());
                    }
                    catch (Exception)
                    {
                        //do nothing
                    }
                }
                return dt;
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteValue(string.Empty);
                }
                else
                {
                    DateTime dt = System.Convert.ToDateTime(value);
                    writer.WriteValue(dt.ToString("yyyy-MM-dd"));
                }
            }
        }


        public class NullableEnumConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                object obj = null;

                if (reader.Value != null && !string.IsNullOrEmpty(reader.Value.ToString()))
                {
                    try
                    {
                        obj = reader.Value;

                        return obj;
                    }
                    catch (Exception)
                    {
                        //do nothing
                    }
                }
                return obj;
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteValue(string.Empty);
                }
                else
                {
                    writer.WriteValue(string.Format("{0}", System.Convert.ToInt32(value)));
                }
            }
        }
        #endregion JSON CONVERT
    }
}
