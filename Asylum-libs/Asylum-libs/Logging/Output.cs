using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsylumLibs.Logging
{
    public abstract class Output
    {
        public abstract void Out(Level level, string text);

        public virtual void _Enable()
        {
            Log.OutEvent += Log_OutEvent;
        }

        void Log_OutEvent(LogEventArgs e)
        {
            this.Out(e.Level, e.Text);
        }

        public virtual void _Disable()
        {
            Log.OutEvent -= Log_OutEvent;
        }
    }
}
