#region

using System;
using System.IO;
using System.Linq;
using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Fields;
using Adam.Core.Indexer;
using Adam.Core.Records;
using File = System.IO.File;

#endregion

namespace SharedFolderIndexer
{
    public class SharedFolderIndexer : IndexMaintenanceJob
    {
        private readonly string[] formatsSupported = {".mp3"};

        public SharedFolderIndexer(Application app)
            : base(app)
        {
        }

        protected override void OnPreCatalog(PreCatalogEventArgs e)
        {
            base.OnPreCatalog(e);
            e.Action = CatalogAction.AddRecord;
            if (formatsSupported.Contains(Path.GetExtension(e.Path), StringComparer.InvariantCultureIgnoreCase))
            {
                e.Action = CatalogAction.Manual;
            }
            else
            {
                e.Action = CatalogAction.Fail;
                e.Message = string.Format("Cannot add file {0} - not supported extension", e.Path);
            }
        }

        protected override void OnCatalog(CatalogEventArgs e)
        {
            base.OnCatalog(e);
            if (e.Action == CatalogAction.Manual)
            {
                var record = new Record(App);
                record.AddNew();
                var filePath = e.Path;
                record.Files.AddFile(filePath);
                IMetadataService metadataService =
                    new XmlMetadataService(Path.GetDirectoryName(filePath) + @"\metadata.xml");
                var metadataList = metadataService.GetMetadataList();
                var metadataFounded = false;
                if (metadataList != null)
                {
                    var metadata = metadataList.Find(file => Path.GetFileName(filePath).Equals(file.FileName));
                    if (metadata != null)
                    {
                        metadataFounded = true;
                        record.Classifications.Add(
                            String.IsNullOrEmpty(metadata.Genre)
                                ? new ClassificationPath("SoundCloud/Unclassified")
                                : new ClassificationPath("SoundCloud/" + metadata.Genre), true);
                        record.Fields.GetField<TextField>("SoundTitle")
                            .SetValue(String.IsNullOrEmpty(metadata.Title) ? metadata.FileName : metadata.Title);
                        record.Fields.GetField<TextField>("SoundAuthor")
                            .SetValue(String.IsNullOrEmpty(metadata.Title) ? metadata.FileName : metadata.Title);
                    }
                }
                if (!metadataFounded)
                {
                    record.Classifications.Add(new ClassificationPath("SoundCloud/Unclassified"), true);
                    record.Fields.GetField<TextField>("SoundTitle").SetValue(Path.GetFileName(filePath));
                    record.Fields.GetField<TextField>("SoundAuthor").SetValue("Unkonwn");
                }
                record.Save();
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }
    }
}