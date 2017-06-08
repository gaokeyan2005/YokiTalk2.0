using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yoki.Comm;

namespace Yoki.View
{
    static class Program
    {
        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        private const int SW_MAXIMIZE = 3;//最大化
        private const int SW_MINIMIZE = 6;//最小化
        private const int SW_RESTORE = 9;//还原
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //LogUtil.WriteLogOperation(Comm.Object.LogType.Save, "aa");
            //Application.Run(new frmATest());

            #region 程序开始运行
            Process instance = RunningInstance();
            if (instance == null)
            {
                Yoki.Controls.VideoBox.NoVideoImage = ResourceHelper.NoVideo;

                frmLogin frmLogin = new frmLogin();

                frmLogin.ShowDialog();

                if (frmLogin.IsLoginSuccessed)
                {
                    //成功登录日志记录
                    LogUtil.WriteLogOperation(Comm.Object.LogType.Login, "Successful login!");
                    Application.Run(new frmMain());
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }

            #endregion 程序开始运行
        }
        /// <summary>
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// </summary>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 显示已运行的程序。
        /// </summary>
        public static void HandleRunningInstance(Process instance)
        {
            MessageBox.Show("YokiTalk is already running! ", "Operation tips", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }
        /// <summary>
        ///处理UI线程异常的错误记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str = "";
            //An unhandled exception has occurred(出现应用程序未处理的异常)
            string strDateInfo = "An unhandled exception has occurred:" + DateTime.Now.ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "Exception type:{0}\r\nException message:{1}\r\nAbnormal information:{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                //Application thread error应用程序线程错误
                str = string.Format("Application thread error:{0}", e);
            }
            LogUtil.WriteLogError(error.Source, str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 处理非UI线程异常的错误记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            //An unhandled exception has occurred(出现应用程序未处理的异常)
            string strDateInfo = "An unhandled exception has occurred：" + DateTime.Now.ToString() + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\rStack information:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }
            LogUtil.WriteLogError(error.Source, str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
