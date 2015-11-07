using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adam.Core;
using Adam.Core.MediaEngines;

namespace SoundCloudMediaEngine
{
    class SoundCloudMediaEngine:MediaEngine
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
                    ConvertSound((ConvertSoundAction)action);
                    return true;
                default:
                    return false;

            }
        }

        private void ConvertSound(ConvertSoundAction action)
        {
            string tmpFilePath;
            foreach (string format in action.FormatsRequired)
            {
                tmpFilePath = App.GetTemporaryFile(format);
                /*convertion process here
                 * result of convertion must be saved at 'tmpFilePath'
                 */
                //action.convertedFilesPaths.Add(tmpFilePath);

            }
        }
    }
}
