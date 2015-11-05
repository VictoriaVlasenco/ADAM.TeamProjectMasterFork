#region

using System;
using System.Xml;
using Adam.Core.MediaEngines;
using Adam.Core.Records;

#endregion

namespace SoundCloudMediaEngine
{
    internal class ConvertSoundAction : MediaAction, ICatalogAction
    {
        private const string ActionId = "ConvertSoundAction";

        public ConvertSoundAction(bool isCritical) : base(isCritical)
        {
        }

        public override string Id
        {
            get { return ActionId; }
        }

        public void UpdateFileVersion(FileVersion version, XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}