using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using System.IO;
using Newtonsoft.Json;

namespace AsylumLauncher
{
    public static class Settings
    {
        private const string FILE_NAME = "AsylumLauncher.ini";
        public static string Path = "";

        public static string JavaPath
        {
            get
            {
                string s = ReadValue("Java", "Path");
                if (String.IsNullOrEmpty(s))
                {
                    s = "";
                    WriteValue("Java", "Path", s);
                }
                return s;
            }
            set
            {
                WriteValue("Java", "Path", value);
            }
        }

        public static string JavaOptions
        {
            get
            {
                string s = ReadValue("Java", "Options");
                if (String.IsNullOrEmpty(s))
                {
                    s = "-XX:PermSize=256m -XX:MaxPermSize=512m -Xms4g";
                    WriteValue("Java", "Options", s);
                }
                return s;
            }
            set
            {
                WriteValue("Java", "Options", value);
            }
        }

        public static string IPAddress
        {
            get
            {
                string s = ReadValue("Server", "IP");
                if (String.IsNullOrEmpty(s))
                {
                    s = "asylum.karell.fi:500";
                    WriteValue("Server", "IP", s);
                }
                return s;
            }
            set
            {
                WriteValue("Server", "IP", value);
            }
        }

        public static bool UseJavaDebug
        {
            get
            {
                string s = ReadValue("Java", "UseDebug");
                if (String.IsNullOrEmpty(s))
                {
                    s = "True";
                    WriteValue("Java", "UseDebug", s);
                }
                return s.ToLower() == "true" ? true : false;
            }
            set
            {
                WriteValue("Java", "UseDebug", value ? "True" : "False");
            }
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        public static void WriteValue(string Section, string Key, string Value)
        {
            if (WritePrivateProfileString(Section, Key, Value, Path + FILE_NAME) == 0)
                throw new Exception();
        }

        public static string ReadValue(string Section, string Key)
        {
            StringBuilder s = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", s, 255, Path + FILE_NAME);
            return s.ToString();
        }
    }
}
