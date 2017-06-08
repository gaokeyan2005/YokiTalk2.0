using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Yoki.Comm
{
    internal class HttpUtility
    {
        internal static string Request(string url, byte[] bs)
        {
            int retryCounter = 3;
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
        RETRY:

            try
            {
                request = (System.Net.HttpWebRequest)WebRequest.Create(url);
                //Post请求方式  
                request.Method = "POST";
                //内容类型  
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/json";
                //参数经过URL编码    
                Encoding myEncoding = Encoding.UTF8;
                //设置请求的ContentLength   
                request.ContentLength = bs.Length;
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(bs, 0, bs.Length);
                    writer.Flush();
                }

                response = (System.Net.HttpWebResponse)request.GetResponse();
                
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                if (retryCounter > 0)
                {
                    request.Abort();
                    retryCounter--;
                    goto RETRY;
                }
                else
                {
                    throw ex;
                }
            }

            if ((response == null || response.StatusCode != HttpStatusCode.OK) && retryCounter > 0)
            {
                request.Abort();
                retryCounter--;
                goto RETRY;
            }

            string rData = string.Empty;
            try
            {
                using (System.IO.Stream s = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                    {
                        rData = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rData;
        }
    }
}
