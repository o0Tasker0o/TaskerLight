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
            this.screenCaptureTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.backgroundTab = new System.Windows.Forms.TabPage();
            this.ledPreview1 = new ControlPanel.LEDPreview();
            this.hsvPicker1 = new ControlPanel.HSVPicker();
            this.staticColoursBackgroundRadioButton = new System.Windows.Forms.RadioButton();
            this.capturedBackgroundModeRadioButton = new System.Windows.Forms.RadioButton();
            this.activeSceneModeRadioButton = new System.Windows.Forms.RadioButton();
            this.wallpaperBackgroundModeRadioButton = new System.Windows.Forms.RadioButton();
            this.wallpaperTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.backgroundTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // screenCaptureTimer
            // 
            this.screenCaptureTimer.Tick += new System.EventHandler(this.screenCaptureTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.backgroundTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 405);
            this.tabControl1.TabIndex = 0;
            // 
            // backgroundTab
            // 
            this.backgroundTab.BackColor = System.Drawing.SystemColors.Control;
            this.backgroundTab.Controls.Add(this.ledPreview1);
            this.backgroundTab.Controls.Add(this.hsvPicker1);
            this.backgroundTab.Controls.Add(this.staticColoursBackgroundRadioButton);
            this.backgroundTab.Controls.Add(this.capturedBackgroundModeRadioButton);
            this.backgroundTab.Controls.Add(this.activeSceneModeRadioButton);
            this.backgroundTab.Controls.Add(this.wallpaperBackgroundModeRadioButton);
            this.backgroundTab.Location = new System.Drawing.Point(4, 22);
            this.backgroundTab.Name = "backgroundTab";
            this.backgroundTab.Padding = new System.Windows.Forms.Padding(3);
            this.backgroundTab.Size = new System.Drawing.Size(449, 379);
            this.backgroundTab.TabIndex = 0;
            this.backgroundTab.Text = "Background";
            // 
            // ledPreview1
            // 
            this.ledPreview1.Location = new System.Drawing.Point(8, 6);
            this.ledPreview1.Name = "ledPreview1";
            this.ledPreview1.Size = new System.Drawing.Size(222, 142);
            this.ledPreview1.TabIndex = 6;
            this.ledPreview1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ledPreview1_MouseUp);
            // 
            // hsvPicker1
            // 
            this.hsvPicker1.Location = new System.Drawing.Point(9, 154);
            this.hsvPicker1.Name = "hsvPicker1";
            this.hsvPicker1.Size = new System.Drawing.Size(221, 186);
            this.hsvPicker1.TabIndex = 5;
            this.hsvPicker1.ColourChanged += new System.EventHandler<System.EventArgs>(this.hsvPicker1_ColourChanged);
            // 
            // staticColoursBackgroundRadioButton
            // 
            this.staticColoursBackgroundRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.staticColoursBackgroundRadioButton.AutoSize = true;
            this.staticColoursBackgroundRadioButton.Checked = true;
            this.staticColoursBackgroundRadioButton.Location = new System.Drawing.Point(353, 6);
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
            this.capturedBackgroundModeRadioButton.Location = new System.Drawing.Point(353, 75);
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
            this.activeSceneModeRadioButton.Enabled = false;
            this.activeSceneModeRadioButton.Location = new System.Drawing.Point(353, 29);
            this.activeSceneModeRadioButton.Name = "activeSceneModeRadioButton";
            this.activeSceneModeRadioButton.Size = new System.Drawing.Size(89, 17);
            this.activeSceneModeRadioButton.TabIndex = 1;
            this.activeSceneModeRadioButton.Text = "Active Scene";
            this.activeSceneModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // wallpaperBackgroundModeRadioButton
            // 
            this.wallpaperBackgroundModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wallpaperBackgroundModeRadioButton.AutoSize = true;
            this.wallpaperBackgroundModeRadioButton.Location = new System.Drawing.Point(353, 52);
            this.wallpaperBackgroundModeRadioButton.Name = "wallpaperBackgroundModeRadioButton";
            this.wallpaperBackgroundModeRadioButton.Size = new System.Drawing.Size(73, 17);
            this.wallpaperBackgroundModeRadioButton.TabIndex = 0;
            this.wallpaperBackgroundModeRadioButton.Text = "Wallpaper";
            this.wallpaperBackgroundModeRadioButton.UseVisualStyleBackColor = true;
            this.wallpaperBackgroundModeRadioButton.CheckedChanged += new System.EventHandler(this.wallpaperBackgroundModeRadioButton_CheckedChanged);
            // 
            // wallpaperTimer
            // 
            this.wallpaperTimer.Interval = 1000;
            this.wallpaperTimer.Tick += new System.EventHandler(this.wallpaperTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 405);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "TaskerLight Control Panel";
            this.tabControl1.ResumeLayout(false);
            this.backgroundTab.ResumeLayout(false);
            this.backgroundTab.PerformLayout();
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
        private HSVPicker hsvPicker1;
        private LEDPreview ledPreview1;
        private System.Windows.Forms.Timer wallpaperTimer;

    }
}

