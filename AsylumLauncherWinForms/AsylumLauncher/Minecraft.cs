using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Diagnostics;

namespace AsylumLauncher
{
    public static class Minecraft
    {
        public static void Launch(string javapath, string options, string username, string token, bool showDebug)
        {
            if (!Directory.Exists(@"data\libs"))
            {
                AsylumLibs.Logging.Log.Error("Asylum installation is corrupted!");
                return;
            }

            string[] aLibs = Directory.GetFiles(@"data\libs", "*.jar", System.IO.SearchOption.AllDirectories);

            if (!String.IsNullOrEmpty(javapath))
                if (javapath[javapath.Length - 1] != '\\')
                    javapath += "\\";

            if (showDebug)
                javapath += "java.exe";
            else
                javapath += "javaw.exe";

            if (aLibs != null)
            {
                ProcessStartInfo info = new ProcessStartInfo(javapath, "");
                info.Arguments = options.Trim() + @" -Djava.library.path=data\natives\ -cp ";

                foreach (string s in aLibs)
                    info.Arguments += s + ";";

                if (File.Exists(@"Asylum\LaunchInfo"))
                    info.Arguments += " " + File.ReadAllText(@"Asylum\LaunchInfo").Trim();
                else
                    return;

                info.Arguments += String.Format(
                    " --username {1} --accessToken {0} ",
                    token, username);

                Process.Start(info);
            }
            else
            {

            }
        }
    }
}
