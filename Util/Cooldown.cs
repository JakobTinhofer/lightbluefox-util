using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LightBlueFox.Util
{
    /// <summary>
    /// A template for methods that can be triggered through cooldowns.
    /// </summary>
    /// <param name="args">Any arguments for that method. Accepts null!</param>
    public delegate void CooldownAction(object[] args);
    /// <summary>
    /// A class that can be used to allow actions to be reused only after a certain interval.
    /// </summary>
    public class Cooldown
    {
        /// <summary>
        /// Creates a new Cooldown object. Also assigns the action to <see cref="CooldownActionTriggered"/>.
        /// </summary>
        public Cooldown(int timeInMS, CooldownAction action)
        {
            DurationInMilliseconds = timeInMS;
            CooldownActionTriggered += action;
        }

        /// <summary>
        /// Creates a new Cooldown object. Note that by default, this will not do anything when triggered. In order to do that, please assign an action to <see cref="CooldownActionTriggered"/>.
        /// </summary>
        public Cooldown(int timeInMS)
        {
            DurationInMilliseconds = timeInMS;
        }
        /// <summary>
        /// How long the interval between two triggers needs to be.
        /// </summary>
        public int DurationInMilliseconds { get; set; }
        /// <summary>
        /// Assign all actions that should be triggered here.
        /// </summary>
        public event CooldownAction CooldownActionTriggered;
        /// <summary>
        /// If this is set to true, all actions are automaticaly triggered once the timer is reset.
        /// </summary>
        public bool AutoTrigger { get; set; } = false;
        /// <summary>
        /// Whether or not the action is currently available.
        /// </summary>
        public bool Ready { get; private set; } = true;

        private AutoResetEvent ev = new AutoResetEvent(true);

        private async void StartReset()
        {
            await Task.Delay(DurationInMilliseconds);
            Ready = true;
            ev.Set();
            if (AutoTrigger)
                TryTrigger(null);
        }

        /// <summary>
        /// Tries to trigger the action. If the action is still on cooldown, returns false.
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="Ready"/> at the moment of the function call</returns>        
        public bool TryTrigger(object[] args)
        {
            if (Ready)
            {
                Ready = false;
                ev.Reset();
                CooldownActionTriggered.BeginInvoke(args, null, null);
                StartReset();
                return true;
            }    
            return false;
        }

        /// <summary>
        /// This will wait until the cooldown is over, and then trigger the action.
        /// </summary>
        /// <param name="args"></param>
        public void WaitTillTrigger(object[] args)
        {
            ev.WaitOne();
            Ready = false;
            CooldownActionTriggered.BeginInvoke(args, null, null);
            StartReset();
        }
    
    }
}
