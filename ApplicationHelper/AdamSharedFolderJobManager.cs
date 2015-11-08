#region

using System.Collections.Generic;
using System.Timers;
using Adam.Core;
using JobsMaintenanceManager;

#endregion

namespace ApplicationHelper
{
    public static class AdamSharedFolderJobManager
    {
        private static readonly List<Timer> timers = new List<Timer>();

        public static void AddExecuteJobsTimer(Application app, int interval)
        {
            timers.Add(SharedFolderJobsManager.StartNewTimer(app, "SoundCloudSharedFolder", interval));
        }

        public static void StopAllTimers()
        {
            foreach (var timer in timers)
            {
                timer.Stop();
            }
        }

        public static void RemoveAllTimers()
        {
            timers.Clear();
        }
    }
}