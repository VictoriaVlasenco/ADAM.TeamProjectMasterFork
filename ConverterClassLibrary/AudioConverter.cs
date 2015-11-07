#region

using System;
using System.Linq;
using NReco.VideoConverter;
using Format = Microsoft.SqlServer.Server.Format;

#endregion

namespace ConverterClassLibrary
{
    public static class AudioConverter
    {
        public static bool TryConvert(string filePath, string newFormat, string newPath)
        {
            var formats = Enum.GetValues(typeof (Format)).OfType<string>();
            if (formats.Contains(newFormat.ToLower()))
            {  
                try
                {
                    var ffMpeg = new FFMpegConverter();
                    ffMpeg.ConvertMedia(filePath, newPath, newFormat.ToLower());
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}