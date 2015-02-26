using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AsylumLibs.Logging
{
    public class OutputFile : Output
    {
        public static Level Filter;
        public static string FilePath;

        private static OutputFile _OutputFile = new OutputFile();

        public override void Out(Level level, string text)
        {
            if (level.Weight <= Filter.Weight)
            {
                text = String.Format("[{0}] {1}: {2}", DateTime.Now.ToString("o"), level.Tag, text);
                File.AppendAllText(FilePath, text + "\r\n");
            }
        }

        public static void Enable(string file, Level filter)
        {
            if (!String.IsNullOrEmpty(file))
            {
                OutputFile.FilePath = file;
                OutputFile.Filter = filter;

                _OutputFile._Enable();
            }
        }

        public static void Enable(string file)
        {
            Enable(file, Levels.INFO);
        }

        public static void Disable()
        {
            _OutputFile._Disable();
        }
    }
}
