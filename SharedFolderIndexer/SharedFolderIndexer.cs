#region

using System;
using System.IO;
using System.Linq;
using System.Xml;
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
                record.Files.AddFile(e.Path);
                var metadataDocument = new XmlDocument();
                metadataDocument.Load(Path.GetDirectoryName(e.Path) + @"\metadata.xml");
                XmlNode tracks = metadataDocument.DocumentElement;
                XmlNode tmpNode;
                if (tracks != null)
                {
                    foreach (XmlNode track in tracks.ChildNodes)
                    {
                        if (track.SelectSingleNode("filename") != null)
                        {
                            if (Path.GetFileName(e.Path).Equals(track.SelectSingleNode("filename").InnerText))
                            {
                                tmpNode = track.SelectSingleNode("genre");
                                e.Record.Classifications.Add(
                                    tmpNode != null
                                        ? new ClassificationPath("SoundCloud/" + tmpNode.InnerText)
                                        : new ClassificationPath("SoundCloud"), true);

                                tmpNode = track.SelectSingleNode("title");
                                if (tmpNode != null)
                                    e.Record.Fields.GetField<TextField>("SoundTitle").SetValue(tmpNode.InnerText);
                                tmpNode = track.SelectSingleNode("artist");
                                if (tmpNode != null)
                                    e.Record.Fields.GetField<TextField>("SoundAuthor").SetValue(tmpNode.InnerText);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    e.Record.Classifications.Add(new ClassificationPath("SoundCloud/"), true);
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