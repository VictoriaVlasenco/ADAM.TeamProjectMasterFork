using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterClassLibrary
{
    public static class AudioConverter
    {
        //Path = @"d:\\University\\EpamLessons\\ADAM",
        //AudioFileName = @"\\bad.wav"

        public static void Convert(string path, string audioFileName)
        {
            var writer = new BatWriter
            {
                Path = path,
                AudioFileName = audioFileName
            };
            writer.Write();

            var startInfo = new ProcessStartInfo
            {
                FileName = "Execute.bat",
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            Process.Start(startInfo);
        }
    }
}
