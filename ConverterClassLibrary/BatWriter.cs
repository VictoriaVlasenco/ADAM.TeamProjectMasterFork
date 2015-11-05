using System;
using System.Collections.Generic;
using System.IO;

namespace ConverterClassLibrary
{
    class BatWriter
    {
        private readonly List<string> formats = new List<string> { "mp3", "m4a", "ogg", "flac", "wav" };

        public string AudioFileName { get; set; }
        public string Path { get; set; }

        public void Write()
        {
            var command = "\"c:\\Program Files (x86)\\XRECODE\\xrecode.exe\" /input " + Path + AudioFileName
                + " /output " + OutputPath + " /dest ";

            using (var outputFile = new StreamWriter("Execute.bat"))
            {
                for (int i = 0; i < 5; i++)
                {
                    outputFile.WriteLine(command + formats[i]);
                }
            }
        }
		
		public void WriteFormat(string format)
        {
            var command = "\"c:\\Program Files (x86)\\XRECODE\\xrecode.exe\" /input " + Path + AudioFileName
                + " /output " + OutputPath + " /dest " + format;

            using (var outputFile = new StreamWriter("Execute.bat"))
            {
                outputFile.WriteLine(command);
            }
        }
    }
}
