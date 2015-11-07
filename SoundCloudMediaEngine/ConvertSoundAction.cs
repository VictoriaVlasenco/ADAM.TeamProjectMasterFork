#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Adam.Core.MediaEngines;
using Adam.Core.Records;
using Adam.Tools.ExceptionHandler;

#endregion

namespace SoundCloudMediaEngine
{
    internal class ConvertSoundAction : MediaAction, ICatalogAction
    {
        private const string ActionId = "ConvertSoundAction";
        private readonly string[] formatsRequired={"ogg","ac3"};
        private readonly string originalFilePath;
        private readonly List<string> convertedFilesPaths=new List<string>();

        public List<string> ConvertedFilesPaths
        {
            get { return convertedFilesPaths; }
        }
        public string FilePath
        {
            get { return originalFilePath; }
        }
        public string[] FormatsRequired
        {
            get { return formatsRequired; }
        }
        public ConvertSoundAction(string filepath,bool isCritical) : base(isCritical)
        {
            if (filepath == null)
            {
                throw ExceptionManager.CreateArgumentNullException("filepath");
            }
            originalFilePath = filepath;
        }

        public ConvertSoundAction(CatalogActionData data):base(data.IsCritical)
        {
            if (data == null)
            {
                throw ExceptionManager.CreateArgumentNullException("data");
            }

            originalFilePath = data.Path;
        }

        public override string Id
        {
            get { return ActionId; }
        }

        public void UpdateFileVersion(FileVersion version, XmlWriter writer)
        {
            foreach (string path in convertedFilesPaths)
            {
                version.AdditionalFiles.Add(path);
            }
        }
    }
}