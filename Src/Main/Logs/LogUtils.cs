using System.IO;
using USC.GISResearchLab.Common.Utils.Files;

namespace USC.GISResearchLab.Common.Logs
{
    public class LogUtils
    {
        public static string GetLogFileForDll(string dllFilePath)
        {
            return FileUtils.GetFileNameWithoutExtension(dllFilePath) + ".log";
        }

        public static string GetLogFileForApplication(string applicationPath)
        {
            return Path.Combine(applicationPath, "log.txt");
        }
    }
}
