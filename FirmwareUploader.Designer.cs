namespace NIKA_CPS_V1
{
    partial class FirmwareUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirmwareUploader));
            this.tsUpdater = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tbHelp = new System.Windows.Forms.ToolStripButton();
            this.gbRadioType = new System.Windows.Forms.GroupBox();
            this.rbMDUV380 = new System.Windows.Forms.RadioButton();
            this.rbMD9600 = new System.Windows.Forms.RadioButton();
            this.pbUploading = new System.Windows.Forms.ProgressBar();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.ofdOpenFirmware = new System.Windows.Forms.OpenFileDialog();
            this.tsUpdater.SuspendLayout();
            this.gbRadioType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsUpdater
            // 
            this.tsUpdater.BackColor = System.Drawing.Color.White;
            this.tsUpdater.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsUpdater.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsUpdater.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsUpdater.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbUpdate,
            this.tbHelp});
            this.tsUpdater.Location = new System.Drawing.Point(6, 6);
            this.tsUpdater.Name = "tsUpdater";
            this.tsUpdater.Size = new System.Drawing.Size(53, 297);
            this.tsUpdater.TabIndex = 0;
            this.tsUpdater.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(50, 52);
            this.tsbOpen.Text = "Открыть файл прошивки";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUpdate.Enabled = false;
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(50, 52);
            this.tsbUpdate.Text = "Прошить рацию";
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // tbHelp
            // 
            this.tbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tbHelp.Image")));
            this.tbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.Size = new System.Drawing.Size(50, 52);
            this.tbHelp.Text = "Подсказка";
            this.tbHelp.Click += new System.EventHandler(this.tbHelp_Click);
            // 
            // gbRadioType
            // 
            this.gbRadioType.Controls.Add(this.rbMDUV380);
            this.gbRadioType.Controls.Add(this.rbMD9600);
            this.gbRadioType.Location = new System.Drawing.Point(75, 21);
            this.gbRadioType.Name = "gbRadioType";
            this.gbRadioType.Size = new System.Drawing.Size(266, 83);
            this.gbRadioType.TabIndex = 1;
            this.gbRadioType.TabStop = false;
            this.gbRadioType.Text = " Тип рации ";
            // 
            // rbMDUV380
            // 
            this.rbMDUV380.AutoSize = true;
            this.rbMDUV380.Location = new System.Drawing.Point(17, 49);
            this.rbMDUV380.Name = "rbMDUV380";
            this.rbMDUV380.Size = new System.Drawing.Size(236, 19);
            this.rbMDUV380.TabIndex = 1;
            this.rbMDUV380.TabStop = true;
            this.rbMDUV380.Tag = "1";
            this.rbMDUV380.Text = "TYT MD-UV380/390 (Retevis RT-3S)";
            this.rbMDUV380.UseVisualStyleBackColor = true;
            // 
            // rbMD9600
            // 
            this.rbMD9600.AutoSize = true;
            this.rbMD9600.Location = new System.Drawing.Point(17, 23);
            this.rbMD9600.Name = "rbMD9600";
            this.rbMD9600.Size = new System.Drawing.Size(199, 19);
            this.rbMD9600.TabIndex = 0;
            this.rbMD9600.TabStop = true;
            this.rbMD9600.Tag = "0";
            this.rbMD9600.Text = "TYT MD-9600 (Retevis RT-90)";
            this.rbMD9600.UseVisualStyleBackColor = true;
            // 
            // pbUploading
            // 
            this.pbUploading.BackColor = System.Drawing.Color.White;
            this.pbUploading.Location = new System.Drawing.Point(75, 111);
            this.pbUploading.Name = "pbUploading";
            this.pbUploading.Size = new System.Drawing.Size(735, 23);
            this.pbUploading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbUploading.TabIndex = 2;
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(75, 149);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ReadOnly = true;
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(735, 147);
            this.tbConsole.TabIndex = 3;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(364, 26);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(425, 60);
            this.lblText.TabIndex = 4;
            this.lblText.Text = resources.GetString("lblText.Text");
            // 
            // ofdOpenFirmware
            // 
            this.ofdOpenFirmware.Filter = "Файлы прошивки|*.nff";
            // 
            // FirmwareUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(830, 309);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.pbUploading);
            this.Controls.Add(this.gbRadioType);
            this.Controls.Add(this.tsUpdater);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FirmwareUploader";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Загрузчик прошивки";
            this.tsUpdater.ResumeLayout(false);
            this.tsUpdater.PerformLayout();
            this.gbRadioType.ResumeLayout(false);
            this.gbRadioType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsUpdater;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.GroupBox gbRadioType;
        private System.Windows.Forms.RadioButton rbMDUV380;
        private System.Windows.Forms.RadioButton rbMD9600;
        private System.Windows.Forms.ProgressBar pbUploading;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ToolStripButton tbHelp;
        private System.Windows.Forms.OpenFileDialog ofdOpenFirmware;
    }
}