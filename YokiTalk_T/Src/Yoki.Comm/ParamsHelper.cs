using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm
{
    public struct CPU_INFO
    {
        public uint OemID;
        public uint PageSize;
        public uint MinimumApplicationAddress;
        public uint MaximumApplicationAddress;
        public uint ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public uint ProcessorLevel;
        public uint ProcessorRevision;
    }

    public enum ProcessorType
    {
        PROCESSOR_INTEL_386 = 386,
        PROCESSOR_INTEL_486 = 486,
        PROCESSOR_INTEL_PENTIUM = 586,
        PROCESSOR_INTEL_IA64 = 2200,
        PROCESSOR_AMD_X8664 = 8664
    }
    public class ParamsHelper
    {
        public static byte[] MD5Encrypt(string strText)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            return result;
        }

        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }

            return strEncrypt;
        }
        #endregion

        public static string Serializes(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);
        public static ProcessorType GetCpuType()
        {
            CPU_INFO cpuInfo = new CPU_INFO();
            GetSystemInfo(ref cpuInfo);
            return (ProcessorType)Enum.Parse(typeof(ProcessorType), cpuInfo.ProcessorType.ToString());
        }
    }
}
