using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightBlueFox.Util.Logging
{
    public class Logger
    {
        private static List<BaseLogWriter> Writers = new List<BaseLogWriter>();

        /// <summary>
        /// Logs a message to the curresponding log writers.
        /// </summary>
        /// <param name="lvl">The log level of the message.</param>
        /// <param name="format">A format string.</param>
        /// <param name="args">Arguments for the format string.</param>
        /// <returns></returns>
        public static void Log(LogLevel lvl, string format, params object[] args)
        {
            Log(lvl, string.Format(format, args));
        }
        /// <summary>
        /// Logs a message to the curresponding log writers.
        /// </summary>
        /// <param name="lvl">The log level of the message.</param>
        /// <param name="format">The message to print.</param>
        public static void Log(LogLevel lvl, string str)
        {
                foreach (var w in Writers)
                {
                    if (w.LoggedLevels.Contains(lvl))
                        w.QueueOutput(lvl, str);
                }
        }



        /// <summary>
        /// Registers a new writer.
        /// </summary>
        /// <param name="writer"></param>
        public static void AddLogWriter(BaseLogWriter writer)
        {
            Writers.Add(writer);
        }
    }

    
}
