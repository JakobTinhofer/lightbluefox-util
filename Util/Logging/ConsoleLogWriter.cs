using System;
using System.Threading;

namespace LightBlueFox.Util.Logging
{
    /// <summary>
    /// A log writer printing the output out to the console.
    /// </summary>
    public class ConsoleLogWriter : BaseLogWriter
    {
        /// <summary>
        /// Use this to check if the console is currently available.
        /// </summary>
        public static AutoResetEvent ConsoleAvailable = new AutoResetEvent(true);

        /// <summary>
        /// Yes, this will print in color.
        /// </summary>
        public override bool isColorAllowed()
        {
            return true;
        }

        /// <summary>
        /// Adds a new string to be output to the console.
        /// </summary>
        public override void QueueOutput(LogLevel lvl, string output)
        {
            
            if (Enabled)
            {
                ConsoleAvailable.WaitOne();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");
                Console.ForegroundColor = lvl.Color.ToClosestConsoleColor();
                Console.Write(lvl.Tag);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");
                Console.WriteLine(" " + output);
                ConsoleAvailable.Set();
            }

        }

        /// <summary>
        /// Creates a new logwriter
        /// </summary>
        public ConsoleLogWriter(LogLevel loggedLevels) : base(loggedLevels)
        {

        }
    }
}
