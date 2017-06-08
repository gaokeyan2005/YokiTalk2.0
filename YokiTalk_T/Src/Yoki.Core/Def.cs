using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Core
{
    public delegate void DataEventHandle<TEventArgs>(TEventArgs e);
    
    public delegate void MessageEventHandle(string msg);

    public delegate void NoneArgsHandle();


    public enum Gender
    {
        UnKnown = 0,
        Male = 1,
        Female = 2,
        Not_Filled = 3,
    }
}
