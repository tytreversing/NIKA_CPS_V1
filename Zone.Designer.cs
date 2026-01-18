namespace NIKA_CPS_V1
{
    partial class Zone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zone));
            this.lbUsedChannels = new System.Windows.Forms.ListBox();
            this.lbAvailableChannels = new System.Windows.Forms.ListBox();
            this.lCounter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bAdd = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbUsedChannels
            // 
            this.lbUsedChannels.FormattingEnabled = true;
            this.lbUsedChannels.ItemHeight = 15;
            this.lbUsedChannels.Location = new System.Drawing.Point(24, 31);
            this.lbUsedChannels.Name = "lbUsedChannels";
            this.lbUsedChannels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbUsedChannels.Size = new System.Drawing.Size(307, 469);
            this.lbUsedChannels.TabIndex = 0;
            // 
            // lbAvailableChannels
            // 
            this.lbAvailableChannels.FormattingEnabled = true;
            this.lbAvailableChannels.ItemHeight = 15;
            this.lbAvailableChannels.Location = new System.Drawing.Point(600, 31);
            this.lbAvailableChannels.MultiColumn = true;
            this.lbAvailableChannels.Name = "lbAvailableChannels";
            this.lbAvailableChannels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAvailableChannels.Size = new System.Drawing.Size(471, 469);
            this.lbAvailableChannels.TabIndex = 1;
            // 
            // lCounter
            // 
            this.lCounter.AutoSize = true;
            this.lCounter.Location = new System.Drawing.Point(21, 9);
            this.lCounter.Name = "lCounter";
            this.lCounter.Size = new System.Drawing.Size(163, 15);
            this.lCounter.TabIndex = 2;
            this.lCounter.Text = "Каналы в зоне (лимит: 80)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(597, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Все каналы:";
            // 
            // bAdd
            // 
            this.bAdd.AutoSize = true;
            this.bAdd.Location = new System.Drawing.Point(382, 71);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(152, 30);
            this.bAdd.TabIndex = 4;
            this.bAdd.Text = "<- Добавить";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bRemove
            // 
            this.bRemove.AutoSize = true;
            this.bRemove.Location = new System.Drawing.Point(382, 117);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(152, 30);
            this.bRemove.TabIndex = 5;
            this.bRemove.Text = "-> Убрать";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(382, 452);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(152, 30);
            this.bOK.TabIndex = 6;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(355, 31);
            this.tbName.MaxLength = 16;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(214, 23);
            this.tbName.TabIndex = 7;
            this.tbName.WordWrap = false;
            // 
            // Zone
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1103, 519);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lCounter);
            this.Controls.Add(this.lbAvailableChannels);
            this.Controls.Add(this.lbUsedChannels);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Zone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование зоны";
            this.Load += new System.EventHandler(this.Zone_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbUsedChannels;
        private System.Windows.Forms.ListBox lbAvailableChannels;
        private System.Windows.Forms.Label lCounter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.TextBox tbName;
    }
}