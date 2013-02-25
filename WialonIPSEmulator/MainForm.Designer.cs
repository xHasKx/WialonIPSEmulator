namespace WialonIPSEmulator
{
    partial class MainForm
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        	this.gbConnection = new System.Windows.Forms.GroupBox();
        	this.btnDevicesConfig = new System.Windows.Forms.Button();
        	this.cbDevice = new System.Windows.Forms.ComboBox();
        	this.label2 = new System.Windows.Forms.Label();
        	this.btnDisconnect = new System.Windows.Forms.Button();
        	this.btnConnect = new System.Windows.Forms.Button();
        	this.btnServersConfig = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.cbServer = new System.Windows.Forms.ComboBox();
        	this.tabMain = new System.Windows.Forms.TabControl();
        	this.tabLog = new System.Windows.Forms.TabPage();
        	this.tbLog = new System.Windows.Forms.TextBox();
        	this.gbSendRaw = new System.Windows.Forms.GroupBox();
        	this.tbSendRaw = new System.Windows.Forms.TextBox();
        	this.btnSendRaw = new System.Windows.Forms.Button();
        	this.tabMessages = new System.Windows.Forms.TabPage();
        	this.tbMessages = new System.Windows.Forms.TextBox();
        	this.gbSendMessage = new System.Windows.Forms.GroupBox();
        	this.pbSendImage = new System.Windows.Forms.ProgressBar();
        	this.btnSendImage = new System.Windows.Forms.Button();
        	this.tbSendMessage = new System.Windows.Forms.TextBox();
        	this.btnSendMessage = new System.Windows.Forms.Button();
        	this.tabManual = new System.Windows.Forms.TabPage();
        	this.panParams = new System.Windows.Forms.Panel();
        	this.cbiButtonEnabled = new System.Windows.Forms.CheckBox();
        	this.btnEditCustomParams = new System.Windows.Forms.Button();
        	this.btnADCsCount = new System.Windows.Forms.Button();
        	this.labOutputs = new System.Windows.Forms.Label();
        	this.ioOutputs = new WialonIPSEmulator.IOPanel();
        	this.labInputs = new System.Windows.Forms.Label();
        	this.ioInputs = new WialonIPSEmulator.IOPanel();
        	this.tbIButton = new System.Windows.Forms.TextBox();
        	this.tbHdop = new System.Windows.Forms.TextBox();
        	this.label12 = new System.Windows.Forms.Label();
        	this.cbAlarm = new System.Windows.Forms.CheckBox();
        	this.gbSendPacket = new System.Windows.Forms.GroupBox();
        	this.labNextSend = new System.Windows.Forms.Label();
        	this.labTextNextSend = new System.Windows.Forms.Label();
        	this.labTextSec = new System.Windows.Forms.Label();
        	this.numAutoSendInterval = new System.Windows.Forms.NumericUpDown();
        	this.cbAutoSend = new System.Windows.Forms.CheckBox();
        	this.btnSendData = new System.Windows.Forms.Button();
        	this.tbResultPacket = new System.Windows.Forms.TextBox();
        	this.gbPos = new System.Windows.Forms.GroupBox();
        	this.rbFullData = new System.Windows.Forms.RadioButton();
        	this.rbShortData = new System.Windows.Forms.RadioButton();
        	this.tbSats = new System.Windows.Forms.TextBox();
        	this.label8 = new System.Windows.Forms.Label();
        	this.tbSpeed = new System.Windows.Forms.TextBox();
        	this.label7 = new System.Windows.Forms.Label();
        	this.tbHeading = new System.Windows.Forms.TextBox();
        	this.label6 = new System.Windows.Forms.Label();
        	this.tbAlt = new System.Windows.Forms.TextBox();
        	this.label5 = new System.Windows.Forms.Label();
        	this.tbLon = new System.Windows.Forms.TextBox();
        	this.label4 = new System.Windows.Forms.Label();
        	this.tbLat = new System.Windows.Forms.TextBox();
        	this.label3 = new System.Windows.Forms.Label();
        	this.msMainMenu = new System.Windows.Forms.MenuStrip();
        	this.emulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.dlgSaveLog = new System.Windows.Forms.SaveFileDialog();
        	this.tmrAutoSend = new System.Windows.Forms.Timer(this.components);
        	this.dlgOpenImage = new System.Windows.Forms.OpenFileDialog();
        	this.gbConnection.SuspendLayout();
        	this.tabMain.SuspendLayout();
        	this.tabLog.SuspendLayout();
        	this.gbSendRaw.SuspendLayout();
        	this.tabMessages.SuspendLayout();
        	this.gbSendMessage.SuspendLayout();
        	this.tabManual.SuspendLayout();
        	this.panParams.SuspendLayout();
        	this.gbSendPacket.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.numAutoSendInterval)).BeginInit();
        	this.gbPos.SuspendLayout();
        	this.msMainMenu.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// gbConnection
        	// 
        	this.gbConnection.Controls.Add(this.btnDevicesConfig);
        	this.gbConnection.Controls.Add(this.cbDevice);
        	this.gbConnection.Controls.Add(this.label2);
        	this.gbConnection.Controls.Add(this.btnDisconnect);
        	this.gbConnection.Controls.Add(this.btnConnect);
        	this.gbConnection.Controls.Add(this.btnServersConfig);
        	this.gbConnection.Controls.Add(this.label1);
        	this.gbConnection.Controls.Add(this.cbServer);
        	this.gbConnection.Dock = System.Windows.Forms.DockStyle.Top;
        	this.gbConnection.Location = new System.Drawing.Point(0, 24);
        	this.gbConnection.Name = "gbConnection";
        	this.gbConnection.Size = new System.Drawing.Size(604, 67);
        	this.gbConnection.TabIndex = 0;
        	this.gbConnection.TabStop = false;
        	this.gbConnection.Text = "Connection";
        	// 
        	// btnDevicesConfig
        	// 
        	this.btnDevicesConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnDevicesConfig.Location = new System.Drawing.Point(558, 40);
        	this.btnDevicesConfig.Name = "btnDevicesConfig";
        	this.btnDevicesConfig.Size = new System.Drawing.Size(36, 19);
        	this.btnDevicesConfig.TabIndex = 7;
        	this.btnDevicesConfig.Text = "...";
        	this.btnDevicesConfig.UseVisualStyleBackColor = true;
        	this.btnDevicesConfig.Click += new System.EventHandler(this.btnDevicesConfig_Click);
        	// 
        	// cbDevice
        	// 
        	this.cbDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cbDevice.FormattingEnabled = true;
        	this.cbDevice.Location = new System.Drawing.Point(62, 40);
        	this.cbDevice.Name = "cbDevice";
        	this.cbDevice.Size = new System.Drawing.Size(490, 21);
        	this.cbDevice.TabIndex = 6;
        	this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(12, 45);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(44, 13);
        	this.label2.TabIndex = 5;
        	this.label2.Text = "Device:";
        	// 
        	// btnDisconnect
        	// 
        	this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnDisconnect.Location = new System.Drawing.Point(513, 12);
        	this.btnDisconnect.Name = "btnDisconnect";
        	this.btnDisconnect.Size = new System.Drawing.Size(84, 23);
        	this.btnDisconnect.TabIndex = 4;
        	this.btnDisconnect.Text = "Disconnect";
        	this.btnDisconnect.UseVisualStyleBackColor = true;
        	this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
        	// 
        	// btnConnect
        	// 
        	this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnConnect.Location = new System.Drawing.Point(423, 12);
        	this.btnConnect.Name = "btnConnect";
        	this.btnConnect.Size = new System.Drawing.Size(84, 23);
        	this.btnConnect.TabIndex = 3;
        	this.btnConnect.Text = "Connect";
        	this.btnConnect.UseVisualStyleBackColor = true;
        	this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
        	// 
        	// btnServersConfig
        	// 
        	this.btnServersConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnServersConfig.Location = new System.Drawing.Point(381, 12);
        	this.btnServersConfig.Name = "btnServersConfig";
        	this.btnServersConfig.Size = new System.Drawing.Size(36, 23);
        	this.btnServersConfig.TabIndex = 2;
        	this.btnServersConfig.Text = "...";
        	this.btnServersConfig.UseVisualStyleBackColor = true;
        	this.btnServersConfig.Click += new System.EventHandler(this.btnServersConfig_Click);
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(12, 17);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(41, 13);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "Server:";
        	// 
        	// cbServer
        	// 
        	this.cbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cbServer.FormattingEnabled = true;
        	this.cbServer.Location = new System.Drawing.Point(62, 14);
        	this.cbServer.Name = "cbServer";
        	this.cbServer.Size = new System.Drawing.Size(313, 21);
        	this.cbServer.TabIndex = 0;
        	this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
        	// 
        	// tabMain
        	// 
        	this.tabMain.Controls.Add(this.tabLog);
        	this.tabMain.Controls.Add(this.tabMessages);
        	this.tabMain.Controls.Add(this.tabManual);
        	this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tabMain.Location = new System.Drawing.Point(0, 91);
        	this.tabMain.Name = "tabMain";
        	this.tabMain.SelectedIndex = 0;
        	this.tabMain.Size = new System.Drawing.Size(604, 491);
        	this.tabMain.TabIndex = 1;
        	// 
        	// tabLog
        	// 
        	this.tabLog.Controls.Add(this.tbLog);
        	this.tabLog.Controls.Add(this.gbSendRaw);
        	this.tabLog.Location = new System.Drawing.Point(4, 22);
        	this.tabLog.Name = "tabLog";
        	this.tabLog.Padding = new System.Windows.Forms.Padding(3);
        	this.tabLog.Size = new System.Drawing.Size(596, 465);
        	this.tabLog.TabIndex = 0;
        	this.tabLog.Text = "Log/Send";
        	this.tabLog.UseVisualStyleBackColor = true;
        	// 
        	// tbLog
        	// 
        	this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tbLog.Location = new System.Drawing.Point(3, 3);
        	this.tbLog.Multiline = true;
        	this.tbLog.Name = "tbLog";
        	this.tbLog.ReadOnly = true;
        	this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        	this.tbLog.Size = new System.Drawing.Size(590, 411);
        	this.tbLog.TabIndex = 4;
        	// 
        	// gbSendRaw
        	// 
        	this.gbSendRaw.Controls.Add(this.tbSendRaw);
        	this.gbSendRaw.Controls.Add(this.btnSendRaw);
        	this.gbSendRaw.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.gbSendRaw.Location = new System.Drawing.Point(3, 414);
        	this.gbSendRaw.Name = "gbSendRaw";
        	this.gbSendRaw.Size = new System.Drawing.Size(590, 48);
        	this.gbSendRaw.TabIndex = 3;
        	this.gbSendRaw.TabStop = false;
        	this.gbSendRaw.Text = "Send to server";
        	// 
        	// tbSendRaw
        	// 
        	this.tbSendRaw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.tbSendRaw.Location = new System.Drawing.Point(5, 19);
        	this.tbSendRaw.Name = "tbSendRaw";
        	this.tbSendRaw.Size = new System.Drawing.Size(501, 20);
        	this.tbSendRaw.TabIndex = 5;
        	this.tbSendRaw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSendRaw_KeyDown);
        	// 
        	// btnSendRaw
        	// 
        	this.btnSendRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnSendRaw.Location = new System.Drawing.Point(512, 17);
        	this.btnSendRaw.Name = "btnSendRaw";
        	this.btnSendRaw.Size = new System.Drawing.Size(75, 23);
        	this.btnSendRaw.TabIndex = 4;
        	this.btnSendRaw.Text = "Send";
        	this.btnSendRaw.UseVisualStyleBackColor = true;
        	this.btnSendRaw.Click += new System.EventHandler(this.btnSendRaw_Click);
        	// 
        	// tabMessages
        	// 
        	this.tabMessages.Controls.Add(this.tbMessages);
        	this.tabMessages.Controls.Add(this.gbSendMessage);
        	this.tabMessages.Location = new System.Drawing.Point(4, 22);
        	this.tabMessages.Name = "tabMessages";
        	this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
        	this.tabMessages.Size = new System.Drawing.Size(596, 465);
        	this.tabMessages.TabIndex = 1;
        	this.tabMessages.Text = "Messages";
        	this.tabMessages.UseVisualStyleBackColor = true;
        	// 
        	// tbMessages
        	// 
        	this.tbMessages.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tbMessages.Location = new System.Drawing.Point(3, 3);
        	this.tbMessages.Multiline = true;
        	this.tbMessages.Name = "tbMessages";
        	this.tbMessages.ReadOnly = true;
        	this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        	this.tbMessages.Size = new System.Drawing.Size(590, 411);
        	this.tbMessages.TabIndex = 5;
        	// 
        	// gbSendMessage
        	// 
        	this.gbSendMessage.Controls.Add(this.pbSendImage);
        	this.gbSendMessage.Controls.Add(this.btnSendImage);
        	this.gbSendMessage.Controls.Add(this.tbSendMessage);
        	this.gbSendMessage.Controls.Add(this.btnSendMessage);
        	this.gbSendMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.gbSendMessage.Location = new System.Drawing.Point(3, 414);
        	this.gbSendMessage.Name = "gbSendMessage";
        	this.gbSendMessage.Size = new System.Drawing.Size(590, 48);
        	this.gbSendMessage.TabIndex = 4;
        	this.gbSendMessage.TabStop = false;
        	this.gbSendMessage.Text = "Send text message to server";
        	// 
        	// pbSendImage
        	// 
        	this.pbSendImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.pbSendImage.Location = new System.Drawing.Point(0, 19);
        	this.pbSendImage.Name = "pbSendImage";
        	this.pbSendImage.Size = new System.Drawing.Size(590, 23);
        	this.pbSendImage.TabIndex = 7;
        	this.pbSendImage.Visible = false;
        	// 
        	// btnSendImage
        	// 
        	this.btnSendImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnSendImage.Location = new System.Drawing.Point(510, 17);
        	this.btnSendImage.Name = "btnSendImage";
        	this.btnSendImage.Size = new System.Drawing.Size(75, 23);
        	this.btnSendImage.TabIndex = 6;
        	this.btnSendImage.Text = "Send image";
        	this.btnSendImage.UseVisualStyleBackColor = true;
        	this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
        	// 
        	// tbSendMessage
        	// 
        	this.tbSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.tbSendMessage.Location = new System.Drawing.Point(5, 19);
        	this.tbSendMessage.Name = "tbSendMessage";
        	this.tbSendMessage.Size = new System.Drawing.Size(405, 20);
        	this.tbSendMessage.TabIndex = 5;
        	this.tbSendMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSendMessage_KeyDown);
        	// 
        	// btnSendMessage
        	// 
        	this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.btnSendMessage.Location = new System.Drawing.Point(416, 16);
        	this.btnSendMessage.Name = "btnSendMessage";
        	this.btnSendMessage.Size = new System.Drawing.Size(88, 23);
        	this.btnSendMessage.TabIndex = 4;
        	this.btnSendMessage.Text = "Send message";
        	this.btnSendMessage.UseVisualStyleBackColor = true;
        	this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
        	// 
        	// tabManual
        	// 
        	this.tabManual.Controls.Add(this.panParams);
        	this.tabManual.Controls.Add(this.gbSendPacket);
        	this.tabManual.Controls.Add(this.gbPos);
        	this.tabManual.Location = new System.Drawing.Point(4, 22);
        	this.tabManual.Name = "tabManual";
        	this.tabManual.Size = new System.Drawing.Size(596, 465);
        	this.tabManual.TabIndex = 2;
        	this.tabManual.Text = "Manual sending";
        	this.tabManual.UseVisualStyleBackColor = true;
        	// 
        	// panParams
        	// 
        	this.panParams.AutoScroll = true;
        	this.panParams.Controls.Add(this.cbiButtonEnabled);
        	this.panParams.Controls.Add(this.btnEditCustomParams);
        	this.panParams.Controls.Add(this.btnADCsCount);
        	this.panParams.Controls.Add(this.labOutputs);
        	this.panParams.Controls.Add(this.ioOutputs);
        	this.panParams.Controls.Add(this.labInputs);
        	this.panParams.Controls.Add(this.ioInputs);
        	this.panParams.Controls.Add(this.tbIButton);
        	this.panParams.Controls.Add(this.tbHdop);
        	this.panParams.Controls.Add(this.label12);
        	this.panParams.Controls.Add(this.cbAlarm);
        	this.panParams.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panParams.Location = new System.Drawing.Point(0, 70);
        	this.panParams.Name = "panParams";
        	this.panParams.Size = new System.Drawing.Size(596, 293);
        	this.panParams.TabIndex = 2;
        	// 
        	// cbiButtonEnabled
        	// 
        	this.cbiButtonEnabled.Location = new System.Drawing.Point(177, 4);
        	this.cbiButtonEnabled.Name = "cbiButtonEnabled";
        	this.cbiButtonEnabled.Size = new System.Drawing.Size(62, 24);
        	this.cbiButtonEnabled.TabIndex = 28;
        	this.cbiButtonEnabled.Text = "iButton:";
        	this.cbiButtonEnabled.UseVisualStyleBackColor = true;
        	this.cbiButtonEnabled.CheckedChanged += new System.EventHandler(this.CbiButtonEnabledCheckedChanged);
        	// 
        	// btnEditCustomParams
        	// 
        	this.btnEditCustomParams.Location = new System.Drawing.Point(8, 138);
        	this.btnEditCustomParams.Name = "btnEditCustomParams";
        	this.btnEditCustomParams.Size = new System.Drawing.Size(140, 23);
        	this.btnEditCustomParams.TabIndex = 27;
        	this.btnEditCustomParams.Text = "Parameters count...";
        	this.btnEditCustomParams.UseVisualStyleBackColor = true;
        	this.btnEditCustomParams.Click += new System.EventHandler(this.btnEditCustomParams_Click);
        	// 
        	// btnADCsCount
        	// 
        	this.btnADCsCount.Location = new System.Drawing.Point(392, 138);
        	this.btnADCsCount.Name = "btnADCsCount";
        	this.btnADCsCount.Size = new System.Drawing.Size(168, 23);
        	this.btnADCsCount.TabIndex = 26;
        	this.btnADCsCount.Text = "Count of ADCs";
        	this.btnADCsCount.UseVisualStyleBackColor = true;
        	this.btnADCsCount.Click += new System.EventHandler(this.btnADCsCount_Click);
        	// 
        	// labOutputs
        	// 
        	this.labOutputs.AutoSize = true;
        	this.labOutputs.Location = new System.Drawing.Point(5, 80);
        	this.labOutputs.Name = "labOutputs";
        	this.labOutputs.Size = new System.Drawing.Size(47, 13);
        	this.labOutputs.TabIndex = 25;
        	this.labOutputs.Text = "Outputs:";
        	// 
        	// ioOutputs
        	// 
        	this.ioOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.ioOutputs.Location = new System.Drawing.Point(8, 96);
        	this.ioOutputs.Name = "ioOutputs";
        	this.ioOutputs.Size = new System.Drawing.Size(580, 30);
        	this.ioOutputs.TabIndex = 24;
        	this.ioOutputs.Value = ((uint)(0u));
        	this.ioOutputs.OnChange += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// labInputs
        	// 
        	this.labInputs.AutoSize = true;
        	this.labInputs.Location = new System.Drawing.Point(3, 31);
        	this.labInputs.Name = "labInputs";
        	this.labInputs.Size = new System.Drawing.Size(39, 13);
        	this.labInputs.TabIndex = 23;
        	this.labInputs.Text = "Inputs:";
        	// 
        	// ioInputs
        	// 
        	this.ioInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.ioInputs.Location = new System.Drawing.Point(8, 47);
        	this.ioInputs.Name = "ioInputs";
        	this.ioInputs.Size = new System.Drawing.Size(580, 30);
        	this.ioInputs.TabIndex = 22;
        	this.ioInputs.Value = ((uint)(0u));
        	this.ioInputs.OnChange += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// tbIButton
        	// 
        	this.tbIButton.Enabled = false;
        	this.tbIButton.Location = new System.Drawing.Point(245, 6);
        	this.tbIButton.Name = "tbIButton";
        	this.tbIButton.Size = new System.Drawing.Size(119, 20);
        	this.tbIButton.TabIndex = 21;
        	this.tbIButton.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// tbHdop
        	// 
        	this.tbHdop.Location = new System.Drawing.Point(52, 6);
        	this.tbHdop.Name = "tbHdop";
        	this.tbHdop.Size = new System.Drawing.Size(119, 20);
        	this.tbHdop.TabIndex = 19;
        	this.tbHdop.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label12
        	// 
        	this.label12.AutoSize = true;
        	this.label12.Location = new System.Drawing.Point(5, 9);
        	this.label12.Name = "label12";
        	this.label12.Size = new System.Drawing.Size(41, 13);
        	this.label12.TabIndex = 18;
        	this.label12.Text = "HDOP:";
        	// 
        	// cbAlarm
        	// 
        	this.cbAlarm.AutoSize = true;
        	this.cbAlarm.Location = new System.Drawing.Point(370, 8);
        	this.cbAlarm.Name = "cbAlarm";
        	this.cbAlarm.Size = new System.Drawing.Size(52, 17);
        	this.cbAlarm.TabIndex = 17;
        	this.cbAlarm.Text = "Alarm";
        	this.cbAlarm.UseVisualStyleBackColor = true;
        	this.cbAlarm.CheckedChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// gbSendPacket
        	// 
        	this.gbSendPacket.Controls.Add(this.labNextSend);
        	this.gbSendPacket.Controls.Add(this.labTextNextSend);
        	this.gbSendPacket.Controls.Add(this.labTextSec);
        	this.gbSendPacket.Controls.Add(this.numAutoSendInterval);
        	this.gbSendPacket.Controls.Add(this.cbAutoSend);
        	this.gbSendPacket.Controls.Add(this.btnSendData);
        	this.gbSendPacket.Controls.Add(this.tbResultPacket);
        	this.gbSendPacket.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.gbSendPacket.Location = new System.Drawing.Point(0, 363);
        	this.gbSendPacket.Name = "gbSendPacket";
        	this.gbSendPacket.Size = new System.Drawing.Size(596, 102);
        	this.gbSendPacket.TabIndex = 1;
        	this.gbSendPacket.TabStop = false;
        	this.gbSendPacket.Text = "Send packet";
        	// 
        	// labNextSend
        	// 
        	this.labNextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.labNextSend.AutoSize = true;
        	this.labNextSend.Location = new System.Drawing.Point(539, 76);
        	this.labNextSend.Name = "labNextSend";
        	this.labNextSend.Size = new System.Drawing.Size(13, 13);
        	this.labNextSend.TabIndex = 6;
        	this.labNextSend.Text = "0";
        	// 
        	// labTextNextSend
        	// 
        	this.labTextNextSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.labTextNextSend.AutoSize = true;
        	this.labTextNextSend.Location = new System.Drawing.Point(475, 76);
        	this.labTextNextSend.Name = "labTextNextSend";
        	this.labTextNextSend.Size = new System.Drawing.Size(58, 13);
        	this.labTextNextSend.TabIndex = 5;
        	this.labTextNextSend.Text = "Next send:";
        	// 
        	// labTextSec
        	// 
        	this.labTextSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.labTextSec.AutoSize = true;
        	this.labTextSec.Location = new System.Drawing.Point(442, 76);
        	this.labTextSec.Name = "labTextSec";
        	this.labTextSec.Size = new System.Drawing.Size(27, 13);
        	this.labTextSec.TabIndex = 4;
        	this.labTextSec.Text = "sec.";
        	// 
        	// numAutoSendInterval
        	// 
        	this.numAutoSendInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.numAutoSendInterval.Location = new System.Drawing.Point(363, 74);
        	this.numAutoSendInterval.Maximum = new decimal(new int[] {
        	        	        	86400,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.numAutoSendInterval.Minimum = new decimal(new int[] {
        	        	        	1,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.numAutoSendInterval.Name = "numAutoSendInterval";
        	this.numAutoSendInterval.Size = new System.Drawing.Size(73, 20);
        	this.numAutoSendInterval.TabIndex = 3;
        	this.numAutoSendInterval.Value = new decimal(new int[] {
        	        	        	30,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.numAutoSendInterval.ValueChanged += new System.EventHandler(this.numAutoSendInterval_ValueChanged);
        	// 
        	// cbAutoSend
        	// 
        	this.cbAutoSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.cbAutoSend.AutoSize = true;
        	this.cbAutoSend.Location = new System.Drawing.Point(235, 75);
        	this.cbAutoSend.Name = "cbAutoSend";
        	this.cbAutoSend.Size = new System.Drawing.Size(106, 17);
        	this.cbAutoSend.TabIndex = 2;
        	this.cbAutoSend.Text = "Auto send every:";
        	this.cbAutoSend.UseVisualStyleBackColor = true;
        	this.cbAutoSend.CheckedChanged += new System.EventHandler(this.cbAutoSend_CheckedChanged);
        	// 
        	// btnSendData
        	// 
        	this.btnSendData.Location = new System.Drawing.Point(6, 71);
        	this.btnSendData.Name = "btnSendData";
        	this.btnSendData.Size = new System.Drawing.Size(75, 23);
        	this.btnSendData.TabIndex = 1;
        	this.btnSendData.Text = "Send now";
        	this.btnSendData.UseVisualStyleBackColor = true;
        	this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
        	// 
        	// tbResultPacket
        	// 
        	this.tbResultPacket.Dock = System.Windows.Forms.DockStyle.Top;
        	this.tbResultPacket.Location = new System.Drawing.Point(3, 16);
        	this.tbResultPacket.Multiline = true;
        	this.tbResultPacket.Name = "tbResultPacket";
        	this.tbResultPacket.ReadOnly = true;
        	this.tbResultPacket.Size = new System.Drawing.Size(590, 49);
        	this.tbResultPacket.TabIndex = 0;
        	this.tbResultPacket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbResultPacket_KeyDown);
        	// 
        	// gbPos
        	// 
        	this.gbPos.Controls.Add(this.rbFullData);
        	this.gbPos.Controls.Add(this.rbShortData);
        	this.gbPos.Controls.Add(this.tbSats);
        	this.gbPos.Controls.Add(this.label8);
        	this.gbPos.Controls.Add(this.tbSpeed);
        	this.gbPos.Controls.Add(this.label7);
        	this.gbPos.Controls.Add(this.tbHeading);
        	this.gbPos.Controls.Add(this.label6);
        	this.gbPos.Controls.Add(this.tbAlt);
        	this.gbPos.Controls.Add(this.label5);
        	this.gbPos.Controls.Add(this.tbLon);
        	this.gbPos.Controls.Add(this.label4);
        	this.gbPos.Controls.Add(this.tbLat);
        	this.gbPos.Controls.Add(this.label3);
        	this.gbPos.Dock = System.Windows.Forms.DockStyle.Top;
        	this.gbPos.Location = new System.Drawing.Point(0, 0);
        	this.gbPos.Name = "gbPos";
        	this.gbPos.Size = new System.Drawing.Size(596, 70);
        	this.gbPos.TabIndex = 0;
        	this.gbPos.TabStop = false;
        	this.gbPos.Text = "Position";
        	// 
        	// rbFullData
        	// 
        	this.rbFullData.AutoSize = true;
        	this.rbFullData.Location = new System.Drawing.Point(393, 38);
        	this.rbFullData.Name = "rbFullData";
        	this.rbFullData.Size = new System.Drawing.Size(101, 17);
        	this.rbFullData.TabIndex = 12;
        	this.rbFullData.Text = "Full data packet";
        	this.rbFullData.UseVisualStyleBackColor = true;
        	this.rbFullData.CheckedChanged += new System.EventHandler(this.rbFullData_CheckedChanged);
        	// 
        	// rbShortData
        	// 
        	this.rbShortData.AutoSize = true;
        	this.rbShortData.Checked = true;
        	this.rbShortData.Location = new System.Drawing.Point(393, 15);
        	this.rbShortData.Name = "rbShortData";
        	this.rbShortData.Size = new System.Drawing.Size(110, 17);
        	this.rbShortData.TabIndex = 11;
        	this.rbShortData.TabStop = true;
        	this.rbShortData.Text = "Short data packet";
        	this.rbShortData.UseVisualStyleBackColor = true;
        	this.rbShortData.CheckedChanged += new System.EventHandler(this.rbShortData_CheckedChanged);
        	// 
        	// tbSats
        	// 
        	this.tbSats.Location = new System.Drawing.Point(331, 39);
        	this.tbSats.Name = "tbSats";
        	this.tbSats.Size = new System.Drawing.Size(46, 20);
        	this.tbSats.TabIndex = 5;
        	this.tbSats.Text = "7";
        	this.tbSats.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label8
        	// 
        	this.label8.AutoSize = true;
        	this.label8.Location = new System.Drawing.Point(273, 42);
        	this.label8.Name = "label8";
        	this.label8.Size = new System.Drawing.Size(52, 13);
        	this.label8.TabIndex = 10;
        	this.label8.Text = "Satellites:";
        	// 
        	// tbSpeed
        	// 
        	this.tbSpeed.Location = new System.Drawing.Point(331, 13);
        	this.tbSpeed.Name = "tbSpeed";
        	this.tbSpeed.Size = new System.Drawing.Size(46, 20);
        	this.tbSpeed.TabIndex = 4;
        	this.tbSpeed.Text = "0";
        	this.tbSpeed.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label7
        	// 
        	this.label7.AutoSize = true;
        	this.label7.Location = new System.Drawing.Point(273, 16);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(41, 13);
        	this.label7.TabIndex = 8;
        	this.label7.Text = "Speed:";
        	// 
        	// tbHeading
        	// 
        	this.tbHeading.Location = new System.Drawing.Point(210, 39);
        	this.tbHeading.Name = "tbHeading";
        	this.tbHeading.Size = new System.Drawing.Size(57, 20);
        	this.tbHeading.TabIndex = 3;
        	this.tbHeading.Text = "0";
        	this.tbHeading.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label6
        	// 
        	this.label6.AutoSize = true;
        	this.label6.Location = new System.Drawing.Point(154, 42);
        	this.label6.Name = "label6";
        	this.label6.Size = new System.Drawing.Size(50, 13);
        	this.label6.TabIndex = 3;
        	this.label6.Text = "Heading:";
        	// 
        	// tbAlt
        	// 
        	this.tbAlt.Location = new System.Drawing.Point(210, 13);
        	this.tbAlt.Name = "tbAlt";
        	this.tbAlt.Size = new System.Drawing.Size(57, 20);
        	this.tbAlt.TabIndex = 2;
        	this.tbAlt.Text = "300";
        	this.tbAlt.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label5
        	// 
        	this.label5.AutoSize = true;
        	this.label5.Location = new System.Drawing.Point(154, 16);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(45, 13);
        	this.label5.TabIndex = 0;
        	this.label5.Text = "Altitude:";
        	// 
        	// tbLon
        	// 
        	this.tbLon.Location = new System.Drawing.Point(71, 39);
        	this.tbLon.Name = "tbLon";
        	this.tbLon.Size = new System.Drawing.Size(77, 20);
        	this.tbLon.TabIndex = 1;
        	this.tbLon.Text = "27,540165";
        	this.tbLon.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(8, 42);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(57, 13);
        	this.label4.TabIndex = 2;
        	this.label4.Text = "Longitude:";
        	// 
        	// tbLat
        	// 
        	this.tbLat.Location = new System.Drawing.Point(71, 13);
        	this.tbLat.Name = "tbLat";
        	this.tbLat.Size = new System.Drawing.Size(77, 20);
        	this.tbLat.TabIndex = 0;
        	this.tbLat.Text = "53,918210";
        	this.tbLat.TextChanged += new System.EventHandler(this.UpdateCurrentPacketDataHandler);
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(8, 16);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(48, 13);
        	this.label3.TabIndex = 0;
        	this.label3.Text = "Latitude:";
        	// 
        	// msMainMenu
        	// 
        	this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.emulatorToolStripMenuItem,
        	        	        	this.helpToolStripMenuItem});
        	this.msMainMenu.Location = new System.Drawing.Point(0, 0);
        	this.msMainMenu.Name = "msMainMenu";
        	this.msMainMenu.Size = new System.Drawing.Size(604, 24);
        	this.msMainMenu.TabIndex = 2;
        	this.msMainMenu.Text = "menuStrip1";
        	// 
        	// emulatorToolStripMenuItem
        	// 
        	this.emulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.settingsToolStripMenuItem,
        	        	        	this.saveLogToolStripMenuItem,
        	        	        	this.exitToolStripMenuItem});
        	this.emulatorToolStripMenuItem.Name = "emulatorToolStripMenuItem";
        	this.emulatorToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
        	this.emulatorToolStripMenuItem.Text = "&Emulator";
        	// 
        	// settingsToolStripMenuItem
        	// 
        	this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        	this.settingsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
        	this.settingsToolStripMenuItem.Text = "Se&ttings";
        	this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
        	// 
        	// saveLogToolStripMenuItem
        	// 
        	this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
        	this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
        	this.saveLogToolStripMenuItem.Text = "&Save log";
        	this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
        	// 
        	// exitToolStripMenuItem
        	// 
        	this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        	this.exitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
        	this.exitToolStripMenuItem.Text = "E&xit";
        	this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
        	// 
        	// helpToolStripMenuItem
        	// 
        	this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.contentToolStripMenuItem,
        	        	        	this.aboutToolStripMenuItem});
        	this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        	this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
        	this.helpToolStripMenuItem.Text = "&Help";
        	// 
        	// contentToolStripMenuItem
        	// 
        	this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
        	this.contentToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
        	this.contentToolStripMenuItem.Text = "&Wiki";
        	this.contentToolStripMenuItem.Click += new System.EventHandler(this.contentToolStripMenuItem_Click);
        	// 
        	// aboutToolStripMenuItem
        	// 
        	this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        	this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
        	this.aboutToolStripMenuItem.Text = "&About";
        	this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
        	// 
        	// dlgSaveLog
        	// 
        	this.dlgSaveLog.DefaultExt = "*.txt";
        	this.dlgSaveLog.Filter = "Text files|*.txt|All files|*.*";
        	// 
        	// tmrAutoSend
        	// 
        	this.tmrAutoSend.Interval = 1000;
        	this.tmrAutoSend.Tick += new System.EventHandler(this.tmrAutoSend_Tick);
        	// 
        	// dlgOpenImage
        	// 
        	this.dlgOpenImage.Filter = "JPG|*.jpg|PNG|*.png";
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(604, 582);
        	this.Controls.Add(this.tabMain);
        	this.Controls.Add(this.gbConnection);
        	this.Controls.Add(this.msMainMenu);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MainMenuStrip = this.msMainMenu;
        	this.MinimumSize = new System.Drawing.Size(620, 620);
        	this.Name = "MainForm";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Wialon IPS Emulator";
        	this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
        	this.Load += new System.EventHandler(this.MainForm_Load);
        	this.Shown += new System.EventHandler(this.MainForm_Shown);
        	this.gbConnection.ResumeLayout(false);
        	this.gbConnection.PerformLayout();
        	this.tabMain.ResumeLayout(false);
        	this.tabLog.ResumeLayout(false);
        	this.tabLog.PerformLayout();
        	this.gbSendRaw.ResumeLayout(false);
        	this.gbSendRaw.PerformLayout();
        	this.tabMessages.ResumeLayout(false);
        	this.tabMessages.PerformLayout();
        	this.gbSendMessage.ResumeLayout(false);
        	this.gbSendMessage.PerformLayout();
        	this.tabManual.ResumeLayout(false);
        	this.panParams.ResumeLayout(false);
        	this.panParams.PerformLayout();
        	this.gbSendPacket.ResumeLayout(false);
        	this.gbSendPacket.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.numAutoSendInterval)).EndInit();
        	this.gbPos.ResumeLayout(false);
        	this.gbPos.PerformLayout();
        	this.msMainMenu.ResumeLayout(false);
        	this.msMainMenu.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.CheckBox cbiButtonEnabled;

        #endregion

        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbSendRaw;
        private System.Windows.Forms.TextBox tbSendRaw;
        private System.Windows.Forms.Button btnSendRaw;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnServersConfig;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDevicesConfig;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem emulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog dlgSaveLog;
        private System.Windows.Forms.TabPage tabManual;
        private System.Windows.Forms.TextBox tbMessages;
        private System.Windows.Forms.GroupBox gbSendMessage;
        private System.Windows.Forms.TextBox tbSendMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.GroupBox gbPos;
        private System.Windows.Forms.TextBox tbLon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHeading;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAlt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSats;
        private System.Windows.Forms.RadioButton rbFullData;
        private System.Windows.Forms.RadioButton rbShortData;
        private System.Windows.Forms.GroupBox gbSendPacket;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox tbResultPacket;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.CheckBox cbAutoSend;
        private System.Windows.Forms.NumericUpDown numAutoSendInterval;
        private System.Windows.Forms.Label labTextNextSend;
        private System.Windows.Forms.Label labTextSec;
        private System.Windows.Forms.Timer tmrAutoSend;
        private System.Windows.Forms.Label labNextSend;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.OpenFileDialog dlgOpenImage;
        private System.Windows.Forms.ProgressBar pbSendImage;
        private System.Windows.Forms.Panel panParams;
        private System.Windows.Forms.Label labOutputs;
        private IOPanel ioOutputs;
        private System.Windows.Forms.Label labInputs;
        private IOPanel ioInputs;
        private System.Windows.Forms.TextBox tbIButton;
        private System.Windows.Forms.TextBox tbHdop;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbAlarm;
        private System.Windows.Forms.Button btnADCsCount;
        private System.Windows.Forms.Button btnEditCustomParams;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

