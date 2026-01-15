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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Узел0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Контакты", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Узел20");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Списки групп", 6, 6, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Узел9");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Зоны", 7, 7, new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Узел13");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Каналы", 8, 8, new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("VFO A", 9, 9);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("VFO B", 9, 9);
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("VFO", 9, 9, new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Настройки", 1, 1);
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Узел5");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Узел6");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Узел7");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Узел8");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Узел9");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Радиолюбительские спутники", 10, 10, new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            this.cmsAllContacts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSimilar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAllSatellites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiReloadLocalSatellites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReloadFromNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearSatellites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMainControls = new System.Windows.Forms.ToolStrip();
            this.tsbMenuToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewFile = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveFile = new System.Windows.Forms.ToolStripButton();
            this.tsbCSV = new System.Windows.Forms.ToolStripButton();
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
            this.ilTreeItems = new System.Windows.Forms.ImageList(this.components);
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msiCodeplug = new System.Windows.Forms.ToolStripMenuItem();
            this.msiStdChannelsGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.msiReadWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.msiReadFromRadio = new System.Windows.Forms.ToolStripMenuItem();
            this.msiWriteToRadio = new System.Windows.Forms.ToolStripMenuItem();
            this.msiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msiDMRMon = new System.Windows.Forms.ToolStripMenuItem();
            this.msiCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.pollingTimer = new System.Windows.Forms.Timer(this.components);
            this.cmsSingleContact = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteContact = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSingleSatellite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteSatellite = new System.Windows.Forms.ToolStripMenuItem();
            this.tvSecondary = new System.Windows.Forms.TreeView();
            this.cmsAllChannels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSortChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAllContacts.SuspendLayout();
            this.cmsAllSatellites.SuspendLayout();
            this.tsMainControls.SuspendLayout();
            this.msMain.SuspendLayout();
            this.cmsSingleContact.SuspendLayout();
            this.cmsSingleSatellite.SuspendLayout();
            this.cmsAllChannels.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsAllContacts
            // 
            this.cmsAllContacts.BackColor = System.Drawing.Color.White;
            this.cmsAllContacts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewContact,
            this.tsmiArrange,
            this.tsmiDeleteSimilar});
            this.cmsAllContacts.Name = "cmsAllContacts";
            this.cmsAllContacts.Size = new System.Drawing.Size(275, 76);
            // 
            // tsmiNewContact
            // 
            this.tsmiNewContact.Name = "tsmiNewContact";
            this.tsmiNewContact.Size = new System.Drawing.Size(274, 24);
            this.tsmiNewContact.Text = "Новый";
            this.tsmiNewContact.Click += new System.EventHandler(this.tsmiNewContact_Click);
            // 
            // tsmiArrange
            // 
            this.tsmiArrange.Name = "tsmiArrange";
            this.tsmiArrange.Size = new System.Drawing.Size(274, 24);
            this.tsmiArrange.Text = "Упорядочить по алфавиту";
            this.tsmiArrange.Click += new System.EventHandler(this.tsmiArrange_Click);
            // 
            // tsmiDeleteSimilar
            // 
            this.tsmiDeleteSimilar.Name = "tsmiDeleteSimilar";
            this.tsmiDeleteSimilar.Size = new System.Drawing.Size(274, 24);
            this.tsmiDeleteSimilar.Text = "Удалить дубликаты по DMR ID";
            this.tsmiDeleteSimilar.Click += new System.EventHandler(this.tsmiDeleteSimilar_Click);
            // 
            // cmsAllSatellites
            // 
            this.cmsAllSatellites.BackColor = System.Drawing.Color.White;
            this.cmsAllSatellites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReloadLocalSatellites,
            this.tsmiReloadFromNetwork,
            this.tsmiClearSatellites});
            this.cmsAllSatellites.Name = "cmsAllSatellites";
            this.cmsAllSatellites.Size = new System.Drawing.Size(260, 76);
            // 
            // tsmiReloadLocalSatellites
            // 
            this.tsmiReloadLocalSatellites.Name = "tsmiReloadLocalSatellites";
            this.tsmiReloadLocalSatellites.Size = new System.Drawing.Size(259, 24);
            this.tsmiReloadLocalSatellites.Text = "Перезагрузить локально";
            // 
            // tsmiReloadFromNetwork
            // 
            this.tsmiReloadFromNetwork.Name = "tsmiReloadFromNetwork";
            this.tsmiReloadFromNetwork.Size = new System.Drawing.Size(259, 24);
            this.tsmiReloadFromNetwork.Text = "Перезагрузить из Интернета";
            // 
            // tsmiClearSatellites
            // 
            this.tsmiClearSatellites.Name = "tsmiClearSatellites";
            this.tsmiClearSatellites.Size = new System.Drawing.Size(259, 24);
            this.tsmiClearSatellites.Text = "Очистить";
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
            this.tsMainControls.Size = new System.Drawing.Size(53, 675);
            this.tsMainControls.TabIndex = 0;
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
            this.tsbMenuToggle.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(50, 6);
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
            this.tsbNewFile.Click += new System.EventHandler(this.tsbNewFile_Click);
            this.tsbNewFile.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tsbOpenFile
            // 
            this.tsbOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFile.Image")));
            this.tsbOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFile.Name = "tsbOpenFile";
            this.tsbOpenFile.Size = new System.Drawing.Size(50, 52);
            this.tsbOpenFile.Text = "Открыть кодплаг";
            this.tsbOpenFile.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tsbSaveFile
            // 
            this.tsbSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveFile.Image")));
            this.tsbSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveFile.Name = "tsbSaveFile";
            this.tsbSaveFile.Size = new System.Drawing.Size(50, 52);
            this.tsbSaveFile.Text = "Сохранить кодплаг";
            this.tsbSaveFile.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tsbCSV
            // 
            this.tsbCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCSV.Image = ((System.Drawing.Image)(resources.GetObject("tsbCSV.Image")));
            this.tsbCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCSV.Name = "tsbCSV";
            this.tsbCSV.Size = new System.Drawing.Size(50, 52);
            this.tsbCSV.Text = "Импорт и экспорт из CSV";
            this.tsbCSV.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
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
            this.tsbReadFromRadio.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tsbWriteToRadio
            // 
            this.tsbWriteToRadio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWriteToRadio.Image = ((System.Drawing.Image)(resources.GetObject("tsbWriteToRadio.Image")));
            this.tsbWriteToRadio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWriteToRadio.Name = "tsbWriteToRadio";
            this.tsbWriteToRadio.Size = new System.Drawing.Size(50, 52);
            this.tsbWriteToRadio.Text = "Записать в рацию";
            this.tsbWriteToRadio.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
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
            this.tsbEngineering.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
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
            this.tsbFirmware.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
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
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            this.tsbSettings.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(50, 52);
            this.tsbAbout.Text = "О приложении";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            this.tsbAbout.MouseEnter += new System.EventHandler(this.Control_MouseEnter);
            // 
            // tvMain
            // 
            this.tvMain.BackColor = System.Drawing.SystemColors.Window;
            this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvMain.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.ilTreeItems;
            this.tvMain.Location = new System.Drawing.Point(59, 6);
            this.tvMain.Margin = new System.Windows.Forms.Padding(8, 8, 4, 3);
            this.tvMain.Name = "tvMain";
            treeNode1.Name = "Узел0";
            treeNode1.Text = "Узел0";
            treeNode2.ContextMenuStrip = this.cmsAllContacts;
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "ContactsNode";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "Контакты";
            treeNode3.Name = "Узел20";
            treeNode3.Text = "Узел20";
            treeNode4.ImageIndex = 6;
            treeNode4.Name = "GroupListsNode";
            treeNode4.SelectedImageIndex = 6;
            treeNode4.Text = "Списки групп";
            treeNode5.Name = "Узел9";
            treeNode5.Text = "Узел9";
            treeNode6.ImageIndex = 7;
            treeNode6.Name = "ZonesNode";
            treeNode6.SelectedImageIndex = 7;
            treeNode6.Text = "Зоны";
            treeNode7.Name = "Узел13";
            treeNode7.Text = "Узел13";
            treeNode8.ContextMenuStrip = this.cmsAllChannels;
            treeNode8.ImageIndex = 8;
            treeNode8.Name = "ChannelsNode";
            treeNode8.SelectedImageIndex = 8;
            treeNode8.Text = "Каналы";
            treeNode9.ImageIndex = 9;
            treeNode9.Name = "tnVFOA";
            treeNode9.SelectedImageIndex = 9;
            treeNode9.Text = "VFO A";
            treeNode10.ImageIndex = 9;
            treeNode10.Name = "tnVFOB";
            treeNode10.SelectedImageIndex = 9;
            treeNode10.Text = "VFO B";
            treeNode11.ImageIndex = 9;
            treeNode11.Name = "VFONode";
            treeNode11.SelectedImageIndex = 9;
            treeNode11.Text = "VFO";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode6,
            treeNode8,
            treeNode11});
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.ShowNodeToolTips = true;
            this.tvMain.Size = new System.Drawing.Size(337, 605);
            this.tvMain.TabIndex = 1;
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
            this.tvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMain_NodeMouseClick);
            this.tvMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMain_NodeMouseDoubleClick);
            // 
            // ilTreeItems
            // 
            this.ilTreeItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeItems.ImageStream")));
            this.ilTreeItems.TransparentColor = System.Drawing.Color.White;
            this.ilTreeItems.Images.SetKeyName(0, "empty.png");
            this.ilTreeItems.Images.SetKeyName(1, "settings_tree.png");
            this.ilTreeItems.Images.SetKeyName(2, "address-book.png");
            this.ilTreeItems.Images.SetKeyName(3, "user.png");
            this.ilTreeItems.Images.SetKeyName(4, "group.png");
            this.ilTreeItems.Images.SetKeyName(5, "worldwide.png");
            this.ilTreeItems.Images.SetKeyName(6, "grouplist.png");
            this.ilTreeItems.Images.SetKeyName(7, "zone.png");
            this.ilTreeItems.Images.SetKeyName(8, "channels.png");
            this.ilTreeItems.Images.SetKeyName(9, "vfo.png");
            this.ilTreeItems.Images.SetKeyName(10, "satellite.png");
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
            this.msMain.Location = new System.Drawing.Point(6, 6);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1156, 27);
            this.msMain.TabIndex = 2;
            this.msMain.Text = "menuStrip1";
            this.msMain.Visible = false;
            // 
            // msiFile
            // 
            this.msiFile.Name = "msiFile";
            this.msiFile.Size = new System.Drawing.Size(59, 23);
            this.msiFile.Text = "Файл";
            // 
            // msiCodeplug
            // 
            this.msiCodeplug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiStdChannelsGenerator});
            this.msiCodeplug.Name = "msiCodeplug";
            this.msiCodeplug.Size = new System.Drawing.Size(84, 23);
            this.msiCodeplug.Text = "Кодплаг";
            // 
            // msiStdChannelsGenerator
            // 
            this.msiStdChannelsGenerator.BackColor = System.Drawing.Color.White;
            this.msiStdChannelsGenerator.Name = "msiStdChannelsGenerator";
            this.msiStdChannelsGenerator.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.msiStdChannelsGenerator.Size = new System.Drawing.Size(395, 24);
            this.msiStdChannelsGenerator.Text = "Генератор стандартных каналов";
            this.msiStdChannelsGenerator.Click += new System.EventHandler(this.msiStdChannelsGenerator_Click);
            // 
            // msiReadWrite
            // 
            this.msiReadWrite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiReadFromRadio,
            this.msiWriteToRadio});
            this.msiReadWrite.Name = "msiReadWrite";
            this.msiReadWrite.Size = new System.Drawing.Size(170, 23);
            this.msiReadWrite.Text = "Программирование";
            // 
            // msiReadFromRadio
            // 
            this.msiReadFromRadio.BackColor = System.Drawing.Color.White;
            this.msiReadFromRadio.Name = "msiReadFromRadio";
            this.msiReadFromRadio.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.msiReadFromRadio.Size = new System.Drawing.Size(244, 24);
            this.msiReadFromRadio.Text = "Считать из рации";
            // 
            // msiWriteToRadio
            // 
            this.msiWriteToRadio.BackColor = System.Drawing.Color.White;
            this.msiWriteToRadio.Name = "msiWriteToRadio";
            this.msiWriteToRadio.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.msiWriteToRadio.Size = new System.Drawing.Size(244, 24);
            this.msiWriteToRadio.Text = "Записать в рацию";
            // 
            // msiTools
            // 
            this.msiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiDMRMon,
            this.msiCalibration});
            this.msiTools.Name = "msiTools";
            this.msiTools.Size = new System.Drawing.Size(124, 23);
            this.msiTools.Text = "Инструменты";
            // 
            // msiDMRMon
            // 
            this.msiDMRMon.Name = "msiDMRMon";
            this.msiDMRMon.Size = new System.Drawing.Size(244, 24);
            this.msiDMRMon.Text = "Монитор DMR";
            this.msiDMRMon.Click += new System.EventHandler(this.msiDMRMon_Click);
            // 
            // msiCalibration
            // 
            this.msiCalibration.Name = "msiCalibration";
            this.msiCalibration.Size = new System.Drawing.Size(244, 24);
            this.msiCalibration.Text = "Редактор калибровок";
            this.msiCalibration.Click += new System.EventHandler(this.msiCalibration_Click);
            // 
            // msiAbout
            // 
            this.msiAbout.Name = "msiAbout";
            this.msiAbout.Size = new System.Drawing.Size(120, 23);
            this.msiAbout.Text = "О программе";
            // 
            // tbConsole
            // 
            this.tbConsole.BackColor = System.Drawing.Color.White;
            this.tbConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbConsole.Location = new System.Drawing.Point(59, 611);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ReadOnly = true;
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(1103, 70);
            this.tbConsole.TabIndex = 3;
            // 
            // pollingTimer
            // 
            this.pollingTimer.Interval = 500;
            this.pollingTimer.Tick += new System.EventHandler(this.pollingTimer_Tick);
            // 
            // cmsSingleContact
            // 
            this.cmsSingleContact.BackColor = System.Drawing.Color.White;
            this.cmsSingleContact.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteContact});
            this.cmsSingleContact.Name = "cmsSingleContact";
            this.cmsSingleContact.Size = new System.Drawing.Size(130, 28);
            // 
            // tsmiDeleteContact
            // 
            this.tsmiDeleteContact.Name = "tsmiDeleteContact";
            this.tsmiDeleteContact.Size = new System.Drawing.Size(129, 24);
            this.tsmiDeleteContact.Text = "Удалить";
            this.tsmiDeleteContact.Click += new System.EventHandler(this.tsmiDeleteContact_Click);
            // 
            // cmsSingleSatellite
            // 
            this.cmsSingleSatellite.BackColor = System.Drawing.Color.White;
            this.cmsSingleSatellite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteSatellite});
            this.cmsSingleSatellite.Name = "cmsSingleSatellite";
            this.cmsSingleSatellite.Size = new System.Drawing.Size(130, 28);
            // 
            // tsmiDeleteSatellite
            // 
            this.tsmiDeleteSatellite.Name = "tsmiDeleteSatellite";
            this.tsmiDeleteSatellite.Size = new System.Drawing.Size(129, 24);
            this.tsmiDeleteSatellite.Text = "Удалить";
            // 
            // tvSecondary
            // 
            this.tvSecondary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSecondary.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvSecondary.ImageIndex = 0;
            this.tvSecondary.ImageList = this.ilTreeItems;
            this.tvSecondary.Location = new System.Drawing.Point(396, 6);
            this.tvSecondary.Name = "tvSecondary";
            treeNode12.ImageIndex = 1;
            treeNode12.Name = "SettingsNode";
            treeNode12.SelectedImageIndex = 1;
            treeNode12.Text = "Настройки";
            treeNode13.Name = "Узел5";
            treeNode13.Text = "Узел5";
            treeNode14.Name = "Узел6";
            treeNode14.Text = "Узел6";
            treeNode15.Name = "Узел7";
            treeNode15.Text = "Узел7";
            treeNode16.Name = "Узел8";
            treeNode16.Text = "Узел8";
            treeNode17.Name = "Узел9";
            treeNode17.Text = "Узел9";
            treeNode18.ImageIndex = 10;
            treeNode18.Name = "SatellitesNode";
            treeNode18.SelectedImageIndex = 10;
            treeNode18.Text = "Радиолюбительские спутники";
            this.tvSecondary.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode18});
            this.tvSecondary.SelectedImageIndex = 0;
            this.tvSecondary.Size = new System.Drawing.Size(347, 605);
            this.tvSecondary.TabIndex = 4;
            this.tvSecondary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSecondary_AfterSelect);
            this.tvSecondary.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSecondary_NodeMouseClick);
            this.tvSecondary.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSecondary_NodeMouseDoubleClick);
            // 
            // cmsAllChannels
            // 
            this.cmsAllChannels.BackColor = System.Drawing.Color.White;
            this.cmsAllChannels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSortChannels});
            this.cmsAllChannels.Name = "cmsAllChannels";
            this.cmsAllChannels.Size = new System.Drawing.Size(246, 28);
            // 
            // tsmiSortChannels
            // 
            this.tsmiSortChannels.Name = "tsmiSortChannels";
            this.tsmiSortChannels.Size = new System.Drawing.Size(245, 24);
            this.tsmiSortChannels.Text = "Упорядочить по алфавиту";
            this.tsmiSortChannels.Click += new System.EventHandler(this.tsmiSortChannels_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1168, 687);
            this.Controls.Add(this.tvSecondary);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.tsMainControls);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.cmsAllContacts.ResumeLayout(false);
            this.cmsAllSatellites.ResumeLayout(false);
            this.tsMainControls.ResumeLayout(false);
            this.tsMainControls.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.cmsSingleContact.ResumeLayout(false);
            this.cmsSingleSatellite.ResumeLayout(false);
            this.cmsAllChannels.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem msiDMRMon;
        private System.Windows.Forms.Timer pollingTimer;
        private System.Windows.Forms.ToolStripMenuItem msiCalibration;
        private System.Windows.Forms.ContextMenuStrip cmsAllContacts;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewContact;
        private System.Windows.Forms.ToolStripMenuItem tsmiArrange;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSimilar;
        private System.Windows.Forms.ContextMenuStrip cmsSingleContact;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteContact;
        private System.Windows.Forms.ImageList ilTreeItems;
        private System.Windows.Forms.ContextMenuStrip cmsAllSatellites;
        private System.Windows.Forms.ToolStripMenuItem tsmiReloadLocalSatellites;
        private System.Windows.Forms.ToolStripMenuItem tsmiReloadFromNetwork;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearSatellites;
        private System.Windows.Forms.ContextMenuStrip cmsSingleSatellite;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSatellite;
        private System.Windows.Forms.TreeView tvSecondary;
        private System.Windows.Forms.ToolStripMenuItem msiStdChannelsGenerator;
        private System.Windows.Forms.ToolStripMenuItem msiReadFromRadio;
        private System.Windows.Forms.ToolStripMenuItem msiWriteToRadio;
        private System.Windows.Forms.ContextMenuStrip cmsAllChannels;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortChannels;
    }
}

