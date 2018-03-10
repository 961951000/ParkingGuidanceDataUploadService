using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace ParkingGuidanceDataUploadService.Helper
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug($"{message}{Environment.NewLine}MemberName:{memberName}{Environment.NewLine}FilePath: {sourceFilePath}{Environment.NewLine}LineNumber:{sourceLineNumber}");
            }
        }

        public static void Debug(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug($"{message}{Environment.NewLine}成员名称：   {memberName}{Environment.NewLine}文件路径：   {sourceFilePath}{Environment.NewLine}代码行号：   {sourceLineNumber}");
            }
        }

        public static void Info(object message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void Info(object message, Exception exception)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message, exception);
            }
        }

        public static void Warn(object message)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message);
            }
        }

        public static void Warn(object message, Exception exception)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message, exception);
            }
        }

        public static void Error(object message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error($"{message}{Environment.NewLine}成员名称：   {memberName}{Environment.NewLine}文件路径：   {sourceFilePath}{Environment.NewLine}代码行号：   {sourceLineNumber}");
            }
        }

        public static void Error(object message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error($"{message}{Environment.NewLine}成员名称：   {memberName}{Environment.NewLine}文件路径：   {sourceFilePath}{Environment.NewLine}代码行号：   {sourceLineNumber}", exception);
            }
        }

        public static void Fatal(object message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsFatalEnabled)
            {
                Log.Fatal($"{message}{Environment.NewLine}成员名称：   {memberName}{Environment.NewLine}文件路径：   {sourceFilePath}{Environment.NewLine}代码行号：   {sourceLineNumber}");
            }
        }

        public static void Fatal(object message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Log.IsFatalEnabled)
            {
                Log.Fatal($"{message}{Environment.NewLine}成员名称：   {memberName}{Environment.NewLine}文件路径：   {sourceFilePath}{Environment.NewLine}代码行号：   {sourceLineNumber}", exception);
            }
        }
    }
}
