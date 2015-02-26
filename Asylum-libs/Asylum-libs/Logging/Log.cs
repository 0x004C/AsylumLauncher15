using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsylumLibs.Logging
{
    #region Levels
    public class Level
    {
        public string Tag;
        public int Weight;

        public Level(string tag, int weight)
        {
            this.Tag = tag;
            this.Weight = weight;
        }

        public override string ToString()
        {
            return this.Tag;
        }
    }

    public static class Levels
    {
        public static Level CRITICAL = new Level("[CRITICAL]", 0);
        public static Level ERROR = new Level("[ERROR]", 1);
        public static Level WARNING = new Level("[WARNING]", 2);
        public static Level INFO = new Level("[INFO]", 3);
        public static Level VERBOSE = new Level("[VERBOSE]", 4);
    }
    #endregion

    public class LogEventArgs : EventArgs
    {
        public Level Level;
        public string Text;

        public LogEventArgs(Level level, string text)
        {
            this.Level = level;
            this.Text = text;
        }
    }

    public static class Log
    {
        public delegate void LogHandler(LogEventArgs e);
        public static event LogHandler OutEvent;

        public static void Out(Level level, string text)
        {
            if (OutEvent != null)
                OutEvent(new LogEventArgs(level, text));
        }

        public static void Out(string format, params object[] args)
        {
            Out(Levels.VERBOSE, String.Format(format, args));
        }

        public static void Info(string format, params object[] args)
        {
            Out(Levels.INFO, String.Format(format, args));
        }

        public static void Warn(string format, params object[] args)
        {
            Out(Levels.WARNING, String.Format(format, args));
            MessageBox.Show(String.Format(format, args));
        }

        public static void Error(string format, params object[] args)
        {
            Out(Levels.ERROR, String.Format(format, args));
            MessageBox.Show(String.Format(format, args));
        }
    }
}
