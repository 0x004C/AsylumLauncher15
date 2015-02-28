using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace AsylumLauncher.DownloadManager
{
    public class Downloader : IDisposable
    {
        public delegate void DownloadDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
        public event DownloadDataCompletedEventHandler DownloadCompleted;

        public string Url = "";
        public string File = "";
        public long BytesTotal = 0;
        public long BytesDownloaded = 0;
        public long BytesLeft = 0;
        public float PercentDone
        {
            get
            {
                return (float)((double)BytesDownloaded / (double)BytesTotal) * 100;
            }
            private set { }
        }

        public DateTime TimeStart = new DateTime();
        public TimeSpan TimeDownloaded
        {
            get
            {
                return DateTime.Now - TimeStart;
            }
            private set { }
        }

        private double?[] _OldKBps = { null, null };
        private double _KBps = 0;
        public double KBps
        {
            get
            {
                return _KBps;
            }
            private set 
            {
                _KBps = value;
            }
        }

        /// <summary>
        /// 0: Url, 1: File, 2: BytesTotal, 3: BytesDownloaded, 4: BytesLeft, 5: PercentDone, 6: KBps
        /// Default format: Downloading {1} ({5}%)   {6} kB/s - {3}/{2} kB
        /// </summary>
        public string StatusFormat = "Downloading {1} ({5}%)   {6} kB/s - {3}/{2} kB";

        private WebClient _Client = new WebClient();

        private System.Windows.Forms.ProgressBar _ProgressBar = null;
        private System.Windows.Forms.Label _Label = null;

        private Timer _UpdateKBps = new Timer(1000);
        private long _LastBytesDownloaded = 0;

        public Downloader(string url, string file)
        {
            Url = url;
            File = file;

            _Client.DownloadProgressChanged += _Client_DownloadProgressChanged;
            _Client.DownloadFileCompleted += _Client_DownloadFileCompleted;
            _Client.DownloadFileAsync(new Uri(url), file);

            _UpdateKBps.Elapsed += _UpdateKBps_Elapsed;
            _UpdateKBps.Start();
        }

        public Downloader(string url, string file, System.Windows.Forms.ProgressBar progressBar)
        {
            this.Url = url;
            this.File = file;

            _ProgressBar = progressBar;
            _ProgressBar.Minimum = 0;
            _ProgressBar.Maximum = 100;
            _ProgressBar.Value = 0;

            _Client.DownloadProgressChanged += _Client_DownloadProgressChanged;
            _Client.DownloadFileCompleted += _Client_DownloadFileCompleted;

            _UpdateKBps.Elapsed += _UpdateKBps_Elapsed;

        }

        public void AttachStatusTextBox(System.Windows.Forms.Label label, string format, params object[] args)
        {
            _Label = label;

            StatusFormat = format;
        }

        public void AttachStatusLabel(System.Windows.Forms.Label label)
        {
            _Label = label;
        }

        public void Start()
        {
            _Client.DownloadFileAsync(new Uri(Url), File);
            _UpdateKBps.Start();
        }

        private void _UpdateKBps_Elapsed(object sender, ElapsedEventArgs e)
        {
            double averageKBps = 0;
            KBps = (BytesDownloaded - _LastBytesDownloaded) / 1000f;
            
            // Stabilizing speed with avarage of current and past two speeds
            if (_OldKBps[0] != null)
            {
                if (_OldKBps[1] != null)
                {
                    averageKBps = (KBps + (double)_OldKBps[0] + (double)_OldKBps[1]) / 3;
                }
                else
                {
                    averageKBps = (KBps + (double)_OldKBps[0]) / 2;
                }
            }

            _OldKBps[1] = _OldKBps[0];
            _OldKBps[0] = KBps;

            KBps = averageKBps;

            _LastBytesDownloaded = BytesDownloaded;
        }

        void _Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BytesTotal = e.TotalBytesToReceive;
            BytesDownloaded = e.BytesReceived;

            if (_ProgressBar != null)
            {
                _ProgressBar.Value = (int)Math.Round(PercentDone) < 100 ? (int)Math.Round(PercentDone) : 100;
            }

            if (_Label != null)
            {
                _Label.Text = String.Format(
                    StatusFormat, 
                    Url, 
                    File,
                    BytesTotal / 1000, 
                    BytesDownloaded / 1000, 
                    BytesLeft / 1000, 
                    PercentDone.ToString("0.0"), 
                    KBps.ToString("0.00"));
            }
        }

        void _Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (_ProgressBar != null)
            {
                _ProgressBar.Value = _ProgressBar.Maximum;
            }

            if (_Label != null)
            {
                _Label.Text = "Done.";
            }

            if (DownloadCompleted != null)
            {
                DownloadCompleted(sender, e);
            }

            this.Dispose();
        }

        public void Dispose()
        {
            _Client.Dispose();
            _UpdateKBps.Dispose();
        }
    }
}
