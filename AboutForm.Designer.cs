namespace NIKA_CPS_V1
{
    partial class AboutForm
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
            this.bOK = new System.Windows.Forms.Button();
            this.rbAbout = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(83, 624);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(292, 30);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // rbAbout
            // 
            this.rbAbout.BackColor = System.Drawing.Color.White;
            this.rbAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rbAbout.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rbAbout.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbAbout.Location = new System.Drawing.Point(9, 9);
            this.rbAbout.Name = "rbAbout";
            this.rbAbout.ReadOnly = true;
            this.rbAbout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rbAbout.Size = new System.Drawing.Size(448, 609);
            this.rbAbout.TabIndex = 2;
            this.rbAbout.Text = "";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 660);
            this.Controls.Add(this.rbAbout);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.RichTextBox rbAbout;
    }
}