namespace NIKA_CPS_V1
{
    partial class Channel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Channel));
            this.gbAnalog = new System.Windows.Forms.GroupBox();
            this.gbDigital = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gbAnalog
            // 
            this.gbAnalog.Location = new System.Drawing.Point(16, 46);
            this.gbAnalog.Name = "gbAnalog";
            this.gbAnalog.Size = new System.Drawing.Size(405, 456);
            this.gbAnalog.TabIndex = 0;
            this.gbAnalog.TabStop = false;
            this.gbAnalog.Text = "Аналоговый режим";
            // 
            // gbDigital
            // 
            this.gbDigital.Location = new System.Drawing.Point(437, 46);
            this.gbDigital.Name = "gbDigital";
            this.gbDigital.Size = new System.Drawing.Size(484, 455);
            this.gbDigital.TabIndex = 1;
            this.gbDigital.TabStop = false;
            this.gbDigital.Text = "Цифровой режим";
            // 
            // Channel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 544);
            this.Controls.Add(this.gbDigital);
            this.Controls.Add(this.gbAnalog);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Channel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Channel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAnalog;
        private System.Windows.Forms.GroupBox gbDigital;
    }
}