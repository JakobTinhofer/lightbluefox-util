using System;
using System.Threading;

namespace LightBlueFox.Util.Logging
{
    public class ConsoleLogWriter : BaseLogWriter
    {
        public static AutoResetEvent ConsoleAvailable = new AutoResetEvent(true);

        public override bool isColorAllowed()
        {
            return true;
        }

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


        public ConsoleLogWriter(LogLevel loggedLevels) : base(loggedLevels)
        {

        }
    }
}
