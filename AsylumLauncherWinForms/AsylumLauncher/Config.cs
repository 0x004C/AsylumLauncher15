using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AsylumLauncher
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbJavaPath.Text))
                folderBrowserDialog.SelectedPath = tbJavaPath.Text;

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                bool isJava = false;
                foreach (string file in Directory.GetFiles(folderBrowserDialog.SelectedPath))
                {
                    if (file.Contains("java.exe"))
                    {
                        isJava = true;
                        break;
                    }
                }

                if (isJava)
                    tbJavaPath.Text = folderBrowserDialog.SelectedPath;
                else
                    AsylumLibs.Logging.Log.Error("Javaa ei löydetty kansiosta..");
            }
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            // TODO: Haistele java

            tbJavaPath.Text = Settings.JavaPath;
            tbLaunchOptions.Text = Settings.JavaOptions;
            tbIPAddress.Text = Settings.IPAddress;
        }

        private void tbSave_Click(object sender, EventArgs e)
        {
            Settings.JavaOptions = tbLaunchOptions.Text;
            Settings.JavaPath = tbJavaPath.Text;
            Settings.IPAddress = tbIPAddress.Text;
            this.Close();
        }

        private void tbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
