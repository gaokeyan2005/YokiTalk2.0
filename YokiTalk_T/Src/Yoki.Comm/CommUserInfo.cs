using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm
{
    public class CommUserInfo
    {
        private static string _UserId;
        private static string _Account;
        private static string _UserNickName;
        /// <summary>
        /// 用户ID（用于全局记录）
        /// </summary>
        public static string UserId
        {
            get
            {
                return _UserId;
            }

            set
            {
                _UserId = value;
            }
        }
        /// <summary>
        /// 用户帐号（用于全局记录）
        /// </summary>
        public static string Account
        {
            get
            {
                return _Account;
            }

            set
            {
                _Account = value;
            }
        }
        /// <summary>
        /// 用户昵称（用于全局记录）
        /// </summary>
        public static string UserNickName
        {
            get
            {
                return _UserNickName;
            }

            set
            {
                _UserNickName = value;
            }
        }
    }
}
