using LightBlueFox.Util;
using LightBlueFox.Util.Logging;
using LightBlueFox.Util.Types;
using System;

namespace Util_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            CooldownTests();
            Console.WriteLine("Starting tests.");

            Console.WriteLine("Running color tests.");
            if (ColorTests())
                Console.WriteLine("ColorTests succeeded.");
            else
                Console.WriteLine("ColorTests failed.");



            Console.WriteLine("Running logger tests.");
            if (LoggerTest())
                Console.WriteLine("LoggerTests succeeded.");
            else
                Console.WriteLine("LoggerTests failed.");

        }

        private static bool ColorTests()
        {
            bool success = true;
            Console.Write("Creating color #00fF00...");
            Color c = new Color("#00fF00");
            if (c.R != 0 || c.G != 255 || c.B != 0)
            {
                success = false;
                Console.WriteLine("[FAILED]");
            }
            else
                Console.WriteLine("[OK]");

            Console.Write("Creating color 0x10fF0156...");
            c = new Color("0x10fF0156");
            if (c.R != 16 || c.G != 255 || c.B != 1 || c.Alpha != 86)
            {
                success = false;
                Console.WriteLine("[FAILED]");
            }
            else
                Console.WriteLine("[OK]");

            Console.Write("Creating color from R = 44, G = 22, B = 11, A = 55...");
            c = new Color(44, 22, 11, 55);
            if (c.R != 44 || c.G != 22 || c.B != 11 || c.Alpha != 55 || new Color(c.HexRGBA) != c)
            {
                success = false;
                Console.WriteLine("[FAILED]");
            }
            else
                Console.WriteLine("[OK]");

            Console.Write("Changing rgb values...");
            c.R = 255; c.G = 255; c.B = 0; c.Alpha = 0;
            if (c.HexRGBA != "#FFFF0000")
            {
                success = false;
                Console.WriteLine("[FAILED]");
            }
            else
                Console.WriteLine("[OK]");


            return success;
        }

        private static bool LoggerTest()
        {
            bool success = true;

            Console.Write("Creating ConsoleLogger....");
            ConsoleLogWriter w = new ConsoleLogWriter(new LogLevel(LogLevel.INFO, LogLevel.FATAL));
            Console.WriteLine("[OK]");

            Console.Write("Adding writer to Logger...");
            Logger.AddLogWriter(w);
            Console.WriteLine("[OK]");

            Console.Write("Logging to console.");
            Logger.Log(LogLevel.WARNING, "FAILED");
            Logger.Log(LogLevel.INFO, "OK");

            return success;
        }

        private static bool CooldownTests()
        {
            bool success = true;

            Console.WriteLine("Creating long cooldown");
            LongCooldown c = new LongCooldown(new TimeSpan(0, 0, 30));
            Console.WriteLine(c.Trigger());
            Console.WriteLine(c.Trigger());
            return success;
        }
    }
}
