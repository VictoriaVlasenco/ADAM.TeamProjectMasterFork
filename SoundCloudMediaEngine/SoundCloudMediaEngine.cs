#region

using Adam.Core;
using Adam.Core.MediaEngines;
using ConverterClassLibrary;

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
                    ConvertSound((ConvertSoundAction) action);
                    return true;
                default:
                    return false;
            }
        }

        private void ConvertSound(ConvertSoundAction action)
        {
            foreach (var format in action.FormatsRequired)
            {
                var newFilePath = App.GetTemporaryFile(format);
                if (AudioConverter.TryConvert(action.FilePath, format, newFilePath))
            {
                    action.ConvertedFilesPaths.Add(newFilePath);
                }
            }
        }
    }
}
