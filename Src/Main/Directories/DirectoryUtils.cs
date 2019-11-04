using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using USC.GISResearchLab.Common.Diagnostics.TraceEvents;

namespace USC.GISResearchLab.Common.Utils.Directories
{
    /// <summary>
    /// Summary description for DirectoryUtils.
    /// </summary>
    public class DirectoryUtilsTraceable
    {
        public DirectoryUtilsTraceable()
        {
        }


        public static bool CopyDirectoryTraceable(string sourceDirectory, string targetDirectory, TraceSource traceSource, bool ignoreError, BackgroundWorker backgroundWorker)
        {
            bool ret = false;
            try
            {
                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                ret = CopyAllTraceable(diSource, diTarget, traceSource, ignoreError, backgroundWorker);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred Copying Directory", e);
            }
            return ret;
        }

        public static bool CopyAllTraceable(DirectoryInfo source, DirectoryInfo target, TraceSource traceSource, bool ignoreError, BackgroundWorker backgroundWorker)
        {
            bool ret = false;
            try
            {
                if (backgroundWorker != null && backgroundWorker.CancellationPending)
                {
                    if (traceSource != null)
                        traceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Cancelling, "Cancelling");
                    return false;
                }
                else
                {
                    if (!Directory.Exists(target.FullName))
                    {
                        Directory.CreateDirectory(target.FullName);
                    }

                    foreach (FileInfo fi in source.GetFiles())
                    {
                        if (backgroundWorker != null && backgroundWorker.CancellationPending)
                        {
                            if (traceSource != null)
                                traceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Cancelling, "Cancelling");
                            return false;
                        }
                        else
                        {
                            if (traceSource != null)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat(@"Copying {0}\{1}", target.FullName, fi.Name);
                                if (traceSource != null)
                                    traceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Completing, sb.ToString());
                            }
                            fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                        }
                    }

                    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                    {
                        if (backgroundWorker != null && backgroundWorker.CancellationPending)
                        {
                            if (traceSource != null)
                                traceSource.TraceEvent(TraceEventType.Information, (int)ProcessEvents.Cancelling, "Cancelling");
                            return false;
                        }
                        else
                        {
                            DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                            ret = CopyAllTraceable(diSourceSubDir, nextTargetSubDir, traceSource, ignoreError, backgroundWorker);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (traceSource != null)
                    traceSource.TraceEvent(TraceEventType.Error, (int)ExceptionEvents.ExceptionOccurred, e.Message);
                if (!ignoreError)
                {
                    throw new Exception("An error occurred in CopyAll", e);
                }
            }
            return ret;
        }

    }


}
