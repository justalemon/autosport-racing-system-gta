using System;
using System.IO;

namespace ARS
{
    public static class Logger
    {
        public enum LogImportance { Info, Error, Fatal }
        public static void Log(LogImportance i, string text, bool forced = false)
        {
            if (ARS.DevSettingsFile != null && ARS.DevSettingsFile.GetValue<LogImportance>("GENERAL", "LogLevel", LogImportance.Info) > i && !forced) return;
            string log = "\n[" + DateTime.Now + "](" + i.ToString() + "): " + text;
            File.AppendAllText(@"scripts\ARS\Log.log", log);
        }
    }
}
