using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.IM
{

    public delegate bool ReceivceVChatHandle(VChatHandleEventArgs args);



    public class LoginErrorEventArgs : EventArgs
    {
        public int Code;
        public string Msg;
    }

    
    public enum VChatType
    {
        Audio,
        Video
    }

    public class VChatHandleEventArgs
    {
        public long ChannelId
        {
            get;
            set;
        }
        public string Uid
        {
            get;
            set;
        }
        public VChatType Type
        {
            get;
            set;
        }
    }


    public enum VDevicesState
    {
        Running,
        Stoped
    }


    public struct IntptrInfo
    {
        public int Size;
        public IntPtr Ptr;
    }
    
    public struct VideoDataInfo
    {
        public long Time;
        public int Size;
        public int Width;
        public int Height;
        public IntPtr Data;
    }

    public struct AudioDataInfo
    {
        public long Time;
        public IntPtr Data;
        public int Size;
        public int Rate;
    }

    



}
