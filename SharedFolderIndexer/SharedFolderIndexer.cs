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
            XmlDocument metadataDocument = new XmlDocument();
            metadataDocument.Load(Path.GetDirectoryName(e.Path)+@"\metadata.xml");
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
        }
    }
}
