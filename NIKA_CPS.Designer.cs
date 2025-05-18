namespace NIKA_CPS_V1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Узел5");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Настройки", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Узел6");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Узел7");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Узел8");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Контакты", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Узел9");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Узел10");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Узел11");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Зоны", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Узел12");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Узел13");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Узел14");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Узел15");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Узел16");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Каналы", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Узел17");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Узел18");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("VFO", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            this.tsMainControls = new System.Windows.Forms.ToolStrip();
            this.tsbNewFile = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbReadFromRadio = new System.Windows.Forms.ToolStripButton();
            this.tsbWriteToRadio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEngineering = new System.Windows.Forms.ToolStripButton();
            this.tsbFirmware = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.tsMainControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMainControls
            // 
            this.tsMainControls.BackColor = System.Drawing.Color.White;
            this.tsMainControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMainControls.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsMainControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewFile,
            this.tsbOpenFile,
            this.tsbSaveFile,
            this.toolStripSeparator1,
            this.tsbReadFromRadio,
            this.tsbWriteToRadio,
            this.toolStripSeparator2,
            this.tsbEngineering,
            this.tsbFirmware,
            this.toolStripSeparator3,
            this.tsbSettings,
            this.tsbAbout});
            this.tsMainControls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMainControls.Location = new System.Drawing.Point(0, 0);
            this.tsMainControls.Name = "tsMainControls";
            this.tsMainControls.Size = new System.Drawing.Size(53, 608);
            this.tsMainControls.TabIndex = 0;
            // 
            // tsbNewFile
            // 
            this.tsbNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewFile.Image")));
            this.tsbNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewFile.Name = "tsbNewFile";
            this.tsbNewFile.Size = new System.Drawing.Size(50, 52);
            this.tsbNewFile.Text = "Новый кодплаг";
            this.tsbNewFile.ToolTipText = "Новый кодплаг";
            // 
            // tsbOpenFile
            // 
            this.tsbOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFile.Image")));
            this.tsbOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFile.Name = "tsbOpenFile";
            this.tsbOpenFile.Size = new System.Drawing.Size(50, 52);
            this.tsbOpenFile.Text = "Открыть кодплаг";
            // 
            // tsbSaveFile
            // 
            this.tsbSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveFile.Image")));
            this.tsbSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveFile.Name = "tsbSaveFile";
            this.tsbSaveFile.Size = new System.Drawing.Size(50, 52);
            this.tsbSaveFile.Text = "Сохранить кодплаг";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(50, 6);
            // 
            // tsbReadFromRadio
            // 
            this.tsbReadFromRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReadFromRadio.Image = ((System.Drawing.Image)(resources.GetObject("tsbReadFromRadio.Image")));
            this.tsbReadFromRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReadFromRadio.Name = "tsbReadFromRadio";
            this.tsbReadFromRadio.Size = new System.Drawing.Size(50, 52);
            this.tsbReadFromRadio.Text = "Считать данные из рации";
            // 
            // tsbWriteToRadio
            // 
            this.tsbWriteToRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWriteToRadio.Image = ((System.Drawing.Image)(resources.GetObject("tsbWriteToRadio.Image")));
            this.tsbWriteToRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWriteToRadio.Name = "tsbWriteToRadio";
            this.tsbWriteToRadio.Size = new System.Drawing.Size(50, 52);
            this.tsbWriteToRadio.Text = "Записать в рацию";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(50, 6);
            // 
            // tsbEngineering
            // 
            this.tsbEngineering.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEngineering.Image = ((System.Drawing.Image)(resources.GetObject("tsbEngineering.Image")));
            this.tsbEngineering.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEngineering.Name = "tsbEngineering";
            this.tsbEngineering.Size = new System.Drawing.Size(50, 52);
            this.tsbEngineering.Text = "Технический центр";
            // 
            // tsbFirmware
            // 
            this.tsbFirmware.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFirmware.Image = ((System.Drawing.Image)(resources.GetObject("tsbFirmware.Image")));
            this.tsbFirmware.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFirmware.Name = "tsbFirmware";
            this.tsbFirmware.Size = new System.Drawing.Size(50, 52);
            this.tsbFirmware.Text = "Обновление прошивки";
            this.tsbFirmware.Click += new System.EventHandler(this.tsbFirmware_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(50, 6);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(50, 52);
            this.tsbSettings.Text = "Настройки приложения";
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(50, 52);
            this.tsbAbout.Text = "О приложении";
            // 
            // tvMain
            // 
            this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvMain.Location = new System.Drawing.Point(53, 0);
            this.tvMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tvMain.Name = "tvMain";
            treeNode1.Name = "Узел5";
            treeNode1.Text = "Узел5";
            treeNode2.Name = "Узел0";
            treeNode2.Text = "Настройки";
            treeNode3.Name = "Узел6";
            treeNode3.Text = "Узел6";
            treeNode4.Name = "Узел7";
            treeNode4.Text = "Узел7";
            treeNode5.Name = "Узел8";
            treeNode5.Text = "Узел8";
            treeNode6.Name = "Узел1";
            treeNode6.Text = "Контакты";
            treeNode7.Name = "Узел9";
            treeNode7.Text = "Узел9";
            treeNode8.Name = "Узел10";
            treeNode8.Text = "Узел10";
            treeNode9.Name = "Узел11";
            treeNode9.Text = "Узел11";
            treeNode10.Name = "Узел2";
            treeNode10.Text = "Зоны";
            treeNode11.Name = "Узел12";
            treeNode11.Text = "Узел12";
            treeNode12.Name = "Узел13";
            treeNode12.Text = "Узел13";
            treeNode13.Name = "Узел14";
            treeNode13.Text = "Узел14";
            treeNode14.Name = "Узел15";
            treeNode14.Text = "Узел15";
            treeNode15.Name = "Узел16";
            treeNode15.Text = "Узел16";
            treeNode16.Name = "Узел3";
            treeNode16.Text = "Каналы";
            treeNode17.Name = "Узел17";
            treeNode17.Text = "Узел17";
            treeNode18.Name = "Узел18";
            treeNode18.Text = "Узел18";
            treeNode19.Name = "Узел4";
            treeNode19.Text = "VFO";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode6,
            treeNode10,
            treeNode16,
            treeNode19});
            this.tvMain.Size = new System.Drawing.Size(1114, 608);
            this.tvMain.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1167, 608);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.tsMainControls);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tsMainControls.ResumeLayout(false);
            this.tsMainControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMainControls;
        private System.Windows.Forms.ToolStripButton tsbNewFile;
        private System.Windows.Forms.ToolStripButton tsbOpenFile;
        private System.Windows.Forms.ToolStripButton tsbSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbReadFromRadio;
        private System.Windows.Forms.ToolStripButton tsbWriteToRadio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbEngineering;
        private System.Windows.Forms.ToolStripButton tsbFirmware;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.TreeView tvMain;
    }
}

