namespace NIKA_CPS_V1
{
    partial class Settings
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
            this.cbShowSplashScreen = new System.Windows.Forms.CheckBox();
            this.cbUseVoiceHelp = new System.Windows.Forms.CheckBox();
            this.bSaveAppSettings = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRadioVID = new System.Windows.Forms.TextBox();
            this.tbRadioPID = new System.Windows.Forms.TextBox();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbShowSplashScreen
            // 
            this.cbShowSplashScreen.AutoSize = true;
            this.cbShowSplashScreen.Location = new System.Drawing.Point(13, 13);
            this.cbShowSplashScreen.Name = "cbShowSplashScreen";
            this.cbShowSplashScreen.Size = new System.Drawing.Size(236, 19);
            this.cbShowSplashScreen.TabIndex = 0;
            this.cbShowSplashScreen.Text = "Показывать заставку при запуске";
            this.cbShowSplashScreen.UseVisualStyleBackColor = true;
            this.cbShowSplashScreen.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // cbUseVoiceHelp
            // 
            this.cbUseVoiceHelp.AutoSize = true;
            this.cbUseVoiceHelp.Location = new System.Drawing.Point(13, 39);
            this.cbUseVoiceHelp.Name = "cbUseVoiceHelp";
            this.cbUseVoiceHelp.Size = new System.Drawing.Size(240, 19);
            this.cbUseVoiceHelp.TabIndex = 1;
            this.cbUseVoiceHelp.Text = "Проигрывать голосовые подсказки";
            this.cbUseVoiceHelp.UseVisualStyleBackColor = true;
            this.cbUseVoiceHelp.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // bSaveAppSettings
            // 
            this.bSaveAppSettings.BackColor = System.Drawing.Color.White;
            this.bSaveAppSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSaveAppSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.bSaveAppSettings.FlatAppearance.BorderSize = 2;
            this.bSaveAppSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.bSaveAppSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.bSaveAppSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSaveAppSettings.Location = new System.Drawing.Point(314, 478);
            this.bSaveAppSettings.Name = "bSaveAppSettings";
            this.bSaveAppSettings.Size = new System.Drawing.Size(204, 28);
            this.bSaveAppSettings.TabIndex = 2;
            this.bSaveAppSettings.Text = "Сохранить настройки";
            this.bSaveAppSettings.UseVisualStyleBackColor = false;
            this.bSaveAppSettings.Click += new System.EventHandler(this.bSaveAppSettings_Click);
            this.bSaveAppSettings.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "VID устройства";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "PID устройства";
            // 
            // tbRadioVID
            // 
            this.tbRadioVID.Location = new System.Drawing.Point(122, 119);
            this.tbRadioVID.Name = "tbRadioVID";
            this.tbRadioVID.Size = new System.Drawing.Size(117, 23);
            this.tbRadioVID.TabIndex = 8;
            // 
            // tbRadioPID
            // 
            this.tbRadioPID.Location = new System.Drawing.Point(122, 152);
            this.tbRadioPID.Name = "tbRadioPID";
            this.tbRadioPID.Size = new System.Drawing.Size(117, 23);
            this.tbRadioPID.TabIndex = 9;
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(131, 67);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(155, 23);
            this.cbPorts.Sorted = true;
            this.cbPorts.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Порт соединения";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.tbRadioPID);
            this.Controls.Add(this.tbRadioVID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bSaveAppSettings);
            this.Controls.Add(this.cbUseVoiceHelp);
            this.Controls.Add(this.cbShowSplashScreen);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки программы";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbShowSplashScreen;
        private System.Windows.Forms.CheckBox cbUseVoiceHelp;
        private System.Windows.Forms.Button bSaveAppSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRadioVID;
        private System.Windows.Forms.TextBox tbRadioPID;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.Label label4;
    }
}