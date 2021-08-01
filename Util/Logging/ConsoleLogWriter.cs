using System;
using System.Threading;

namespace LightBlueFox.Util.Logging
{
    public class ConsoleLogWriter : BaseLogWriter
    {
        private static AutoResetEvent consoleAvail = new AutoResetEvent(true);

        public override bool isColorAllowed()
        {
            return true;
        }

        public override void QueueOutput(LogLevel lvl, string output)
        {
            
            if (Enabled)
            {
                consoleAvail.WaitOne();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");
                Console.ForegroundColor = lvl.Color.ToClosestConsoleColor();
                Console.Write(lvl.Tag);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");
                Console.WriteLine(" " + output);
                consoleAvail.Set();
            }

        }


        public ConsoleLogWriter(LogLevel loggedLevels) : base(loggedLevels)
        {

        }
    }
}
