using System;
using System.Diagnostics;

namespace USC.GISResearchLab.Common.Core.Logging.Tracing
{
    public class TraceUtils
    {

        public static SourceLevels GetLevel(int level)
        {
            SourceLevels ret = SourceLevels.Off;
            switch (level)
            {
                case 0:
                    ret = SourceLevels.Off;
                    break;
                case 1:
                    ret = SourceLevels.Critical;
                    break;
                case 2:
                    ret = SourceLevels.Error;
                    break;
                case 3:
                    ret = SourceLevels.Warning;
                    break;
                case 4:
                    ret = SourceLevels.Information;
                    break;
                case 5:
                    ret = SourceLevels.Verbose;
                    break;
                case 6:
                    ret = SourceLevels.All;
                    break;
                default:
                    throw new Exception("Unexpected log level encountered: " + level);
            }
            return ret;

        }

    }
}
