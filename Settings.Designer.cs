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
            this.label1 = new System.Windows.Forms.Label();
            this.rbFastPolling = new System.Windows.Forms.RadioButton();
            this.rbSlowPolling = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRadioVID = new System.Windows.Forms.TextBox();
            this.tbRadioPID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbExpandContacts = new System.Windows.Forms.CheckBox();
            this.cbExpandChannels = new System.Windows.Forms.CheckBox();
            this.cbConfirmExit = new System.Windows.Forms.CheckBox();
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
            this.bSaveAppSettings.Location = new System.Drawing.Point(127, 240);
            this.bSaveAppSettings.Name = "bSaveAppSettings";
            this.bSaveAppSettings.Size = new System.Drawing.Size(204, 28);
            this.bSaveAppSettings.TabIndex = 2;
            this.bSaveAppSettings.Text = "Сохранить настройки";
            this.bSaveAppSettings.UseVisualStyleBackColor = false;
            this.bSaveAppSettings.Click += new System.EventHandler(this.bSaveAppSettings_Click);
            this.bSaveAppSettings.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Таймер поллинга";
            // 
            // rbFastPolling
            // 
            this.rbFastPolling.AutoSize = true;
            this.rbFastPolling.Location = new System.Drawing.Point(127, 65);
            this.rbFastPolling.Name = "rbFastPolling";
            this.rbFastPolling.Size = new System.Drawing.Size(80, 19);
            this.rbFastPolling.TabIndex = 4;
            this.rbFastPolling.TabStop = true;
            this.rbFastPolling.Text = "быстрый";
            this.rbFastPolling.UseVisualStyleBackColor = true;
            this.rbFastPolling.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // rbSlowPolling
            // 
            this.rbSlowPolling.AutoSize = true;
            this.rbSlowPolling.Location = new System.Drawing.Point(127, 91);
            this.rbSlowPolling.Name = "rbSlowPolling";
            this.rbSlowPolling.Size = new System.Drawing.Size(95, 19);
            this.rbSlowPolling.TabIndex = 5;
            this.rbSlowPolling.TabStop = true;
            this.rbSlowPolling.Text = "медленный";
            this.rbSlowPolling.UseVisualStyleBackColor = true;
            this.rbSlowPolling.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Разворачивать при запуске:";
            // 
            // cbExpandContacts
            // 
            this.cbExpandContacts.AutoSize = true;
            this.cbExpandContacts.Location = new System.Drawing.Point(274, 39);
            this.cbExpandContacts.Name = "cbExpandContacts";
            this.cbExpandContacts.Size = new System.Drawing.Size(137, 19);
            this.cbExpandContacts.TabIndex = 11;
            this.cbExpandContacts.Text = "Дерево контактов";
            this.cbExpandContacts.UseVisualStyleBackColor = true;
            // 
            // cbExpandChannels
            // 
            this.cbExpandChannels.AutoSize = true;
            this.cbExpandChannels.Location = new System.Drawing.Point(274, 63);
            this.cbExpandChannels.Name = "cbExpandChannels";
            this.cbExpandChannels.Size = new System.Drawing.Size(123, 19);
            this.cbExpandChannels.TabIndex = 12;
            this.cbExpandChannels.Text = "Дерево каналов";
            this.cbExpandChannels.UseVisualStyleBackColor = true;
            // 
            // cbConfirmExit
            // 
            this.cbConfirmExit.AutoSize = true;
            this.cbConfirmExit.Location = new System.Drawing.Point(13, 193);
            this.cbConfirmExit.Name = "cbConfirmExit";
            this.cbConfirmExit.Size = new System.Drawing.Size(279, 19);
            this.cbConfirmExit.TabIndex = 13;
            this.cbConfirmExit.Text = "Запрашивать подтверждение при выходе";
            this.cbConfirmExit.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 278);
            this.Controls.Add(this.cbConfirmExit);
            this.Controls.Add(this.cbExpandChannels);
            this.Controls.Add(this.cbExpandContacts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRadioPID);
            this.Controls.Add(this.tbRadioVID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbSlowPolling);
            this.Controls.Add(this.rbFastPolling);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbFastPolling;
        private System.Windows.Forms.RadioButton rbSlowPolling;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRadioVID;
        private System.Windows.Forms.TextBox tbRadioPID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbExpandContacts;
        private System.Windows.Forms.CheckBox cbExpandChannels;
        private System.Windows.Forms.CheckBox cbConfirmExit;
    }
}