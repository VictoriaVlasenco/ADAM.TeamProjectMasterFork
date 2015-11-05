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
        private readonly string[] formatsSupported = { ".mp3", ".wma", ".aac", ".flac" };
        private const string ActionId = "ConvertSoundAction";
        private readonly string[] _formatsRequired;
        private readonly string _originalFilePath;
        public List<string> convertedFilesPaths  { get; set; }

        public string[] FormatsRequired
        {
            get { return _formatsRequired; }
        }
        public ConvertSoundAction(string filepath,string[]formatsRequired,bool isCritical) : base(isCritical)
        {
            if (filepath == null)
            {
                throw ExceptionManager.CreateArgumentNullException("filepath");
            }
            if (formatsRequired == null)
            {
                throw ExceptionManager.CreateArgumentNullException("formatsRequired");
            }
            _formatsRequired = formatsRequired;
            _originalFilePath = filepath;
            convertedFilesPaths=new List<string>();
        }

        public ConvertSoundAction(CatalogActionData data):base(data.IsCritical)
        {
            if (data == null)
            {
                throw ExceptionManager.CreateArgumentNullException("data");
            }

            _originalFilePath = data.Path;
            _formatsRequired = formatsSupported.Except(new List<string>() {Path.GetExtension(data.Path)}).ToArray();
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