using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsylumLibs.Logging
{
    public class OutputConsole : Output
    {
        private static OutputConsole _OutputConsole = new OutputConsole();

        public override void Out(Level level, string text)
        {
            Console.WriteLine(String.Format("[{0}] {1}: {2}", DateTime.Now.ToString("o"), level.Tag, text));
        }

        public static void Enable()
        {
            _OutputConsole._Enable();
        }

        public static void Disable()
        {
            _OutputConsole._Disable();
        }
    }
}
