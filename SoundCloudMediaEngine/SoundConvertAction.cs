#region

using System;
using System.IO;
using System.Xml;
using Adam.Core.MediaEngines;
using Adam.Core.Records;
using Adam.Tools.ExceptionHandler;
using ConverterClassLibrary;

#endregion

namespace SoundCloudMediaEngine
{
    public class SoundConvertAction : MediaAction, ICatalogAction
    {
        private const string ActionId = "SoundConvertAction";
        private readonly string[] requiredFormats = {"ogg", "ac3"};


        public SoundConvertAction(bool isCritical) : base(isCritical)
        {
        }

        public SoundConvertAction(CatalogActionData data) : base(data.IsCritical)
        {
            if (data == null)
            {
                throw ExceptionManager.CreateArgumentNullException("data");
            }
        }

        public override string Id
        {
            get { return ActionId; }
        }

        public void UpdateFileVersion(FileVersion version, XmlWriter writer)
        {
            var directoryName = Path.GetDirectoryName(version.Path);
            var fileName = Path.GetFileNameWithoutExtension(version.FileName);
            foreach (var format in requiredFormats)
            {
                var newFilePath = String.Format("{0}\\{1}.{2}", directoryName, fileName, format);
                if (AudioConverter.TryConvert(version.Path, format, newFilePath))
                {
                    version.AdditionalFiles.Add(newFilePath);
                }
            }
        }
    }
}