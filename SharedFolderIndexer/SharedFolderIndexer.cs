#region

using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Indexer;

#endregion

namespace SharedFolderIndexer
{
    public class SharedFolderIndexer : IndexMaintenanceJob
    {
        public SharedFolderIndexer(Application app) : base(app)
        {
        }

        protected override void OnCatalog(CatalogEventArgs e)
        {
            base.OnCatalog(e);
            e.Record.Classifications.Add(new ClassificationPath("/SoundCloud"), true);
        }
    }
}