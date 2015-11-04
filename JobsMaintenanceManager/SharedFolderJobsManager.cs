#region

using System.Timers;
using Adam.Core;
using Adam.Core.Indexer;
using Adam.Core.Maintenance;

#endregion

namespace JobsMaintenanceManager
{
    public static class SharedFolderJobsManager
    {
        public static Timer StartNewTimer(Application app, string indexerName, int interval)
        {
            var timer = new Timer(interval);
            timer.Elapsed += delegate
            {
                var indexerTask = new IndexerTask(app);
                if (indexerTask.TryLoad(indexerName) == TryLoadResult.Success)
                {
                    var maintenanceManager = new MaintenanceManager(app) {GroupId = indexerTask.Id};
                    maintenanceManager.Execute();
                }
            };
            return timer;
        }
    }
}