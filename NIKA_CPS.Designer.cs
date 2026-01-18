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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Контакты", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Каналы", 8, 8);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Зоны", 7, 7);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Списки групп", 6, 6);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("VFO A", 9, 9);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("VFO B", 9, 9);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("VFO", 9, 9, new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Настройки", 1, 1);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("DMR ID", 13, 13);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Радиолюбительские спутники", 10, 10);
            this.cmsAllContacts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewContact = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSimilar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAllZones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSortZonesByName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClearZones = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAllChannels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSortChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClearChannels = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsmiNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsmiContactUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContactDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteContact = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSingleSatellite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteSatellite = new System.Windows.Forms.ToolStripMenuItem();
            this.tvSecondary = new System.Windows.Forms.TreeView();
            this.sfdCodeplug = new System.Windows.Forms.SaveFileDialog();
            this.ofdCodeplug = new System.Windows.Forms.OpenFileDialog();
            this.cmsSingleChannel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChannelUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChannelDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAllContacts.SuspendLayout();
            this.cmsAllZones.SuspendLayout();
            this.cmsAllChannels.SuspendLayout();
            this.cmsAllSatellites.SuspendLayout();
            this.tsMainControls.SuspendLayout();
            this.msMain.SuspendLayout();
            this.cmsSingleContact.SuspendLayout();
            this.cmsSingleSatellite.SuspendLayout();
            this.cmsSingleChannel.SuspendLayout();
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
            // cmsAllZones
            // 
            this.cmsAllZones.BackColor = System.Drawing.Color.White;
            this.cmsAllZones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewZone,
            this.tsmiSortZonesByName,
            this.toolStripSeparator9,
            this.tsmiClearZones});
            this.cmsAllZones.Name = "cmsAllZones";
            this.cmsAllZones.Size = new System.Drawing.Size(246, 104);
            // 
            // tsmiNewZone
            // 
            this.tsmiNewZone.Name = "tsmiNewZone";
            this.tsmiNewZone.Size = new System.Drawing.Size(245, 24);
            this.tsmiNewZone.Text = "Новая";
            this.tsmiNewZone.Click += new System.EventHandler(this.tsmiNewZone_Click);
            // 
            // tsmiSortZonesByName
            // 
            this.tsmiSortZonesByName.Name = "tsmiSortZonesByName";
            this.tsmiSortZonesByName.Size = new System.Drawing.Size(245, 24);
            this.tsmiSortZonesByName.Text = "Упорядочить по алфавиту";
            this.tsmiSortZonesByName.Click += new System.EventHandler(this.tsmiSortZonesByName_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(242, 6);
            // 
            // tsmiClearZones
            // 
            this.tsmiClearZones.Name = "tsmiClearZones";
            this.tsmiClearZones.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.tsmiClearZones.Size = new System.Drawing.Size(245, 24);
            this.tsmiClearZones.Text = "Очистить";
            // 
            // cmsAllChannels
            // 
            this.cmsAllChannels.BackColor = System.Drawing.Color.White;
            this.cmsAllChannels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewChannel,
            this.tsmiSortChannels,
            this.toolStripSeparator5,
            this.tsmiClearChannels});
            this.cmsAllChannels.Name = "cmsAllChannels";
            this.cmsAllChannels.Size = new System.Drawing.Size(246, 82);
            // 
            // tsmiNewChannel
            // 
            this.tsmiNewChannel.Name = "tsmiNewChannel";
            this.tsmiNewChannel.Size = new System.Drawing.Size(245, 24);
            this.tsmiNewChannel.Text = "Новый";
            // 
            // tsmiSortChannels
            // 
            this.tsmiSortChannels.Name = "tsmiSortChannels";
            this.tsmiSortChannels.Size = new System.Drawing.Size(245, 24);
            this.tsmiSortChannels.Text = "Упорядочить по алфавиту";
            this.tsmiSortChannels.Click += new System.EventHandler(this.tsmiSortChannels_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(242, 6);
            // 
            // tsmiClearChannels
            // 
            this.tsmiClearChannels.Name = "tsmiClearChannels";
            this.tsmiClearChannels.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.tsmiClearChannels.Size = new System.Drawing.Size(245, 24);
            this.tsmiClearChannels.Text = "Очистить";
            this.tsmiClearChannels.Click += new System.EventHandler(this.tsmiClearChannels_Click);
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
            this.tsmiClearSatellites.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
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
            this.tsbOpenFile.Click += new System.EventHandler(this.tsbOpenFile_Click);
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
            this.tsbSaveFile.Click += new System.EventHandler(this.tsbSaveFile_Click);
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
            treeNode1.ContextMenuStrip = this.cmsAllContacts;
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "ContactsNode";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "Контакты";
            treeNode2.ContextMenuStrip = this.cmsAllChannels;
            treeNode2.ImageIndex = 8;
            treeNode2.Name = "ChannelsNode";
            treeNode2.SelectedImageIndex = 8;
            treeNode2.Text = "Каналы";
            treeNode3.ContextMenuStrip = this.cmsAllZones;
            treeNode3.ImageIndex = 7;
            treeNode3.Name = "ZonesNode";
            treeNode3.SelectedImageIndex = 7;
            treeNode3.Text = "Зоны";
            treeNode4.ImageIndex = 6;
            treeNode4.Name = "GroupListsNode";
            treeNode4.SelectedImageIndex = 6;
            treeNode4.Text = "Списки групп";
            treeNode5.ImageIndex = 9;
            treeNode5.Name = "tnVFOA";
            treeNode5.SelectedImageIndex = 9;
            treeNode5.Text = "VFO A";
            treeNode6.ImageIndex = 9;
            treeNode6.Name = "tnVFOB";
            treeNode6.SelectedImageIndex = 9;
            treeNode6.Text = "VFO B";
            treeNode7.ImageIndex = 9;
            treeNode7.Name = "VFONode";
            treeNode7.SelectedImageIndex = 9;
            treeNode7.Text = "VFO";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode7});
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.ShowNodeToolTips = true;
            this.tvMain.Size = new System.Drawing.Size(337, 605);
            this.tvMain.TabIndex = 1;
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
            this.tvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvNodeMouseClick);
            this.tvMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvNodeMouseDoubleClick);
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
            this.ilTreeItems.Images.SetKeyName(11, "analog.png");
            this.ilTreeItems.Images.SetKeyName(12, "digital.png");
            this.ilTreeItems.Images.SetKeyName(13, "id.png");
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
            this.msiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFile,
            this.tsmiOpenFile,
            this.tsmiSaveFile,
            this.tsmiSaveAs,
            this.toolStripSeparator6,
            this.tsmiExit});
            this.msiFile.Name = "msiFile";
            this.msiFile.Size = new System.Drawing.Size(59, 23);
            this.msiFile.Text = "Файл";
            // 
            // tsmiNewFile
            // 
            this.tsmiNewFile.BackColor = System.Drawing.Color.White;
            this.tsmiNewFile.Name = "tsmiNewFile";
            this.tsmiNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNewFile.Size = new System.Drawing.Size(322, 24);
            this.tsmiNewFile.Text = "Новый";
            this.tsmiNewFile.Click += new System.EventHandler(this.tsbNewFile_Click);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.BackColor = System.Drawing.Color.White;
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpenFile.Size = new System.Drawing.Size(322, 24);
            this.tsmiOpenFile.Text = "Открыть";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsbOpenFile_Click);
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.BackColor = System.Drawing.Color.White;
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSaveFile.Size = new System.Drawing.Size(322, 24);
            this.tsmiSaveFile.Text = "Сохранить";
            this.tsmiSaveFile.Click += new System.EventHandler(this.tsbSaveFile_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.BackColor = System.Drawing.Color.White;
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsmiSaveAs.Size = new System.Drawing.Size(322, 24);
            this.tsmiSaveAs.Tag = "SaveAs";
            this.tsmiSaveAs.Text = "Сохранить как...";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsbSaveFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.BackColor = System.Drawing.Color.White;
            this.toolStripSeparator6.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(319, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.BackColor = System.Drawing.Color.White;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiExit.Size = new System.Drawing.Size(322, 24);
            this.tsmiExit.Text = "Выход";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
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
            this.tsmiContactUp,
            this.tsmiContactDown,
            this.toolStripSeparator8,
            this.tsmiDeleteContact});
            this.cmsSingleContact.Name = "cmsSingleContact";
            this.cmsSingleContact.Size = new System.Drawing.Size(195, 82);
            // 
            // tsmiContactUp
            // 
            this.tsmiContactUp.Name = "tsmiContactUp";
            this.tsmiContactUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.tsmiContactUp.Size = new System.Drawing.Size(194, 24);
            this.tsmiContactUp.Text = "Вверх";
            this.tsmiContactUp.Click += new System.EventHandler(this.MoveNodeUpFromMenu);
            // 
            // tsmiContactDown
            // 
            this.tsmiContactDown.Name = "tsmiContactDown";
            this.tsmiContactDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.tsmiContactDown.Size = new System.Drawing.Size(194, 24);
            this.tsmiContactDown.Text = "Вниз";
            this.tsmiContactDown.Click += new System.EventHandler(this.MoveNodeDownFromMenu);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiDeleteContact
            // 
            this.tsmiDeleteContact.Name = "tsmiDeleteContact";
            this.tsmiDeleteContact.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiDeleteContact.Size = new System.Drawing.Size(194, 24);
            this.tsmiDeleteContact.Text = "Удалить";
            this.tsmiDeleteContact.Click += new System.EventHandler(this.tsmiDeleteContact_Click);
            // 
            // cmsSingleSatellite
            // 
            this.cmsSingleSatellite.BackColor = System.Drawing.Color.White;
            this.cmsSingleSatellite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteSatellite});
            this.cmsSingleSatellite.Name = "cmsSingleSatellite";
            this.cmsSingleSatellite.Size = new System.Drawing.Size(184, 28);
            // 
            // tsmiDeleteSatellite
            // 
            this.tsmiDeleteSatellite.Name = "tsmiDeleteSatellite";
            this.tsmiDeleteSatellite.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiDeleteSatellite.Size = new System.Drawing.Size(183, 24);
            this.tsmiDeleteSatellite.Text = "Удалить";
            this.tsmiDeleteSatellite.Click += new System.EventHandler(this.tsmiDeleteSatellite_Click);
            // 
            // tvSecondary
            // 
            this.tvSecondary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSecondary.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvSecondary.ImageIndex = 0;
            this.tvSecondary.ImageList = this.ilTreeItems;
            this.tvSecondary.Location = new System.Drawing.Point(396, 6);
            this.tvSecondary.Name = "tvSecondary";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "SettingsNode";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Text = "Настройки";
            treeNode9.ImageIndex = 13;
            treeNode9.Name = "DMRIDNode";
            treeNode9.SelectedImageIndex = 13;
            treeNode9.Text = "DMR ID";
            treeNode10.ImageIndex = 10;
            treeNode10.Name = "SatellitesNode";
            treeNode10.SelectedImageIndex = 10;
            treeNode10.Text = "Радиолюбительские спутники";
            this.tvSecondary.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10});
            this.tvSecondary.SelectedImageIndex = 0;
            this.tvSecondary.ShowNodeToolTips = true;
            this.tvSecondary.Size = new System.Drawing.Size(347, 605);
            this.tvSecondary.TabIndex = 4;
            this.tvSecondary.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvNodeMouseClick);
            this.tvSecondary.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvNodeMouseDoubleClick);
            // 
            // sfdCodeplug
            // 
            this.sfdCodeplug.DefaultExt = "ncf";
            this.sfdCodeplug.Filter = "Файлы кодплага (.ncf)|*.ncf";
            this.sfdCodeplug.Title = "Сохранить кодплаг";
            // 
            // ofdCodeplug
            // 
            this.ofdCodeplug.DefaultExt = "ncf";
            this.ofdCodeplug.Filter = "Файлы кодплага (.ncf)|*.ncf";
            this.ofdCodeplug.Title = "Открыть файл кодплага";
            // 
            // cmsSingleChannel
            // 
            this.cmsSingleChannel.BackColor = System.Drawing.Color.White;
            this.cmsSingleChannel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChannelUp,
            this.tsmiChannelDown,
            this.toolStripSeparator7,
            this.tsmiDeleteChannel});
            this.cmsSingleChannel.Name = "cmsSingleChannel";
            this.cmsSingleChannel.Size = new System.Drawing.Size(195, 82);
            // 
            // tsmiChannelUp
            // 
            this.tsmiChannelUp.Name = "tsmiChannelUp";
            this.tsmiChannelUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.tsmiChannelUp.Size = new System.Drawing.Size(194, 24);
            this.tsmiChannelUp.Text = "Вверх";
            this.tsmiChannelUp.Click += new System.EventHandler(this.MoveNodeUpFromMenu);
            // 
            // tsmiChannelDown
            // 
            this.tsmiChannelDown.Name = "tsmiChannelDown";
            this.tsmiChannelDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.tsmiChannelDown.Size = new System.Drawing.Size(194, 24);
            this.tsmiChannelDown.Text = "Вниз";
            this.tsmiChannelDown.Click += new System.EventHandler(this.MoveNodeDownFromMenu);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiDeleteChannel
            // 
            this.tsmiDeleteChannel.Name = "tsmiDeleteChannel";
            this.tsmiDeleteChannel.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiDeleteChannel.Size = new System.Drawing.Size(194, 24);
            this.tsmiDeleteChannel.Text = "Удалить";
            this.tsmiDeleteChannel.Click += new System.EventHandler(this.tsmiDeleteChannel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1168, 687);
            this.Controls.Add(this.tvSecondary);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.tsMainControls);
            this.Controls.Add(this.msMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.cmsAllContacts.ResumeLayout(false);
            this.cmsAllZones.ResumeLayout(false);
            this.cmsAllChannels.ResumeLayout(false);
            this.cmsAllSatellites.ResumeLayout(false);
            this.tsMainControls.ResumeLayout(false);
            this.tsMainControls.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.cmsSingleContact.ResumeLayout(false);
            this.cmsSingleSatellite.ResumeLayout(false);
            this.cmsSingleChannel.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem tsmiNewChannel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearChannels;
        private System.Windows.Forms.SaveFileDialog sfdCodeplug;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.OpenFileDialog ofdCodeplug;
        private System.Windows.Forms.ContextMenuStrip cmsSingleChannel;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteChannel;
        private System.Windows.Forms.ToolStripMenuItem tsmiChannelUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiChannelDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiContactUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiContactDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ContextMenuStrip cmsAllZones;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortZonesByName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearZones;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewZone;
    }
}

