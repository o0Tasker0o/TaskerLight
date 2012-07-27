namespace ControlPanel
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.screenCaptureTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.backgroundTab = new System.Windows.Forms.TabPage();
            this.editScriptButton = new System.Windows.Forms.Button();
            this.scriptPanel = new System.Windows.Forms.Panel();
            this.newScriptButton = new System.Windows.Forms.Button();
            this.hsvPicker = new CSharpGUIElements.Colour_Pickers.HSVPicker();
            this.staticColoursBackgroundRadioButton = new System.Windows.Forms.RadioButton();
            this.capturedBackgroundModeRadioButton = new System.Windows.Forms.RadioButton();
            this.activeSceneModeRadioButton = new System.Windows.Forms.RadioButton();
            this.wallpaperBackgroundModeRadioButton = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.activeAppsMarginGroupbox = new System.Windows.Forms.GroupBox();
            this.marginsFullscreenCheckbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.topMarginUpDown = new System.Windows.Forms.NumericUpDown();
            this.bottomMarginUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeAppButton = new System.Windows.Forms.Button();
            this.activeAppListView = new System.Windows.Forms.ListView();
            this.mAppImageList = new System.Windows.Forms.ImageList(this.components);
            this.addAppButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.arduinoComPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.contrastTrackbar = new System.Windows.Forms.TrackBar();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.oversaturationTrackbar = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mOpenExeDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.staticColoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeAppTimer = new System.Windows.Forms.Timer(this.components);
            this.ledPreview1 = new ControlPanel.LEDPreview();
            this.connectButton = new System.Windows.Forms.Button();
            this.ledPreviewTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.backgroundTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.activeAppsMarginGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topMarginUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMarginUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arduinoComPortUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oversaturationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // screenCaptureTimer
            // 
            this.screenCaptureTimer.Tick += new System.EventHandler(this.screenCaptureTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.backgroundTab);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 207);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(458, 224);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Visible = false;
            // 
            // backgroundTab
            // 
            this.backgroundTab.BackColor = System.Drawing.SystemColors.Control;
            this.backgroundTab.Controls.Add(this.editScriptButton);
            this.backgroundTab.Controls.Add(this.scriptPanel);
            this.backgroundTab.Controls.Add(this.newScriptButton);
            this.backgroundTab.Controls.Add(this.hsvPicker);
            this.backgroundTab.Controls.Add(this.staticColoursBackgroundRadioButton);
            this.backgroundTab.Controls.Add(this.capturedBackgroundModeRadioButton);
            this.backgroundTab.Controls.Add(this.activeSceneModeRadioButton);
            this.backgroundTab.Controls.Add(this.wallpaperBackgroundModeRadioButton);
            this.backgroundTab.Location = new System.Drawing.Point(4, 22);
            this.backgroundTab.Name = "backgroundTab";
            this.backgroundTab.Padding = new System.Windows.Forms.Padding(3);
            this.backgroundTab.Size = new System.Drawing.Size(450, 198);
            this.backgroundTab.TabIndex = 0;
            this.backgroundTab.Text = "Background";
            // 
            // editScriptButton
            // 
            this.editScriptButton.Location = new System.Drawing.Point(368, 140);
            this.editScriptButton.Name = "editScriptButton";
            this.editScriptButton.Size = new System.Drawing.Size(75, 23);
            this.editScriptButton.TabIndex = 10;
            this.editScriptButton.Text = "Edit Script";
            this.editScriptButton.UseVisualStyleBackColor = true;
            this.editScriptButton.Visible = false;
            this.editScriptButton.Click += new System.EventHandler(this.editScriptButton_Click);
            // 
            // scriptPanel
            // 
            this.scriptPanel.AutoScroll = true;
            this.scriptPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scriptPanel.Location = new System.Drawing.Point(6, 6);
            this.scriptPanel.Name = "scriptPanel";
            this.scriptPanel.Size = new System.Drawing.Size(337, 186);
            this.scriptPanel.TabIndex = 9;
            // 
            // newScriptButton
            // 
            this.newScriptButton.Location = new System.Drawing.Point(369, 169);
            this.newScriptButton.Name = "newScriptButton";
            this.newScriptButton.Size = new System.Drawing.Size(75, 23);
            this.newScriptButton.TabIndex = 7;
            this.newScriptButton.Text = "New Script";
            this.newScriptButton.UseVisualStyleBackColor = true;
            this.newScriptButton.Visible = false;
            this.newScriptButton.Click += new System.EventHandler(this.newScriptButton_Click);
            // 
            // hsvPicker
            // 
            this.hsvPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.hsvPicker.HueBarWidth = 30;
            this.hsvPicker.Location = new System.Drawing.Point(6, 6);
            this.hsvPicker.Name = "hsvPicker";
            this.hsvPicker.Size = new System.Drawing.Size(221, 186);
            this.hsvPicker.TabIndex = 6;
            this.hsvPicker.ColourChanged += new System.EventHandler(this.hsvPicker_ColourChanged);
            // 
            // staticColoursBackgroundRadioButton
            // 
            this.staticColoursBackgroundRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.staticColoursBackgroundRadioButton.AutoSize = true;
            this.staticColoursBackgroundRadioButton.Checked = true;
            this.staticColoursBackgroundRadioButton.Location = new System.Drawing.Point(354, 6);
            this.staticColoursBackgroundRadioButton.Name = "staticColoursBackgroundRadioButton";
            this.staticColoursBackgroundRadioButton.Size = new System.Drawing.Size(90, 17);
            this.staticColoursBackgroundRadioButton.TabIndex = 3;
            this.staticColoursBackgroundRadioButton.TabStop = true;
            this.staticColoursBackgroundRadioButton.Text = "Static Colours";
            this.staticColoursBackgroundRadioButton.UseVisualStyleBackColor = true;
            this.staticColoursBackgroundRadioButton.CheckedChanged += new System.EventHandler(this.staticColoursBackgroundRadioButton_CheckedChanged);
            // 
            // capturedBackgroundModeRadioButton
            // 
            this.capturedBackgroundModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.capturedBackgroundModeRadioButton.AutoSize = true;
            this.capturedBackgroundModeRadioButton.Location = new System.Drawing.Point(354, 75);
            this.capturedBackgroundModeRadioButton.Name = "capturedBackgroundModeRadioButton";
            this.capturedBackgroundModeRadioButton.Size = new System.Drawing.Size(92, 17);
            this.capturedBackgroundModeRadioButton.TabIndex = 2;
            this.capturedBackgroundModeRadioButton.Text = "Video Capture";
            this.capturedBackgroundModeRadioButton.UseVisualStyleBackColor = true;
            this.capturedBackgroundModeRadioButton.CheckedChanged += new System.EventHandler(this.capturedBackgroundModeRadioButton_CheckedChanged);
            // 
            // activeSceneModeRadioButton
            // 
            this.activeSceneModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activeSceneModeRadioButton.AutoSize = true;
            this.activeSceneModeRadioButton.Location = new System.Drawing.Point(354, 29);
            this.activeSceneModeRadioButton.Name = "activeSceneModeRadioButton";
            this.activeSceneModeRadioButton.Size = new System.Drawing.Size(89, 17);
            this.activeSceneModeRadioButton.TabIndex = 1;
            this.activeSceneModeRadioButton.Text = "Active Scene";
            this.activeSceneModeRadioButton.UseVisualStyleBackColor = true;
            this.activeSceneModeRadioButton.CheckedChanged += new System.EventHandler(this.activeSceneModeRadioButton_CheckedChanged);
            // 
            // wallpaperBackgroundModeRadioButton
            // 
            this.wallpaperBackgroundModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wallpaperBackgroundModeRadioButton.AutoSize = true;
            this.wallpaperBackgroundModeRadioButton.Location = new System.Drawing.Point(354, 52);
            this.wallpaperBackgroundModeRadioButton.Name = "wallpaperBackgroundModeRadioButton";
            this.wallpaperBackgroundModeRadioButton.Size = new System.Drawing.Size(73, 17);
            this.wallpaperBackgroundModeRadioButton.TabIndex = 0;
            this.wallpaperBackgroundModeRadioButton.Text = "Wallpaper";
            this.wallpaperBackgroundModeRadioButton.UseVisualStyleBackColor = true;
            this.wallpaperBackgroundModeRadioButton.CheckedChanged += new System.EventHandler(this.wallpaperBackgroundModeRadioButton_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.activeAppsMarginGroupbox);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(450, 198);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Active Applications";
            // 
            // activeAppsMarginGroupbox
            // 
            this.activeAppsMarginGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activeAppsMarginGroupbox.Controls.Add(this.marginsFullscreenCheckbox);
            this.activeAppsMarginGroupbox.Controls.Add(this.label2);
            this.activeAppsMarginGroupbox.Controls.Add(this.label1);
            this.activeAppsMarginGroupbox.Controls.Add(this.topMarginUpDown);
            this.activeAppsMarginGroupbox.Controls.Add(this.bottomMarginUpDown);
            this.activeAppsMarginGroupbox.Location = new System.Drawing.Point(322, 6);
            this.activeAppsMarginGroupbox.Name = "activeAppsMarginGroupbox";
            this.activeAppsMarginGroupbox.Size = new System.Drawing.Size(122, 94);
            this.activeAppsMarginGroupbox.TabIndex = 4;
            this.activeAppsMarginGroupbox.TabStop = false;
            this.activeAppsMarginGroupbox.Text = "Margins";
            this.activeAppsMarginGroupbox.Visible = false;
            // 
            // marginsFullscreenCheckbox
            // 
            this.marginsFullscreenCheckbox.AutoSize = true;
            this.marginsFullscreenCheckbox.Location = new System.Drawing.Point(7, 71);
            this.marginsFullscreenCheckbox.Name = "marginsFullscreenCheckbox";
            this.marginsFullscreenCheckbox.Size = new System.Drawing.Size(114, 17);
            this.marginsFullscreenCheckbox.TabIndex = 6;
            this.marginsFullscreenCheckbox.Text = "Margins Fullscreen";
            this.marginsFullscreenCheckbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bottom";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Top";
            // 
            // topMarginUpDown
            // 
            this.topMarginUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topMarginUpDown.Location = new System.Drawing.Point(52, 19);
            this.topMarginUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.topMarginUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.topMarginUpDown.Name = "topMarginUpDown";
            this.topMarginUpDown.Size = new System.Drawing.Size(64, 20);
            this.topMarginUpDown.TabIndex = 2;
            this.topMarginUpDown.ValueChanged += new System.EventHandler(this.topMarginUpDown_ValueChanged);
            // 
            // bottomMarginUpDown
            // 
            this.bottomMarginUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomMarginUpDown.Location = new System.Drawing.Point(52, 45);
            this.bottomMarginUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.bottomMarginUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.bottomMarginUpDown.Name = "bottomMarginUpDown";
            this.bottomMarginUpDown.Size = new System.Drawing.Size(64, 20);
            this.bottomMarginUpDown.TabIndex = 3;
            this.bottomMarginUpDown.ValueChanged += new System.EventHandler(this.bottomMarginUpDown_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.removeAppButton);
            this.groupBox1.Controls.Add(this.activeAppListView);
            this.groupBox1.Controls.Add(this.addAppButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Applications";
            // 
            // removeAppButton
            // 
            this.removeAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeAppButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeAppButton.Location = new System.Drawing.Point(254, 158);
            this.removeAppButton.Name = "removeAppButton";
            this.removeAppButton.Size = new System.Drawing.Size(25, 23);
            this.removeAppButton.TabIndex = 11;
            this.removeAppButton.Text = "--";
            this.removeAppButton.UseVisualStyleBackColor = true;
            this.removeAppButton.Visible = false;
            this.removeAppButton.Click += new System.EventHandler(this.removeAppButton_Click);
            // 
            // activeAppListView
            // 
            this.activeAppListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeAppListView.BackColor = System.Drawing.Color.Black;
            this.activeAppListView.ForeColor = System.Drawing.Color.White;
            this.activeAppListView.LargeImageList = this.mAppImageList;
            this.activeAppListView.Location = new System.Drawing.Point(6, 17);
            this.activeAppListView.Name = "activeAppListView";
            this.activeAppListView.Size = new System.Drawing.Size(300, 137);
            this.activeAppListView.TabIndex = 0;
            this.activeAppListView.UseCompatibleStateImageBehavior = false;
            this.activeAppListView.SelectedIndexChanged += new System.EventHandler(this.activeAppListView_SelectedIndexChanged);
            // 
            // mAppImageList
            // 
            this.mAppImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.mAppImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.mAppImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // addAppButton
            // 
            this.addAppButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addAppButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAppButton.Location = new System.Drawing.Point(281, 158);
            this.addAppButton.Name = "addAppButton";
            this.addAppButton.Size = new System.Drawing.Size(25, 23);
            this.addAppButton.TabIndex = 8;
            this.addAppButton.Text = "+";
            this.addAppButton.UseVisualStyleBackColor = true;
            this.addAppButton.Click += new System.EventHandler(this.addAppButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.arduinoComPortUpDown);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.contrastTrackbar);
            this.tabPage2.Controls.Add(this.pictureBox3);
            this.tabPage2.Controls.Add(this.pictureBox4);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.oversaturationTrackbar);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(450, 198);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(258, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Arduino COM port (changing this may take a moment)";
            // 
            // arduinoComPortUpDown
            // 
            this.arduinoComPortUpDown.Location = new System.Drawing.Point(270, 4);
            this.arduinoComPortUpDown.Name = "arduinoComPortUpDown";
            this.arduinoComPortUpDown.Size = new System.Drawing.Size(120, 20);
            this.arduinoComPortUpDown.TabIndex = 14;
            this.arduinoComPortUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.arduinoComPortUpDown.ValueChanged += new System.EventHandler(this.arduinoComPortUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(386, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "200%";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "100%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "0%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Contrast";
            // 
            // contrastTrackbar
            // 
            this.contrastTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contrastTrackbar.AutoSize = false;
            this.contrastTrackbar.Location = new System.Drawing.Point(35, 110);
            this.contrastTrackbar.Maximum = 200;
            this.contrastTrackbar.Name = "contrastTrackbar";
            this.contrastTrackbar.Size = new System.Drawing.Size(377, 34);
            this.contrastTrackbar.TabIndex = 7;
            this.contrastTrackbar.TickFrequency = 25;
            this.contrastTrackbar.Value = 100;
            this.contrastTrackbar.ValueChanged += new System.EventHandler(this.contrastTrackbar_ValueChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::ControlPanel.Properties.Resources.ContrastLow;
            this.pictureBox3.Location = new System.Drawing.Point(6, 106);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 34);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackgroundImage = global::ControlPanel.Properties.Resources.ContrastHigh;
            this.pictureBox4.Location = new System.Drawing.Point(410, 106);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(34, 34);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "200%";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "100%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "0%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Saturation";
            // 
            // oversaturationTrackbar
            // 
            this.oversaturationTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oversaturationTrackbar.AutoSize = false;
            this.oversaturationTrackbar.Location = new System.Drawing.Point(35, 46);
            this.oversaturationTrackbar.Maximum = 200;
            this.oversaturationTrackbar.Name = "oversaturationTrackbar";
            this.oversaturationTrackbar.Size = new System.Drawing.Size(377, 34);
            this.oversaturationTrackbar.TabIndex = 0;
            this.oversaturationTrackbar.TickFrequency = 25;
            this.oversaturationTrackbar.Value = 100;
            this.oversaturationTrackbar.ValueChanged += new System.EventHandler(this.oversaturationTrackbar_ValueChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ControlPanel.Properties.Resources.SaturationLow;
            this.pictureBox2.Location = new System.Drawing.Point(6, 42);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 34);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::ControlPanel.Properties.Resources.SaturationHigh;
            this.pictureBox1.Location = new System.Drawing.Point(410, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 34);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // mOpenExeDialog
            // 
            this.mOpenExeDialog.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TaskerLight";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staticColoursToolStripMenuItem,
            this.activeSceneToolStripMenuItem,
            this.wallpaperToolStripMenuItem,
            this.videoCaptureToolStripMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(153, 114);
            // 
            // staticColoursToolStripMenuItem
            // 
            this.staticColoursToolStripMenuItem.CheckOnClick = true;
            this.staticColoursToolStripMenuItem.Name = "staticColoursToolStripMenuItem";
            this.staticColoursToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.staticColoursToolStripMenuItem.Text = "Static Colours";
            this.staticColoursToolStripMenuItem.Click += new System.EventHandler(this.staticColoursToolStripMenuItem_Click);
            // 
            // activeSceneToolStripMenuItem
            // 
            this.activeSceneToolStripMenuItem.CheckOnClick = true;
            this.activeSceneToolStripMenuItem.Name = "activeSceneToolStripMenuItem";
            this.activeSceneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.activeSceneToolStripMenuItem.Text = "Active Scene";
            this.activeSceneToolStripMenuItem.Click += new System.EventHandler(this.activeSceneToolStripMenuItem_Click);
            // 
            // wallpaperToolStripMenuItem
            // 
            this.wallpaperToolStripMenuItem.CheckOnClick = true;
            this.wallpaperToolStripMenuItem.Name = "wallpaperToolStripMenuItem";
            this.wallpaperToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wallpaperToolStripMenuItem.Text = "Wallpaper";
            this.wallpaperToolStripMenuItem.Click += new System.EventHandler(this.wallpaperToolStripMenuItem_Click);
            // 
            // videoCaptureToolStripMenuItem
            // 
            this.videoCaptureToolStripMenuItem.CheckOnClick = true;
            this.videoCaptureToolStripMenuItem.Name = "videoCaptureToolStripMenuItem";
            this.videoCaptureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.videoCaptureToolStripMenuItem.Text = "Video Capture";
            this.videoCaptureToolStripMenuItem.Click += new System.EventHandler(this.videoCaptureToolStripMenuItem_Click);
            // 
            // activeAppTimer
            // 
            this.activeAppTimer.Interval = 200;
            this.activeAppTimer.Tick += new System.EventHandler(this.activeAppTimer_Tick);
            // 
            // ledPreview1
            // 
            this.ledPreview1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ledPreview1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ledPreview1.BackgroundImage")));
            this.ledPreview1.Location = new System.Drawing.Point(112, 2);
            this.ledPreview1.Name = "ledPreview1";
            this.ledPreview1.Size = new System.Drawing.Size(238, 199);
            this.ledPreview1.TabIndex = 6;
            this.ledPreview1.Visible = false;
            this.ledPreview1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ledPreview1_MouseUp);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Visible = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ledPreviewTimer
            // 
            this.ledPreviewTimer.Interval = 50;
            this.ledPreviewTimer.Tick += new System.EventHandler(this.ledPreviewTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 432);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.ledPreview1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "TaskerLight Control Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.backgroundTab.ResumeLayout(false);
            this.backgroundTab.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.activeAppsMarginGroupbox.ResumeLayout(false);
            this.activeAppsMarginGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topMarginUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMarginUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arduinoComPortUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oversaturationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer screenCaptureTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage backgroundTab;
        private System.Windows.Forms.RadioButton activeSceneModeRadioButton;
        private System.Windows.Forms.RadioButton wallpaperBackgroundModeRadioButton;
        private System.Windows.Forms.RadioButton staticColoursBackgroundRadioButton;
        private System.Windows.Forms.RadioButton capturedBackgroundModeRadioButton;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeAppButton;
        private System.Windows.Forms.ListView activeAppListView;
        private System.Windows.Forms.Button addAppButton;
        private System.Windows.Forms.OpenFileDialog mOpenExeDialog;
        private System.Windows.Forms.NumericUpDown bottomMarginUpDown;
        private System.Windows.Forms.NumericUpDown topMarginUpDown;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox activeAppsMarginGroupbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TrackBar oversaturationTrackbar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private LEDPreview ledPreview1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar contrastTrackbar;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.CheckBox marginsFullscreenCheckbox;
        private CSharpGUIElements.Colour_Pickers.HSVPicker hsvPicker;
        private System.Windows.Forms.ImageList mAppImageList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown arduinoComPortUpDown;
        private System.Windows.Forms.Button newScriptButton;
        private System.Windows.Forms.Timer activeAppTimer;
        private System.Windows.Forms.Panel scriptPanel;
        private System.Windows.Forms.Button editScriptButton;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem staticColoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallpaperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoCaptureToolStripMenuItem;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Timer ledPreviewTimer;

    }
}

