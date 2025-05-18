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
            this.tbAbout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbAbout
            // 
            this.tbAbout.BackColor = System.Drawing.Color.White;
            this.tbAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAbout.Enabled = false;
            this.tbAbout.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAbout.ForeColor = System.Drawing.Color.Black;
            this.tbAbout.Location = new System.Drawing.Point(43, 12);
            this.tbAbout.Margin = new System.Windows.Forms.Padding(6);
            this.tbAbout.Multiline = true;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.ReadOnly = true;
            this.tbAbout.Size = new System.Drawing.Size(377, 426);
            this.tbAbout.TabIndex = 0;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 450);
            this.Controls.Add(this.tbAbout);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAbout;
    }
}