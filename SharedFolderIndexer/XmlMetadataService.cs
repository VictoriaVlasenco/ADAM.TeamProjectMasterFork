using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Adam.Core.Fields;

namespace SharedFolderIndexer
{
    class XmlMetadataService : IMetadataService
    {
        public string FilePath { get; set; }
        public XmlMetadataService(string filePath)
        {
            FilePath = filePath;
        }
        public List<RecordMetadata> GetMetadataList()
        {
            if (FilePath == null)
                return null;
            if (!File.Exists(FilePath))
                return null;
            List<RecordMetadata> metadataList = new List<RecordMetadata>();
            var metadataDocument = new XmlDocument();
            metadataDocument.Load(FilePath);
            XmlNode tracks = metadataDocument.DocumentElement;
            XmlNode fileNameNode, titleNode, artistNode, genreNode;
            foreach (XmlNode track in tracks.ChildNodes)
            {
                fileNameNode = track.SelectSingleNode("filename");
                titleNode = track.SelectSingleNode("title");
                artistNode = track.SelectSingleNode("artist");
                genreNode = track.SelectSingleNode("genre");
                metadataList.Add(new RecordMetadata()
                {
                    FileName = fileNameNode != null ? fileNameNode.InnerText : null,
                    Title = titleNode != null ? titleNode.InnerText : null,
                    Artist = artistNode != null ? artistNode.InnerText : null,
                    Genre = genreNode != null ? genreNode.InnerText : null
                });
            }
        }
    }
}
