using System;
using System.Diagnostics;
using System.Reflection;
using USC.GISResearchLab.Common.Utils.Files;

namespace USC.GISResearchLab.Common.Utils.Logs
{
    /// <summary>
    /// Summary description for LogUtils.
    /// </summary>
    public class LogUtils
    {

        public static void Log2File(string fileName, string msg)
        {
            string callingMethod = null;
            string callingAssembly = null;
            string callingAssemblyPath = null;

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            callingAssembly = stackFrame.GetFileName();

            if (callingAssembly != null)
            {
                callingAssemblyPath = FileUtils.GetFileName(callingAssembly);
            }

            if (methodBase != null)
            {
                callingMethod = methodBase.Name;
            }

            Log2File(fileName, msg, 0, 0, callingMethod, callingAssemblyPath);
        }

        public static void Log2File(string fileName, string msg, int logLevel)
        {
            string callingMethod = null;
            string callingAssembly = null;
            string callingAssemblyPath = null;

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            callingAssembly = stackFrame.GetFileName();
            callingAssemblyPath = FileUtils.GetFileName(callingAssembly);
            callingMethod = methodBase.Name;

            Log2File(fileName, msg, 0, 0, callingMethod, callingAssemblyPath);
        }

        public static void Log2File(string fileName, string msg, int logLevel, int minLevel, string callingMethod, string callingAssembly)
        {
            if (logLevel >= minLevel)
            {
                FileUtils.AppendTextFile(fileName, DateTime.Now + ": " + callingAssembly + ": " + callingMethod + " - " + msg);
            }
        }

        public static string GetCurrentMethodName()
        {
            string ret = null;
            try
            {

                StackFrame stackFrame = new StackFrame();
                MethodBase methodBase = stackFrame.GetMethod();
                ret = methodBase.Name;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting current method name", e);
            }
            return ret;
        }

        public static string GetCallingMethodName(int levelUp)
        {
            string ret = null;
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = null;
                if (levelUp <= stackTrace.FrameCount)
                {
                    stackFrame = stackTrace.GetFrame(levelUp);
                }
                else
                {
                    stackFrame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
                }
                MethodBase methodBase = stackFrame.GetMethod();
                ret = methodBase.Name;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting calling method name", e);
            }
            return ret;
        }

        public static string GetCurrentAssemblyName()
        {
            string ret = null;
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);
                ret = stackFrame.GetFileName();
            }
            catch (Exception e)
            {
                throw new Exception("Error getting current dll name", e);
            }
            return ret;
        }

        public static string GetCallingAssemblyName(int levelUp)
        {
            string ret = null;
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = null;
                if (levelUp <= stackTrace.FrameCount)
                {
                    stackFrame = stackTrace.GetFrame(levelUp);
                }
                else
                {
                    stackFrame = stackTrace.GetFrame(stackTrace.FrameCount - 1);
                }

                string callingDllPath = stackFrame.GetFileName();
                ret = FileUtils.GetFileName(callingDllPath);
            }
            catch (Exception e)
            {
                throw new Exception("Error getting calling dll name", e);
            }
            return ret;
        }
    }
}
