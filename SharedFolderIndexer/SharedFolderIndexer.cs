#region

using System;
using System.IO;
using System.Linq;
using System.Xml;
using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Fields;
using Adam.Core.Indexer;

#endregion

namespace SharedFolderIndexer
{
    public class SharedFolderIndexer : IndexMaintenanceJob
    {
        private readonly string[] extensionsSupported = {".mp3", ".wma", ".aac", ".flac"};

        public SharedFolderIndexer(Application app)
            : base(app)
        {
        }

        protected override void OnPreCatalog(PreCatalogEventArgs e)
        {
            base.OnPreCatalog(e);
            if (!extensionsSupported.Contains(Path.GetExtension(e.Path), StringComparer.InvariantCultureIgnoreCase))
            {
                e.Action = CatalogAction.Fail;
                e.Message = string.Format("Cannot add file {0} - not supported extension", e.Path);
            }
        }

        protected override void OnCatalog(CatalogEventArgs e)
        {
            base.OnCatalog(e);
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
                        if (track.SelectSingleNode("filename").InnerText.Equals(Path.GetFileName(e.Path)))
                        {
                            tmpNode = track.SelectSingleNode("title");
                            if (tmpNode != null)
                                e.Record.Fields.GetField<TextField>("Title").SetValue(tmpNode.InnerText);
                            tmpNode = track.SelectSingleNode("artist");
                            if (tmpNode != null)
                                e.Record.Fields.GetField<TextField>("Artist").SetValue(tmpNode.InnerText);
                            tmpNode = track.SelectSingleNode("genre");
                            if (tmpNode != null)
                                e.Record.Classifications.Add(new ClassificationPath("SoundCloud/" + tmpNode.InnerText),
                                    true);
                            break;
                        }
                    }
                }
            }
        }
    }
}