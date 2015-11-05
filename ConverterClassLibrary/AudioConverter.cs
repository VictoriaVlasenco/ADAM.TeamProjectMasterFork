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
                Path = Regex.Escape(path),
                AudioFileName = Regex.Escape(audioFileName)
				OutputPath = Regex.Escape(outPath)
            };
            writer.Write();

            var startInfo = new ProcessStartInfo
            {
                FileName = "Execute.bat",
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            Process.Start(startInfo);
        }
		
		public static void ConvertTo(string fromPath, string audioFileName, string outPath, string formatType)
        {
            var writer = new BatWriter
            {
                Path = Regex.Escape(fromPath),
                AudioFileName = Regex.Escape(audioFileName),
                OutputPath = Regex.Escape(outPath)
            };
            writer.WriteFormat(formatType);

            var startInfo = new ProcessStartInfo
            {
                FileName = "Execute.bat",
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            Process.Start(startInfo);
        }
    }
}
