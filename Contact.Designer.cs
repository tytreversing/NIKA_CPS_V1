namespace NIKA_CPS_V1
{
    partial class Contact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contact));
            this.label1 = new System.Windows.Forms.Label();
            this.tbAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDMRID = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rbPrivateCall = new System.Windows.Forms.RadioButton();
            this.rbGroupCall = new System.Windows.Forms.RadioButton();
            this.rbAllCall = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTimeslot = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Позывной:";
            // 
            // tbAlias
            // 
            this.tbAlias.Location = new System.Drawing.Point(73, 15);
            this.tbAlias.MaxLength = 16;
            this.tbAlias.Name = "tbAlias";
            this.tbAlias.Size = new System.Drawing.Size(214, 20);
            this.tbAlias.TabIndex = 1;
            this.tbAlias.TextChanged += new System.EventHandler(this.tbAlias_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Данные:";
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(73, 44);
            this.tbData.MaxLength = 32;
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(214, 20);
            this.tbData.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "DMR ID:";
            // 
            // tbDMRID
            // 
            this.tbDMRID.Location = new System.Drawing.Point(73, 73);
            this.tbDMRID.MaxLength = 7;
            this.tbDMRID.Name = "tbDMRID";
            this.tbDMRID.Size = new System.Drawing.Size(214, 20);
            this.tbDMRID.TabIndex = 5;
            this.tbDMRID.WordWrap = false;
            this.tbDMRID.TextChanged += new System.EventHandler(this.tbDMRID_TextChanged);
            this.tbDMRID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDMRID_KeyPress);
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(82, 257);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(144, 22);
            this.bOK.TabIndex = 6;
            this.bOK.Text = "ОК";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Тип:";
            // 
            // rbPrivateCall
            // 
            this.rbPrivateCall.AutoSize = true;
            this.rbPrivateCall.Checked = true;
            this.rbPrivateCall.Location = new System.Drawing.Point(76, 110);
            this.rbPrivateCall.Name = "rbPrivateCall";
            this.rbPrivateCall.Size = new System.Drawing.Size(105, 17);
            this.rbPrivateCall.TabIndex = 8;
            this.rbPrivateCall.TabStop = true;
            this.rbPrivateCall.Text = "Частный вызов";
            this.rbPrivateCall.UseVisualStyleBackColor = true;
            // 
            // rbGroupCall
            // 
            this.rbGroupCall.AutoSize = true;
            this.rbGroupCall.Location = new System.Drawing.Point(76, 133);
            this.rbGroupCall.Name = "rbGroupCall";
            this.rbGroupCall.Size = new System.Drawing.Size(113, 17);
            this.rbGroupCall.TabIndex = 9;
            this.rbGroupCall.TabStop = true;
            this.rbGroupCall.Text = "Групповой вызов";
            this.rbGroupCall.UseVisualStyleBackColor = true;
            // 
            // rbAllCall
            // 
            this.rbAllCall.AutoSize = true;
            this.rbAllCall.Location = new System.Drawing.Point(76, 157);
            this.rbAllCall.Name = "rbAllCall";
            this.rbAllCall.Size = new System.Drawing.Size(84, 17);
            this.rbAllCall.TabIndex = 10;
            this.rbAllCall.TabStop = true;
            this.rbAllCall.Text = "Вызов всех";
            this.rbAllCall.UseVisualStyleBackColor = true;
            this.rbAllCall.CheckedChanged += new System.EventHandler(this.rbAllCall_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Таймслот:";
            // 
            // cbTimeslot
            // 
            this.cbTimeslot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeslot.FormattingEnabled = true;
            this.cbTimeslot.Items.AddRange(new object[] {
            "TS1",
            "TS2"});
            this.cbTimeslot.Location = new System.Drawing.Point(73, 197);
            this.cbTimeslot.Name = "cbTimeslot";
            this.cbTimeslot.Size = new System.Drawing.Size(107, 21);
            this.cbTimeslot.TabIndex = 12;
            // 
            // Contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(299, 291);
            this.Controls.Add(this.cbTimeslot);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbAllCall);
            this.Controls.Add(this.rbGroupCall);
            this.Controls.Add(this.rbPrivateCall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tbDMRID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbAlias);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Contact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Контакт";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAlias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDMRID;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbPrivateCall;
        private System.Windows.Forms.RadioButton rbGroupCall;
        private System.Windows.Forms.RadioButton rbAllCall;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTimeslot;
    }
}