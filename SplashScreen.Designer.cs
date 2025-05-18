namespace NIKA_CPS_V1
{
    partial class SplashScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.tSplash = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tSplash
            // 
            this.tSplash.Interval = 2000;
            this.tSplash.Tick += new System.EventHandler(this.tSplash_Tick);
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.ForeColor = System.Drawing.Color.Blue;
            this.progressBar.Location = new System.Drawing.Point(133, 280);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(174, 12);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 0;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(659, 367);
            this.Controls.Add(this.progressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "НИКА";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Click += new System.EventHandler(this.SplashScreen_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tSplash;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}