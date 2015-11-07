#region

using System;
using System.Linq;
using NReco.VideoConverter;

#endregion

namespace AudioConverterLibrary
{
    public static class AudioConverter
    {
        public static bool TryConvert(string filePath, string newFormat, string newPath)
        {
            var supportedFormats = new[] {"ogg", "ac3"};
            if (supportedFormats.Contains(newFormat.ToLower()))
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