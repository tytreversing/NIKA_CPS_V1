namespace NIKA_CPS_V1
{
    partial class ChannelsGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelsGenerator));
            this.label1 = new System.Windows.Forms.Label();
            this.cbLPD = new System.Windows.Forms.CheckBox();
            this.gbLPD = new System.Windows.Forms.GroupBox();
            this.rbLPD16 = new System.Windows.Forms.RadioButton();
            this.rbLPD32 = new System.Windows.Forms.RadioButton();
            this.rbLPDFull = new System.Windows.Forms.RadioButton();
            this.cbPMR = new System.Windows.Forms.CheckBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbLPD.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите сетки каналов, для которых нужно сгенерировать данные \r\nи записать их в " +
    "кодплаг. Если объем данных превысит свободное место \r\nв кодплаге, часть сгенерир" +
    "ованных данных будет отсечена.";
            // 
            // cbLPD
            // 
            this.cbLPD.AutoSize = true;
            this.cbLPD.Location = new System.Drawing.Point(21, 22);
            this.cbLPD.Name = "cbLPD";
            this.cbLPD.Size = new System.Drawing.Size(185, 19);
            this.cbLPD.TabIndex = 1;
            this.cbLPD.Text = "Генерировать каналы LPD";
            this.cbLPD.UseVisualStyleBackColor = true;
            // 
            // gbLPD
            // 
            this.gbLPD.Controls.Add(this.rbLPDFull);
            this.gbLPD.Controls.Add(this.rbLPD32);
            this.gbLPD.Controls.Add(this.rbLPD16);
            this.gbLPD.Controls.Add(this.cbLPD);
            this.gbLPD.Location = new System.Drawing.Point(17, 58);
            this.gbLPD.Name = "gbLPD";
            this.gbLPD.Size = new System.Drawing.Size(232, 135);
            this.gbLPD.TabIndex = 2;
            this.gbLPD.TabStop = false;
            this.gbLPD.Text = "Каналы LPD";
            // 
            // rbLPD16
            // 
            this.rbLPD16.AutoSize = true;
            this.rbLPD16.Checked = true;
            this.rbLPD16.Location = new System.Drawing.Point(40, 47);
            this.rbLPD16.Name = "rbLPD16";
            this.rbLPD16.Size = new System.Drawing.Size(91, 19);
            this.rbLPD16.TabIndex = 2;
            this.rbLPD16.TabStop = true;
            this.rbLPD16.Text = "Сетка 1-16";
            this.rbLPD16.UseVisualStyleBackColor = true;
            // 
            // rbLPD32
            // 
            this.rbLPD32.AutoSize = true;
            this.rbLPD32.Location = new System.Drawing.Point(40, 73);
            this.rbLPD32.Name = "rbLPD32";
            this.rbLPD32.Size = new System.Drawing.Size(91, 19);
            this.rbLPD32.TabIndex = 3;
            this.rbLPD32.Text = "Сетка 1-32";
            this.rbLPD32.UseVisualStyleBackColor = true;
            // 
            // rbLPDFull
            // 
            this.rbLPDFull.AutoSize = true;
            this.rbLPDFull.Location = new System.Drawing.Point(40, 99);
            this.rbLPDFull.Name = "rbLPDFull";
            this.rbLPDFull.Size = new System.Drawing.Size(107, 19);
            this.rbLPDFull.TabIndex = 4;
            this.rbLPDFull.TabStop = true;
            this.rbLPDFull.Text = "Полная сетка";
            this.rbLPDFull.UseVisualStyleBackColor = true;
            // 
            // cbPMR
            // 
            this.cbPMR.AutoSize = true;
            this.cbPMR.Location = new System.Drawing.Point(271, 81);
            this.cbPMR.Name = "cbPMR";
            this.cbPMR.Size = new System.Drawing.Size(102, 19);
            this.cbPMR.TabIndex = 3;
            this.cbPMR.Text = "Каналы PMR";
            this.cbPMR.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(114, 215);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(215, 30);
            this.bOK.TabIndex = 4;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(114, 252);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(215, 29);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // ChannelsGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 302);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbPMR);
            this.Controls.Add(this.gbLPD);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ChannelsGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Генератор списков каналов";
            this.gbLPD.ResumeLayout(false);
            this.gbLPD.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbLPD;
        private System.Windows.Forms.GroupBox gbLPD;
        private System.Windows.Forms.RadioButton rbLPD32;
        private System.Windows.Forms.RadioButton rbLPD16;
        private System.Windows.Forms.RadioButton rbLPDFull;
        private System.Windows.Forms.CheckBox cbPMR;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
    }
}