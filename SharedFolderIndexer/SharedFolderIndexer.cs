#region

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Fields;
using Adam.Core.Indexer;
using Adam.Core.Maintenance;

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
            XPathDocument metadataDocument = new XPathDocument(Path.GetDirectoryName(e.Path) + "/metadata.xml");
            XPathNavigator metadataNavigator = metadataDocument.CreateNavigator();
            XPathNodeIterator trackIterator = metadataNavigator.Select("library/track");
            XmlNode tmpNode;
            foreach (XmlNode track in trackIterator)
            {
                if (Path.GetFileName(e.Path).Equals(track.SelectSingleNode("fileName")))
                {
                    tmpNode = track.SelectSingleNode("title");
                    if (tmpNode != null)
                        e.Record.Fields.GetField<TextField>("Title").SetValue(tmpNode.Value);
                    tmpNode = track.SelectSingleNode("artist");
                    if (tmpNode != null)
                        e.Record.Fields.GetField<TextField>("Artist").SetValue(tmpNode.Value);
                    tmpNode = track.SelectSingleNode("genre");
                    if (tmpNode != null)
                        e.Record.Classifications.Add(new ClassificationPath("SoundCloud/"+tmpNode.Value), true);
                    break;
                }
            }
        }
    }
}