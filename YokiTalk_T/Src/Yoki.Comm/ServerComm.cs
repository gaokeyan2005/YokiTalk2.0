using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Yoki.Comm.Object;

namespace Yoki.Comm
{
    public class ServerComm
    {
        public static T Request<T>(string url, object data)
        {
            string strData = Yoki.Comm.ParamsHelper.Serializes(data);
            
            string r = string.Empty;

            try
            {
                r = HttpUtility.Request(url, System.Text.Encoding.UTF8.GetBytes(strData));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //unicode to String 
            return Yoki.Comm.ParamsHelper.Deserialize<T>(r);
        }

        private static Rectangle[] _rLocations;
        /// <summary>
        /// 存储课件加载控件上一页下一页坐标位置
        /// </summary>
        public static Rectangle[] RLocations
        {
            get
            {
                return _rLocations;
            }

            set
            {
                _rLocations = value;
            }
        }
    }
}
