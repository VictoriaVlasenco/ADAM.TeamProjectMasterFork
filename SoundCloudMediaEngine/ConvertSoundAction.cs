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
        private readonly string[] _formatsRequired={"ogg","ac3"};
        private readonly string _originalFilePath;
        private readonly List<string> _convertedFilesPaths=new List<string>();

        public List<string> ConvertedFilesPaths
        {
            get { return _convertedFilesPaths; }
        }

        public string[] FormatsRequired
        {
            get { return _formatsRequired; }
        }
        public ConvertSoundAction(string filepath,bool isCritical) : base(isCritical)
        {
            if (filepath == null)
            {
                throw ExceptionManager.CreateArgumentNullException("filepath");
            }
            _originalFilePath = filepath;
        }

        public ConvertSoundAction(CatalogActionData data):base(data.IsCritical)
        {
            if (data == null)
            {
                throw ExceptionManager.CreateArgumentNullException("data");
            }

            _originalFilePath = data.Path;
        }

        public override string Id
        {
            get { return ActionId; }
        }

        public void UpdateFileVersion(FileVersion version, XmlWriter writer)
        {
            foreach (string path in _convertedFilesPaths)
            {
                version.AdditionalFiles.Add(path);
            }
        }
    }
}