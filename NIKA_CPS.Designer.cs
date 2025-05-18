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
            System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("Узел5");
            System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("Настройки", new System.Windows.Forms.TreeNode[] {
            treeNode70});
            System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("Узел6");
            System.Windows.Forms.TreeNode treeNode73 = new System.Windows.Forms.TreeNode("Узел7");
            System.Windows.Forms.TreeNode treeNode74 = new System.Windows.Forms.TreeNode("Узел8");
            System.Windows.Forms.TreeNode treeNode75 = new System.Windows.Forms.TreeNode("Контакты", new System.Windows.Forms.TreeNode[] {
            treeNode72,
            treeNode73,
            treeNode74});
            System.Windows.Forms.TreeNode treeNode76 = new System.Windows.Forms.TreeNode("Узел20");
            System.Windows.Forms.TreeNode treeNode77 = new System.Windows.Forms.TreeNode("Узел21");
            System.Windows.Forms.TreeNode treeNode78 = new System.Windows.Forms.TreeNode("Узел22");
            System.Windows.Forms.TreeNode treeNode79 = new System.Windows.Forms.TreeNode("Списки групп", new System.Windows.Forms.TreeNode[] {
            treeNode76,
            treeNode77,
            treeNode78});
            System.Windows.Forms.TreeNode treeNode80 = new System.Windows.Forms.TreeNode("Узел9");
            System.Windows.Forms.TreeNode treeNode81 = new System.Windows.Forms.TreeNode("Узел10");
            System.Windows.Forms.TreeNode treeNode82 = new System.Windows.Forms.TreeNode("Узел11");
            System.Windows.Forms.TreeNode treeNode83 = new System.Windows.Forms.TreeNode("Зоны", new System.Windows.Forms.TreeNode[] {
            treeNode80,
            treeNode81,
            treeNode82});
            System.Windows.Forms.TreeNode treeNode84 = new System.Windows.Forms.TreeNode("Узел12");
            System.Windows.Forms.TreeNode treeNode85 = new System.Windows.Forms.TreeNode("Узел13");
            System.Windows.Forms.TreeNode treeNode86 = new System.Windows.Forms.TreeNode("Узел14");
            System.Windows.Forms.TreeNode treeNode87 = new System.Windows.Forms.TreeNode("Узел15");
            System.Windows.Forms.TreeNode treeNode88 = new System.Windows.Forms.TreeNode("Узел16");
            System.Windows.Forms.TreeNode treeNode89 = new System.Windows.Forms.TreeNode("Каналы", new System.Windows.Forms.TreeNode[] {
            treeNode84,
            treeNode85,
            treeNode86,
            treeNode87,
            treeNode88});
            System.Windows.Forms.TreeNode treeNode90 = new System.Windows.Forms.TreeNode("Узел17");
            System.Windows.Forms.TreeNode treeNode91 = new System.Windows.Forms.TreeNode("Узел18");
            System.Windows.Forms.TreeNode treeNode92 = new System.Windows.Forms.TreeNode("VFO", new System.Windows.Forms.TreeNode[] {
            treeNode90,
            treeNode91});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsMainControls = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.tsbNewFile = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveFile = new System.Windows.Forms.ToolStripButton();
            this.tsbReadFromRadio = new System.Windows.Forms.ToolStripButton();
            this.tsbWriteToRadio = new System.Windows.Forms.ToolStripButton();
            this.tsbEngineering = new System.Windows.Forms.ToolStripButton();
            this.tsbFirmware = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsbMenuToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.msiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msiCodeplug = new System.Windows.Forms.ToolStripMenuItem();
            this.msiReadWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.msiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCSV = new System.Windows.Forms.ToolStripButton();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.tsMainControls.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMainControls
            // 
            this.tsMainControls.BackColor = System.Drawing.Color.White;
            this.tsMainControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMainControls.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsMainControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMenuToggle,
            this.toolStripSeparator4,
            this.tsbNewFile,
            this.tsbOpenFile,
            this.tsbSaveFile,
            this.tsbCSV,
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
            this.tsMainControls.Location = new System.Drawing.Point(6, 6);
            this.tsMainControls.Name = "tsMainControls";
            this.tsMainControls.Size = new System.Drawing.Size(53, 582);
            this.tsMainControls.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(50, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(50, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(50, 6);
            // 
            // tvMain
            // 
            this.tvMain.BackColor = System.Drawing.SystemColors.Window;
            this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvMain.Location = new System.Drawing.Point(59, 6);
            this.tvMain.Margin = new System.Windows.Forms.Padding(8, 8, 4, 3);
            this.tvMain.Name = "tvMain";
            treeNode70.Name = "Узел5";
            treeNode70.Text = "Узел5";
            treeNode71.Name = "Узел0";
            treeNode71.Text = "Настройки";
            treeNode72.Name = "Узел6";
            treeNode72.Text = "Узел6";
            treeNode73.Name = "Узел7";
            treeNode73.Text = "Узел7";
            treeNode74.Name = "Узел8";
            treeNode74.Text = "Узел8";
            treeNode75.Name = "Узел1";
            treeNode75.Text = "Контакты";
            treeNode76.Name = "Узел20";
            treeNode76.Text = "Узел20";
            treeNode77.Name = "Узел21";
            treeNode77.Text = "Узел21";
            treeNode78.Name = "Узел22";
            treeNode78.Text = "Узел22";
            treeNode79.Name = "Узел19";
            treeNode79.Text = "Списки групп";
            treeNode80.Name = "Узел9";
            treeNode80.Text = "Узел9";
            treeNode81.Name = "Узел10";
            treeNode81.Text = "Узел10";
            treeNode82.Name = "Узел11";
            treeNode82.Text = "Узел11";
            treeNode83.Name = "Узел2";
            treeNode83.Text = "Зоны";
            treeNode84.Name = "Узел12";
            treeNode84.Text = "Узел12";
            treeNode85.Name = "Узел13";
            treeNode85.Text = "Узел13";
            treeNode86.Name = "Узел14";
            treeNode86.Text = "Узел14";
            treeNode87.Name = "Узел15";
            treeNode87.Text = "Узел15";
            treeNode88.Name = "Узел16";
            treeNode88.Text = "Узел16";
            treeNode89.Name = "Узел3";
            treeNode89.Text = "Каналы";
            treeNode90.Name = "Узел17";
            treeNode90.Text = "Узел17";
            treeNode91.Name = "Узел18";
            treeNode91.Text = "Узел18";
            treeNode92.Name = "Узел4";
            treeNode92.Text = "VFO";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode71,
            treeNode75,
            treeNode79,
            treeNode83,
            treeNode89,
            treeNode92});
            this.tvMain.Size = new System.Drawing.Size(1103, 582);
            this.tvMain.TabIndex = 1;
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
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(52, 52);
            this.tsbSettings.Text = "Настройки приложения";
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(52, 52);
            this.tsbAbout.Text = "О приложении";
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.Color.White;
            this.msMain.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiFile,
            this.msiCodeplug,
            this.msiReadWrite,
            this.msiTools,
            this.msiAbout});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1168, 27);
            this.msMain.TabIndex = 2;
            this.msMain.Text = "menuStrip1";
            this.msMain.Visible = false;
            // 
            // tsbMenuToggle
            // 
            this.tsbMenuToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMenuToggle.Image = ((System.Drawing.Image)(resources.GetObject("tsbMenuToggle.Image")));
            this.tsbMenuToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMenuToggle.Name = "tsbMenuToggle";
            this.tsbMenuToggle.Size = new System.Drawing.Size(50, 52);
            this.tsbMenuToggle.Text = "Строка меню";
            this.tsbMenuToggle.Click += new System.EventHandler(this.tsbMenuToggle_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(50, 6);
            // 
            // msiFile
            // 
            this.msiFile.Name = "msiFile";
            this.msiFile.Size = new System.Drawing.Size(59, 23);
            this.msiFile.Text = "Файл";
            // 
            // msiCodeplug
            // 
            this.msiCodeplug.Name = "msiCodeplug";
            this.msiCodeplug.Size = new System.Drawing.Size(84, 23);
            this.msiCodeplug.Text = "Кодплаг";
            // 
            // msiReadWrite
            // 
            this.msiReadWrite.Name = "msiReadWrite";
            this.msiReadWrite.Size = new System.Drawing.Size(170, 23);
            this.msiReadWrite.Text = "Программирование";
            // 
            // msiTools
            // 
            this.msiTools.Name = "msiTools";
            this.msiTools.Size = new System.Drawing.Size(124, 23);
            this.msiTools.Text = "Инструменты";
            // 
            // msiAbout
            // 
            this.msiAbout.Name = "msiAbout";
            this.msiAbout.Size = new System.Drawing.Size(120, 23);
            this.msiAbout.Text = "О программе";
            // 
            // tsbCSV
            // 
            this.tsbCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCSV.Image = ((System.Drawing.Image)(resources.GetObject("tsbCSV.Image")));
            this.tsbCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCSV.Name = "tsbCSV";
            this.tsbCSV.Size = new System.Drawing.Size(50, 52);
            this.tsbCSV.Text = "Импорт и экспорт из CSV";
            // 
            // tbConsole
            // 
            this.tbConsole.BackColor = System.Drawing.Color.White;
            this.tbConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbConsole.Location = new System.Drawing.Point(59, 518);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ReadOnly = true;
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(1103, 70);
            this.tbConsole.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1168, 594);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.tsMainControls);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.tsMainControls.ResumeLayout(false);
            this.tsMainControls.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
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
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripButton tsbMenuToggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem msiFile;
        private System.Windows.Forms.ToolStripMenuItem msiCodeplug;
        private System.Windows.Forms.ToolStripMenuItem msiReadWrite;
        private System.Windows.Forms.ToolStripMenuItem msiTools;
        private System.Windows.Forms.ToolStripMenuItem msiAbout;
        private System.Windows.Forms.ToolStripButton tsbCSV;
        private System.Windows.Forms.TextBox tbConsole;
    }
}

