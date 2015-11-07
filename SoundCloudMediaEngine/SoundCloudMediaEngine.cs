#region

using System;
using System.IO;
using Adam.Core;
using Adam.Core.MediaEngines;
using AudioConverterLibrary;

#endregion

namespace SoundCloudMediaEngine
{
    internal class SoundCloudMediaEngine : MediaEngine
    {
        private const string MediaEngineId = "SoundCloudMediaEngine";

        public SoundCloudMediaEngine(Application app) : base(app)
        {
        }

        public override string Id
        {
            get { return MediaEngineId; }
        }

        public override bool Run(MediaAction action)
        {
            switch (action.Id)
            {
                case "ConvertSoundAction":
                    return TryConvertSound((ConvertSoundAction) action);
                default:
                    return false;
            }
        }

        private bool TryConvertSound(ConvertSoundAction action)
        {
            var result = false;
            foreach (var format in action.FormatsRequired)
            {
                var newFilePath = String.Format("{0}\\{1}.{2}", App.GetTemporaryFolder(),
                    Path.GetFileNameWithoutExtension(action.FilePath), format);
                if (AudioConverter.TryConvert(action.FilePath, format, newFilePath))
                {
                    action.ConvertedFilesPaths.Add(newFilePath);
                    result = true;
                }
            }
            return result;
        }
    }
}