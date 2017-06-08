using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.IM
{

    public class NetworkException : Exception
    {
        public override string Message
        {
            get
            {
                return "Network error.";
            }
        }
    }

    public class LoginNullException : Exception
    {
        public override string Message
        {
            get
            {
                return "UserName or password can't be null.";
            }
        }
    }
    public class LoginMismatchException : Exception
    {
        public override string Message
        {
            get
            {
                return "UserName or password mismatch.";
            }
        }
    }
    public class LoginTimeoutException : Exception
    {
        public override string Message
        {
            get
            {
                return "Login timeout.";
            }
        }
    }
}
