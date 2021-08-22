using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Util
{
    /// <summary>
    /// A cooldown for longer timespans.
    /// </summary>
    public class LongCooldown
    {
        /// <summary>
        /// The Point of time that the action was last triggered.
        /// </summary>
        public DateTime StartingPoint { get; private set; }
        /// <summary>
        /// The time to wait after use.
        /// </summary>
        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// Creates a new cooldown object. The <see cref="StartingPoint"/> is set to <see cref="DateTime.Now"/> - <see cref="Duration"/>
        /// </summary>
        public LongCooldown(TimeSpan duration)
        {
            StartingPoint = DateTime.Now - duration;
            Duration = duration;
        }
        /// <summary>
        /// Whether or not the cooldown is run down.
        /// </summary>
        public bool Check()
        {
            if ((StartingPoint + Duration) <= DateTime.Now)
                return true;
            return false;
        }
        /// <summary>
        /// First checks whether or not cooldown is available. If so, resets and sets <see cref="StartingPoint"/> to <see cref="DateTime.Now"/> + <see cref="Duration"/>.
        /// </summary>
        /// <returns></returns>
        public bool Trigger()
        {
            if (Check())
            {
                StartingPoint = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
