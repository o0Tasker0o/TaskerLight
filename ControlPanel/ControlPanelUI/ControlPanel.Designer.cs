﻿namespace ControlPanelUI
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
            this.wallpaperRadioButton = new System.Windows.Forms.RadioButton();
            this.activeScriptRadioButton = new System.Windows.Forms.RadioButton();
            this.videoRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // wallpaperRadioButton
            // 
            this.wallpaperRadioButton.AutoSize = true;
            this.wallpaperRadioButton.Checked = true;
            this.wallpaperRadioButton.Location = new System.Drawing.Point(12, 12);
            this.wallpaperRadioButton.Name = "wallpaperRadioButton";
            this.wallpaperRadioButton.Size = new System.Drawing.Size(73, 17);
            this.wallpaperRadioButton.TabIndex = 0;
            this.wallpaperRadioButton.TabStop = true;
            this.wallpaperRadioButton.Text = "Wallpaper";
            this.wallpaperRadioButton.UseVisualStyleBackColor = true;
            this.wallpaperRadioButton.CheckedChanged += new System.EventHandler(this.modeRadioButton_CheckedChanged);
            // 
            // activeScriptRadioButton
            // 
            this.activeScriptRadioButton.AutoSize = true;
            this.activeScriptRadioButton.Location = new System.Drawing.Point(12, 35);
            this.activeScriptRadioButton.Name = "activeScriptRadioButton";
            this.activeScriptRadioButton.Size = new System.Drawing.Size(85, 17);
            this.activeScriptRadioButton.TabIndex = 1;
            this.activeScriptRadioButton.Text = "Active Script";
            this.activeScriptRadioButton.UseVisualStyleBackColor = true;
            this.activeScriptRadioButton.CheckedChanged += new System.EventHandler(this.modeRadioButton_CheckedChanged);
            // 
            // videoRadioButton
            // 
            this.videoRadioButton.AutoSize = true;
            this.videoRadioButton.Location = new System.Drawing.Point(12, 58);
            this.videoRadioButton.Name = "videoRadioButton";
            this.videoRadioButton.Size = new System.Drawing.Size(52, 17);
            this.videoRadioButton.TabIndex = 2;
            this.videoRadioButton.Text = "Video";
            this.videoRadioButton.UseVisualStyleBackColor = true;
            this.videoRadioButton.CheckedChanged += new System.EventHandler(this.modeRadioButton_CheckedChanged);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.videoRadioButton);
            this.Controls.Add(this.activeScriptRadioButton);
            this.Controls.Add(this.wallpaperRadioButton);
            this.Name = "ControlPanel";
            this.Text = "TaskerLight";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlPanel_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton wallpaperRadioButton;
        private System.Windows.Forms.RadioButton activeScriptRadioButton;
        private System.Windows.Forms.RadioButton videoRadioButton;
    }
}
