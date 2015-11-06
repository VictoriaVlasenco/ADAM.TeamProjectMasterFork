#region

using System;
using System.Collections.Generic;
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
        private readonly string[] formatsSupported = { ".mp3" };

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
                Record record = new Record(App);
                record.AddNew();
                record.Files.AddFile(e.Path);
                XmlMetadataService metadataService =
                    new XmlMetadataService(Path.GetDirectoryName(e.Path) + @"\metadata.xml");
                List<RecordMetadata> metadataList = metadataService.GetMetadataList();
                bool metadataFound = false;
                if (metadataList != null)
                {
                    foreach (RecordMetadata metadata in metadataList)
                    {
                        if (metadata.FileName != null)
                        {
                            if (metadata.FileName.Equals(Path.GetFileName(e.Path)))
                            {
                                metadataFound = true;
                                record.Classifications.Add(
                                    metadata.Genre != null
                                        ? new ClassificationPath("SoundCloud/" + metadata.Genre)
                                        : new ClassificationPath("SoundCloud/Unclassified"), true);

                                record.Fields.GetField<TextField>("SoundTitle").SetValue(metadata.Title ?? metadata.FileName);
                                record.Fields.GetField<TextField>("SoundAuthor").SetValue(metadata.Artist ?? "Unkonwn");
                                break;
                            }
                        }
                    }
                }
                if(!metadataFound)
                {
                    record.Classifications.Add(new ClassificationPath("SoundCloud/Unclassified"), true);
                    record.Fields.GetField<TextField>("SoundTitle").SetValue(Path.GetFileName(e.Path));
                    record.Fields.GetField<TextField>("SoundAuthor").SetValue("Unkonwn");
                }
                record.Save();
                if (File.Exists(e.Path))
                {
                    try
                    {
                        File.Delete(e.Path);
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }
    }
}