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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.wallpaperRadioButton = new System.Windows.Forms.RadioButton();
            this.activeScriptRadioButton = new System.Windows.Forms.RadioButton();
            this.videoRadioButton = new System.Windows.Forms.RadioButton();
            this.ledPreview1 = new ControlPanelUI.LedPreview();
            this.activeScriptBrowserControl1 = new ControlPanelUI.ActiveScriptBrowserControl();
            this.SuspendLayout();
            // 
            // wallpaperRadioButton
            // 
            this.wallpaperRadioButton.AutoSize = true;
            this.wallpaperRadioButton.Checked = true;
            this.wallpaperRadioButton.Location = new System.Drawing.Point(195, 217);
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
            this.activeScriptRadioButton.AutoSize = true;
            this.activeScriptRadioButton.Location = new System.Drawing.Point(195, 194);
            this.activeScriptRadioButton.Name = "activeScriptRadioButton";
            this.activeScriptRadioButton.Size = new System.Drawing.Size(85, 17);
            this.activeScriptRadioButton.TabIndex = 1;
            this.activeScriptRadioButton.Text = "Active Script";
            this.activeScriptRadioButton.UseVisualStyleBackColor = true;
            this.activeScriptRadioButton.CheckedChanged += new System.EventHandler(this.activeScriptRadioButton_CheckedChanged);
            // 
            // videoRadioButton
            // 
            this.videoRadioButton.AutoSize = true;
            this.videoRadioButton.Location = new System.Drawing.Point(195, 240);
            this.videoRadioButton.Name = "videoRadioButton";
            this.videoRadioButton.Size = new System.Drawing.Size(52, 17);
            this.videoRadioButton.TabIndex = 2;
            this.videoRadioButton.Text = "Video";
            this.videoRadioButton.UseVisualStyleBackColor = true;
            this.videoRadioButton.CheckedChanged += new System.EventHandler(this.videoRadioButton_CheckedChanged);
            // 
            // ledPreview1
            // 
            this.ledPreview1.Location = new System.Drawing.Point(130, 12);
            this.ledPreview1.Name = "ledPreview1";
            this.ledPreview1.Size = new System.Drawing.Size(150, 150);
            this.ledPreview1.TabIndex = 3;
            // 
            // activeScriptBrowserControl1
            // 
            this.activeScriptBrowserControl1.Location = new System.Drawing.Point(12, 140);
            this.activeScriptBrowserControl1.Name = "activeScriptBrowserControl1";
            this.activeScriptBrowserControl1.Size = new System.Drawing.Size(150, 117);
            this.activeScriptBrowserControl1.TabIndex = 4;
            this.activeScriptBrowserControl1.Visible = false;
            this.activeScriptBrowserControl1.ScriptSelectionChanged += new ControlPanelUI.ActiveScriptBrowserControl.ScriptSelectionChangedEventHandler(this.activeScriptBrowserControl1_ScriptSelectionChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.activeScriptBrowserControl1);
            this.Controls.Add(this.ledPreview1);
            this.Controls.Add(this.videoRadioButton);
            this.Controls.Add(this.activeScriptRadioButton);
            this.Controls.Add(this.wallpaperRadioButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControlPanel";
            this.Text = "TaskerLight Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanel_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton wallpaperRadioButton;
        private System.Windows.Forms.RadioButton activeScriptRadioButton;
        private System.Windows.Forms.RadioButton videoRadioButton;
        private LedPreview ledPreview1;
        private ActiveScriptBrowserControl activeScriptBrowserControl1;
    }
}

