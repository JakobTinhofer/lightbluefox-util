using LightBlueFox.Util.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightBlueFox.Util.Logging
{
    /// <summary>
    /// Defines in what context the message is to be interpreted. Is this an error? Or just debug info?
    /// </summary>
    public class LogLevel
    {
        #region Log Level Members
        public static LogLevel operator |(LogLevel a, LogLevel b)
        {
            return new LogLevel(a, b);
        }

        public static LogLevel operator &(LogLevel a, LogLevel b)
        {
            return new LogLevel(a.combinedLogLevels.Intersect(b.combinedLogLevels).ToArray());
        }

        /// <summary>
        /// Whether or not this combined loglevel contains a certain log level.
        /// </summary>
        /// <param name="a">The loglevel to check for</param>
        public bool Contains(LogLevel a)
        {
            if (!(combinedLogLevels is null) && combinedLogLevels.Contains(a))
                return true;
            else
                return false;
        }

        private List<LogLevel> combinedLogLevels;
        /// <summary>
        /// The color that if the LogWriter allows it, is added to the tag of the message.
        /// </summary>
        public readonly Color Color;
        /// <summary>
        /// A prefix displaying the LogLevel, like for example: <code>[ERROR] message here.</code>
        /// </summary>
        public readonly string Tag;
        /// <summary>
        /// If this is false, this cannot be used to print a message to the console. This is probably because this is combined loglevel.
        /// </summary>
        public readonly bool Printable;
        #endregion

        #region Constructors
        private LogLevel(Color clr, string tag)
        {
            Color = clr;
            Tag = tag;
            Printable = true;
        }

        public LogLevel(params LogLevel[] args)
        {


            combinedLogLevels = new List<LogLevel>();
            foreach (LogLevel l in args)
            {
                if (l.combinedLogLevels is null)
                    combinedLogLevels.Add(l);
                else
                    foreach (LogLevel l2 in l.combinedLogLevels)
                        combinedLogLevels.Add(l2);
            }
            Tag = "[CMBND]";
            Color = new Color(0, 0, 255, 255);
            Printable = false;
        }
    #endregion

        #region Log Levels

        /// <summary>
        /// Should be the default log level. Use this when normal actions are performed.
        /// </summary>
        public static readonly LogLevel INFO = new LogLevel(new Color("#0efb44")   ," INFOS ");
        /// <summary>
        /// Used in order to debug the program. All small details that are not important enough to be logged with <see cref="INFO"/> should be here.
        /// </summary>
        public static readonly LogLevel DEBUG = new LogLevel(new Color("#bababa")  ," DEBUG ");
        /// <summary>
        /// For when something occurs that might be dangerous in the future.
        /// </summary>
        public static readonly LogLevel WARNING = new LogLevel(new Color("#fac70c"),"WARNING");
        /// <summary>
        /// Used whenever there is an error, but the program can still continue.
        /// </summary>
        public static readonly LogLevel ERROR = new LogLevel(new Color("#fa350c")  ," ERROR ");
        /// <summary>
        /// Used when there is an error so severe that the program cannot continue normally.
        /// </summary>
        public static readonly LogLevel FATAL = new LogLevel(new Color("#aa0000")  ," FATAL ");
        /// <summary>
        /// Used when an operation finished successfully
        /// </summary>
        public static readonly LogLevel SUCCESS = new LogLevel(new Color("#24fc03"), "SUCCESS");
        /// <summary>
        /// Combines all log levels.
        /// </summary>
        public static readonly LogLevel ALL = new LogLevel(FATAL, ERROR, WARNING, DEBUG, INFO, SUCCESS);

        #endregion
    }
}
