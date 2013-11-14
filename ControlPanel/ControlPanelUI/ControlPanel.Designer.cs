namespace ControlPanelUI
{
    partial class ControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.wallpaperRadioButton = new System.Windows.Forms.RadioButton();
            this.activeScriptRadioButton = new System.Windows.Forms.RadioButton();
            this.videoRadioButton = new System.Windows.Forms.RadioButton();
            this.staticColourRadioButton = new System.Windows.Forms.RadioButton();
            this.hsvPicker1 = new CSharpGUIElements.Colour_Pickers.HSVPicker();
            this.modePage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.activeScriptBrowserControl1 = new ControlPanelUI.ActiveScriptBrowserControl();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saturationTrackbar = new System.Windows.Forms.TrackBar();
            this.contrastTrackbar = new System.Windows.Forms.TrackBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.staticColoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ledPreview1 = new ControlPanelUI.LedPreview();
            this.modePage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.settingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackbar)).BeginInit();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // wallpaperRadioButton
            // 
            this.wallpaperRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wallpaperRadioButton.AutoSize = true;
            this.wallpaperRadioButton.Checked = true;
            this.wallpaperRadioButton.Location = new System.Drawing.Point(243, 169);
            this.wallpaperRadioButton.Name = "wallpaperRadioButton";
            this.wallpaperRadioButton.Size = new System.Drawing.Size(73, 17);
            this.wallpaperRadioButton.TabIndex = 0;
            this.wallpaperRadioButton.TabStop = true;
            this.wallpaperRadioButton.Text = "Wallpaper";
            this.wallpaperRadioButton.UseVisualStyleBackColor = true;
            this.wallpaperRadioButton.CheckedChanged += new System.EventHandler(this.wallpaperRadioButton_CheckedChanged);
            // 
            // activeScriptRadioButton
            // 
            this.activeScriptRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.activeScriptRadioButton.AutoSize = true;
            this.activeScriptRadioButton.Location = new System.Drawing.Point(243, 146);
            this.activeScriptRadioButton.Name = "activeScriptRadioButton";
            this.activeScriptRadioButton.Size = new System.Drawing.Size(85, 17);
            this.activeScriptRadioButton.TabIndex = 1;
            this.activeScriptRadioButton.Text = "Active Script";
            this.activeScriptRadioButton.UseVisualStyleBackColor = true;
            this.activeScriptRadioButton.CheckedChanged += new System.EventHandler(this.activeScriptRadioButton_CheckedChanged);
            // 
            // videoRadioButton
            // 
            this.videoRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.videoRadioButton.AutoSize = true;
            this.videoRadioButton.Location = new System.Drawing.Point(243, 192);
            this.videoRadioButton.Name = "videoRadioButton";
            this.videoRadioButton.Size = new System.Drawing.Size(52, 17);
            this.videoRadioButton.TabIndex = 2;
            this.videoRadioButton.Text = "Video";
            this.videoRadioButton.UseVisualStyleBackColor = true;
            this.videoRadioButton.CheckedChanged += new System.EventHandler(this.videoRadioButton_CheckedChanged);
            // 
            // staticColourRadioButton
            // 
            this.staticColourRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.staticColourRadioButton.AutoSize = true;
            this.staticColourRadioButton.Location = new System.Drawing.Point(243, 123);
            this.staticColourRadioButton.Name = "staticColourRadioButton";
            this.staticColourRadioButton.Size = new System.Drawing.Size(90, 17);
            this.staticColourRadioButton.TabIndex = 6;
            this.staticColourRadioButton.TabStop = true;
            this.staticColourRadioButton.Text = "Static Colours";
            this.staticColourRadioButton.UseVisualStyleBackColor = true;
            this.staticColourRadioButton.CheckedChanged += new System.EventHandler(this.staticColourRadioButton_CheckedChanged);
            // 
            // hsvPicker1
            // 
            this.hsvPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hsvPicker1.HueBarWidth = 30;
            this.hsvPicker1.Location = new System.Drawing.Point(6, 23);
            this.hsvPicker1.Name = "hsvPicker1";
            this.hsvPicker1.Size = new System.Drawing.Size(220, 186);
            this.hsvPicker1.TabIndex = 7;
            this.hsvPicker1.Visible = false;
            this.hsvPicker1.ColourChanged += new System.EventHandler(this.hsvPicker1_ColourChanged);
            // 
            // modePage
            // 
            this.modePage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modePage.Controls.Add(this.tabPage1);
            this.modePage.Controls.Add(this.settingsPage);
            this.modePage.Location = new System.Drawing.Point(0, 88);
            this.modePage.Name = "modePage";
            this.modePage.SelectedIndex = 0;
            this.modePage.Size = new System.Drawing.Size(345, 241);
            this.modePage.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.hsvPicker1);
            this.tabPage1.Controls.Add(this.staticColourRadioButton);
            this.tabPage1.Controls.Add(this.activeScriptBrowserControl1);
            this.tabPage1.Controls.Add(this.wallpaperRadioButton);
            this.tabPage1.Controls.Add(this.videoRadioButton);
            this.tabPage1.Controls.Add(this.activeScriptRadioButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(337, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mode";
            // 
            // activeScriptBrowserControl1
            // 
            this.activeScriptBrowserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeScriptBrowserControl1.AutoScroll = true;
            this.activeScriptBrowserControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.activeScriptBrowserControl1.Location = new System.Drawing.Point(6, 23);
            this.activeScriptBrowserControl1.Name = "activeScriptBrowserControl1";
            this.activeScriptBrowserControl1.Size = new System.Drawing.Size(220, 186);
            this.activeScriptBrowserControl1.TabIndex = 4;
            this.activeScriptBrowserControl1.Visible = false;
            this.activeScriptBrowserControl1.ScriptSelectionChanged += new ControlPanelUI.ActiveScriptBrowserControl.ScriptSelectionChangedEventHandler(this.activeScriptBrowserControl1_ScriptSelectionChanged);
            // 
            // settingsPage
            // 
            this.settingsPage.BackColor = System.Drawing.SystemColors.Control;
            this.settingsPage.Controls.Add(this.pictureBox4);
            this.settingsPage.Controls.Add(this.pictureBox3);
            this.settingsPage.Controls.Add(this.pictureBox2);
            this.settingsPage.Controls.Add(this.pictureBox1);
            this.settingsPage.Controls.Add(this.label6);
            this.settingsPage.Controls.Add(this.label7);
            this.settingsPage.Controls.Add(this.label8);
            this.settingsPage.Controls.Add(this.label5);
            this.settingsPage.Controls.Add(this.label4);
            this.settingsPage.Controls.Add(this.label3);
            this.settingsPage.Controls.Add(this.label2);
            this.settingsPage.Controls.Add(this.label1);
            this.settingsPage.Controls.Add(this.saturationTrackbar);
            this.settingsPage.Controls.Add(this.contrastTrackbar);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(337, 215);
            this.settingsPage.TabIndex = 1;
            this.settingsPage.Text = "Settings";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::ControlPanelUI.Properties.Resources.SaturationHigh;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox4.Location = new System.Drawing.Point(300, 19);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(34, 34);
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::ControlPanelUI.Properties.Resources.SaturationLow;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(3, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(34, 34);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ControlPanelUI.Properties.Resources.ContrastHigh;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(300, 93);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 34);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ControlPanelUI.Properties.Resources.ContrastLow;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 34);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "200%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(154, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "100%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "0%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Contrast";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Saturation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "200%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "100%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "0%";
            // 
            // saturationTrackbar
            // 
            this.saturationTrackbar.Location = new System.Drawing.Point(38, 26);
            this.saturationTrackbar.Maximum = 200;
            this.saturationTrackbar.Name = "saturationTrackbar";
            this.saturationTrackbar.Size = new System.Drawing.Size(261, 45);
            this.saturationTrackbar.TabIndex = 1;
            this.saturationTrackbar.TickFrequency = 25;
            this.saturationTrackbar.Value = 100;
            this.saturationTrackbar.ValueChanged += new System.EventHandler(this.saturationTrackbar_ValueChanged);
            // 
            // contrastTrackbar
            // 
            this.contrastTrackbar.Location = new System.Drawing.Point(38, 100);
            this.contrastTrackbar.Maximum = 200;
            this.contrastTrackbar.Name = "contrastTrackbar";
            this.contrastTrackbar.Size = new System.Drawing.Size(261, 45);
            this.contrastTrackbar.TabIndex = 0;
            this.contrastTrackbar.TickFrequency = 25;
            this.contrastTrackbar.Value = 100;
            this.contrastTrackbar.ValueChanged += new System.EventHandler(this.contrastTrackbar_ValueChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TaskerLight";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staticColoursToolStripMenuItem,
            this.activeScriptToolStripMenuItem,
            this.wallpaperToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(153, 142);
            // 
            // staticColoursToolStripMenuItem
            // 
            this.staticColoursToolStripMenuItem.CheckOnClick = true;
            this.staticColoursToolStripMenuItem.Name = "staticColoursToolStripMenuItem";
            this.staticColoursToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.staticColoursToolStripMenuItem.Text = "Static Colours";
            this.staticColoursToolStripMenuItem.Click += new System.EventHandler(this.staticColoursToolStripMenuItem_Click);
            // 
            // activeScriptToolStripMenuItem
            // 
            this.activeScriptToolStripMenuItem.CheckOnClick = true;
            this.activeScriptToolStripMenuItem.Name = "activeScriptToolStripMenuItem";
            this.activeScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.activeScriptToolStripMenuItem.Text = "Active Script";
            this.activeScriptToolStripMenuItem.Click += new System.EventHandler(this.activeScriptToolStripMenuItem_Click);
            // 
            // wallpaperToolStripMenuItem
            // 
            this.wallpaperToolStripMenuItem.Checked = true;
            this.wallpaperToolStripMenuItem.CheckOnClick = true;
            this.wallpaperToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wallpaperToolStripMenuItem.Name = "wallpaperToolStripMenuItem";
            this.wallpaperToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wallpaperToolStripMenuItem.Text = "Wallpaper";
            this.wallpaperToolStripMenuItem.Click += new System.EventHandler(this.wallpaperToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.CheckOnClick = true;
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.videoToolStripMenuItem.Text = "Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ledPreview1
            // 
            this.ledPreview1.AllowInput = false;
            this.ledPreview1.InputColour = System.Drawing.Color.Empty;
            this.ledPreview1.Location = new System.Drawing.Point(135, 12);
            this.ledPreview1.Name = "ledPreview1";
            this.ledPreview1.Size = new System.Drawing.Size(90, 70);
            this.ledPreview1.TabIndex = 5;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 329);
            this.Controls.Add(this.modePage);
            this.Controls.Add(this.ledPreview1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControlPanel";
            this.Text = "TaskerLight Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanel_FormClosing);
            this.Resize += new System.EventHandler(this.ControlPanel_Resize);
            this.modePage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackbar)).EndInit();
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton wallpaperRadioButton;
        private System.Windows.Forms.RadioButton activeScriptRadioButton;
        private System.Windows.Forms.RadioButton videoRadioButton;
        private ActiveScriptBrowserControl activeScriptBrowserControl1;
        private LedPreview ledPreview1;
        private System.Windows.Forms.RadioButton staticColourRadioButton;
        private CSharpGUIElements.Colour_Pickers.HSVPicker hsvPicker1;
        private System.Windows.Forms.TabControl modePage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar saturationTrackbar;
        private System.Windows.Forms.TrackBar contrastTrackbar;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticColoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wallpaperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

