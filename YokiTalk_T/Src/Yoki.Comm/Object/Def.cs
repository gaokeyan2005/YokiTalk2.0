using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yoki.Comm.Object
{
    public enum DeviceType
    {
        Unknown = 0,
        IOS = 1,
        Android = 2,
        PC = 4,
        TV = 8,
    }

    public enum RoleType
    {
        Unknown = 0,
        Student = 1,
        Carers = 2,
        Teacher = 3,
        Administrator = 4,
    }

    public enum LoginStatus
    {
        Offline = 0,
        Online = 1
    }

    public enum StudentStatus
    {
        Unknown = -1,
        Idle = 0,
        WaitingForClass = 1,
        InClass = 2
    }

    public enum TeacherStatus
    {
        Unknown = -1,//未知
        Idle = 0,//空闲
        WaitingForClass = 1,//准备上课
        InClass = 2,//上课中
        NeedEvaluate = 3,//需要评价
        Pause = 4,//暂停
        Locked = 5,//锁定
    }
    
    public enum ClassStatus
    {
        Unknown = -1,
        CallRequest = 0,
        GotCourse = 1,
        AllotCourse = 2,
        AllotTeacher = 3,
        SendToNetease = 4,
        TeacherGotMsg = 5,
        TeacherAccept = 6,
        TeacherRefuse = 7,
        StartClass = 8,
        StudentCallCancel = 9,
        EndClass = 10,
    }


    public enum IdentityCardType
    {
        Idcard = 0,
    }

    public enum DegreeType
    {
        Univercity = 0,
    }



    public enum ClassType
    {
        Video = 0,
    }

    /// <summary>
    /// 用于写入操作日志的操作类型枚举
    /// </summary>
    public enum LogType
    {
        Login=0,//登录
        Select = 1,//查询
        Insert = 2,//写入
        Update = 3,//更新
        Delete=4,//删除
        Save = 5,//保存
        Close=6,//关闭或退出
        Answer=7,//接听或应答
        End=8,//结束课程
        Net=9,//网络
        Evaluate=10//评价

    }
}
