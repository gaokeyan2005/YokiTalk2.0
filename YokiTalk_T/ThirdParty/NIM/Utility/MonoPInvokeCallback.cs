using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM
{
#if !UNITY
    /// <summary>
    /// Unity IOS 需要使用MonoPInvokeCallbackAttribute 标记P/Invoke 回调函数，为了统一 pc 端定义一个空属性作为标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type)
        {

        }
    }
#endif
}
