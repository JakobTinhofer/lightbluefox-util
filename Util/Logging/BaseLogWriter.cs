﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LightBlueFox.Util.Logging
{
    public abstract class BaseLogWriter
    {
        public abstract bool isColorAllowed();
        public abstract Task QueueOutput(LogLevel lvl, string output);
        public bool Enabled { get; set; }

        public LogLevel LoggedLevels;
        public BaseLogWriter(LogLevel loggedLevels)
        {
            LoggedLevels = loggedLevels;
        }
    }
}