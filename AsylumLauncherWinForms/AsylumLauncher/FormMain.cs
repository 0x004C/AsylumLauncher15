//#define HAX

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using System.Net;
using AsylumLibs;
using AsylumLibs.Logging;
using System.IO;

namespace AsylumLauncher
{
    public partial class FormMain : Form
    {

        public static string Version = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion.ToString();

        private Point mouseOffset;
        private bool isMouseDown = false;

        public FormMain()
        {
            InitializeComponent();

            SelfInstall();

            Settings.Path = AppDomain.CurrentDomain.BaseDirectory + "asylum\\";

            HandleCommandLineArgs();
#if DEBUG
            OutputConsole.Enable();
#endif
            Log.Info("AsylumLauncher v{0}", Version);
            Log.Info("LaunchInfo: {0}", String.Join(" ", Environment.GetCommandLineArgs()));
        }

        private void SelfInstall()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "asylum\\"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "asylum\\");

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "asylum\\LaunchInfo"))
                File.WriteAllText(
                    AppDomain.CurrentDomain.BaseDirectory + "asylum\\LaunchInfo",
                    @"net.minecraft.launchwrapper.Launch --tweakClass cpw.mods.fml.common.launcher.FMLTweaker --gameDir data --assetsDir data\assets --assetIndex 1.7.10 --userProperties {}");

        }

        private void HandleCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i];
                    switch (arg)
                    {
                        case "-log":
                            if (i + 1 < args.Length)
                            {
                                i++;
                                OutputFile.Enable(args[i]);
                            }
                            break;
                        case "-v":
                            OutputFile.Filter = Levels.VERBOSE;
                            break;
                    }
                }
            }
        }

#region Basic forming
        
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                if (e.Y < 25)
                {
                    xOffset = -e.X;
                    yOffset = -e.Y;
                    mouseOffset = new Point(xOffset, yOffset);
                    isMouseDown = true;
                }
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < 25)
                Cursor.Current = Cursors.Hand;
            else
                Cursor.Current = Cursors.Default;

            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Log.Info("Form close with picClose_Click");
            this.Close();
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.Image = Properties.Resources.X_hover;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.Image = Properties.Resources.X_default;
        }

        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picMin_MouseEnter(object sender, EventArgs e)
        {
            picMin.Image = Properties.Resources.min_hover;
        }

        private void picMin_MouseLeave(object sender, EventArgs e)
        {
            picMin.Image = Properties.Resources.min_default;
        }

        private void picCfg_MouseEnter(object sender, EventArgs e)
        {
            picCfg.Image = Properties.Resources.cfg_hover;
        }

        private void picCfg_MouseLeave(object sender, EventArgs e)
        {
            picCfg.Image = Properties.Resources.cfg_default;
        }

        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            Log.Out("FormMain_Load");

            lblTitle.Text = String.Format("Asylum Launcher v{0}", Version);
#if DEBUG
            lblTitle.Text += " {DEBUG}";
#endif
#if HAX
            lblTitle.Text += " HAX";
#endif
        }

        private void picCfg_Click(object sender, EventArgs e)
        {
            Log.Out("picCfg_Click");
            new FormConfig().ShowDialog();
        }

        private void tbCredentials_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnLogin_Click(null, null);
        }

#if HAX
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Log.Out("btnLogin_Click");

            if (!string.IsNullOrEmpty(tbAccount.Text))
            {
                Account account = null;
                account = new Account(tbAccount.Text);
                account.IGN = tbAccount.Text;
                account.Token = "HAX";

                if (account != null)
                {
                    Minecraft.Launch(Settings.JavaPath, Settings.JavaOptions, account.IGN, account.Token, Settings.UseJavaDebug);
                }
            }
            else
            {
                Log.Warn("Username is required");
            }
        }

#else
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Log.Out("btnLogin_Click");

            /*
            //Testing Downloader..
            DownloadManager.Downloader d = new DownloadManager.Downloader(@"http://mirror.internode.on.net/pub/test/10meg.test", "file.test", pbarStatus);
            d.AttachStatusLabel(lblStatus);
            d.Start();
            */

            if (!string.IsNullOrEmpty(tbAccount.Text) && !string.IsNullOrEmpty(tbPassword.Text))
            {
                Account account = null;

                if (Login(tbAccount.Text, tbPassword.Text, out account))
                {
                    if (account != null)
                    {
                        Minecraft.Launch(Settings.JavaPath, Settings.JavaOptions, account.IGN, account.Token, Settings.UseJavaDebug);
                    }
                    else
                        Log.Warn("Invalid username or password");
                }
            }
            else
            {
                Log.Warn("Username and password required");
            }
        }
#endif

        private bool Login(string accountName, string password, out Account account)
        {
            try
            {
                WebClient webClient = new WebClient();
                string url = String.Format("http://{0}/Query.aspx?key={1}", Settings.IPAddress, Queries.LOCK.Key);
                Log.Info("Performing HTTP-GET to ({0})", url);

                string html = webClient.DownloadString(url);
                html = html.Substring(0, html.IndexOf("\r\n"));

                password = Helpers.sha256_hash(accountName.ToLower() + "asyLum" + password);
                password = Helpers.sha256_hash(html + password);

                // 
                url = String.Format("http://{0}/Query.aspx?key={1}&paras={2};{3};{4}", Settings.IPAddress, Queries.ACCOUNT.Key, accountName, password, html);
                Log.Info("Performing HTTP-GET to ({0})", url);

                html = webClient.DownloadString(url);
                html = html.Substring(0, html.IndexOf("\r\n"));
                account = Account.Deserialize(html);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            account = null;
            return false;
        }

    }
}
