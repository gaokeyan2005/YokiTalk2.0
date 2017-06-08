/** @file NIMGlobalDef.cs
  * @brief NIM SDK提供的一些全局定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Runtime.InteropServices;

namespace NIM
{
    internal class NIMGlobal
    {
        /// <summary>
        ///     The NIM native DLL
        /// </summary> 
#if UNITY
#if UNITY_IOS
            public const string NIMNativeDLL = "__Internal";
#elif UNITY_ANDROID || UNITY_STANDALONE_LINUX
            public const string NIMNativeDLL = "nim";
#elif UNITY_STANDALONE_WIN
        public const string NIMNativeDLL = "nim";
#endif

#elif DEBUG
        public const string NIMNativeDLL = "nim.dll";
#else
        public const string NIMNativeDLL = "nim.dll";
#endif
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JsonTransportCb(string jsonParams, IntPtr userData);
    }
}