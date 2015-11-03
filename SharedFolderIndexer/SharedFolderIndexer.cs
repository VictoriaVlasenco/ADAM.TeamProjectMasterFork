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

        protected override void OnPreCatalog(PreCatalogEventArgs e)
        {
            base.OnPreCatalog(e);
            e.Action = CatalogAction.AddRecord;
        }

        protected override void OnCatalog(CatalogEventArgs e)
        {
            var classification = new Classification(App);
            if (classification.TryLoad(new ClassificationPath("/SoundCloud")) == TryLoadResult.NotFound)
            {
                classification.AddNew();
                classification.Name = "SoundCloud";
                classification.Save();
            }
            e.Record.Classifications.Add(classification.Id);
            base.OnCatalog(e);
        }
    }
}