using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightBlueFox.Util.Logging
{
    public class ConsoleLogWriter : BaseLogWriter
    {
        private static ManualResetEvent consoleAvail = new ManualResetEvent(true);

        public override bool isColorAllowed()
        {
            return true;
        }

        public override async Task QueueOutput(LogLevel lvl, string output)
        {
            await Task.Run(() => {
                
                if (Enabled)
                {
                    consoleAvail.WaitOne();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[");
                    Console.ForegroundColor = lvl.Color.ToClosestConsoleColor();
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" " + output);
                    consoleAvail.Set();
                }
            });
            
        }


        public ConsoleLogWriter(LogLevel loggedLevels) : base(loggedLevels)
        {

        }
    }
}
