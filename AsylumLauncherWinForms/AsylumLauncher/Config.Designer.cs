namespace AsylumLauncher
{
    partial class FormConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbLaunchOptions = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbJavaPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCancel = new System.Windows.Forms.Button();
            this.tbSave = new System.Windows.Forms.Button();
            this.cbUseJavaDebug = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbLaunchOptions
            // 
            this.tbLaunchOptions.Location = new System.Drawing.Point(12, 165);
            this.tbLaunchOptions.Name = "tbLaunchOptions";
            this.tbLaunchOptions.Size = new System.Drawing.Size(361, 20);
            this.tbLaunchOptions.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Java arguments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Java path";
            // 
            // tbJavaPath
            // 
            this.tbJavaPath.Location = new System.Drawing.Point(12, 29);
            this.tbJavaPath.Name = "tbJavaPath";
            this.tbJavaPath.Size = new System.Drawing.Size(280, 20);
            this.tbJavaPath.TabIndex = 0;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(298, 27);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse..";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "e.g. C:\\Program Files\\Java\\jre7\\bin";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(12, 304);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(361, 20);
            this.tbIPAddress.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Asylum IP";
            // 
            // tbCancel
            // 
            this.tbCancel.Location = new System.Drawing.Point(298, 330);
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.Size = new System.Drawing.Size(75, 23);
            this.tbCancel.TabIndex = 23;
            this.tbCancel.Text = "Cancel";
            this.tbCancel.UseVisualStyleBackColor = true;
            this.tbCancel.Click += new System.EventHandler(this.tbCancel_Click);
            // 
            // tbSave
            // 
            this.tbSave.Location = new System.Drawing.Point(217, 330);
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(75, 23);
            this.tbSave.TabIndex = 24;
            this.tbSave.Text = "Save";
            this.tbSave.UseVisualStyleBackColor = true;
            this.tbSave.Click += new System.EventHandler(this.tbSave_Click);
            // 
            // cbUseJavaDebug
            // 
            this.cbUseJavaDebug.AutoSize = true;
            this.cbUseJavaDebug.Checked = true;
            this.cbUseJavaDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseJavaDebug.Location = new System.Drawing.Point(12, 191);
            this.cbUseJavaDebug.Name = "cbUseJavaDebug";
            this.cbUseJavaDebug.Size = new System.Drawing.Size(154, 17);
            this.cbUseJavaDebug.TabIndex = 25;
            this.cbUseJavaDebug.Text = "Show Java debug -window";
            this.cbUseJavaDebug.UseVisualStyleBackColor = true;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 360);
            this.Controls.Add(this.cbUseJavaDebug);
            this.Controls.Add(this.tbSave);
            this.Controls.Add(this.tbCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbIPAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbJavaPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLaunchOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbLaunchOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbJavaPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button tbCancel;
        private System.Windows.Forms.Button tbSave;
        private System.Windows.Forms.CheckBox cbUseJavaDebug;
    }
}