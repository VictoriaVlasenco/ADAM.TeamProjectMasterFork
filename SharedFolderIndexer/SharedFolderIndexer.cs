#region

using System.IO;
using System.Linq;
using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Indexer;
using Adam.Core.Records;

#endregion

namespace SharedFolderIndexer
{
    public class SharedFolderIndexer : IndexMaintenanceJob
    {
        private readonly string[] supportedExtensions = {".mp3", ".ogg", ".acc", ".wav"};

        public SharedFolderIndexer(Application app) : base(app)
        {
        }

        protected override void OnPreCatalog(PreCatalogEventArgs e)
        {
            base.OnPreCatalog(e);
            if (supportedExtensions.Contains(Path.GetExtension(e.Path)))
            {
                e.Action = CatalogAction.AddRecord;
            }
        }

        protected override void OnCatalog(CatalogEventArgs e)
        {
            base.OnCatalog(e);
            if (e.Action == CatalogAction.AddRecord)
            {
                var record = new Record(App);
                record.AddNew();
                record.Classifications.Add(new ClassificationPath("/SoundCloud"), true);
                record.Files.AddFile(e.Path);
                record.Save();
            }
        }
    }
}