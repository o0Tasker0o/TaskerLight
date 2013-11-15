namespace ControlPanelUI
{
    partial class ScriptCompilerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptCompilerForm));
            this.codeTextbox = new System.Windows.Forms.TextBox();
            this.outputTextbox = new System.Windows.Forms.TextBox();
            this.compileButton = new System.Windows.Forms.Button();
            this.outputDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // codeTextbox
            // 
            this.codeTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeTextbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextbox.Location = new System.Drawing.Point(12, 39);
            this.codeTextbox.Multiline = true;
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.codeTextbox.Size = new System.Drawing.Size(466, 216);
            this.codeTextbox.TabIndex = 0;
            this.codeTextbox.Text = resources.GetString("codeTextbox.Text");
            this.codeTextbox.WordWrap = false;
            // 
            // outputTextbox
            // 
            this.outputTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextbox.BackColor = System.Drawing.SystemColors.WindowText;
            this.outputTextbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextbox.ForeColor = System.Drawing.Color.LimeGreen;
            this.outputTextbox.Location = new System.Drawing.Point(12, 261);
            this.outputTextbox.Multiline = true;
            this.outputTextbox.Name = "outputTextbox";
            this.outputTextbox.ReadOnly = true;
            this.outputTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextbox.Size = new System.Drawing.Size(385, 92);
            this.outputTextbox.TabIndex = 1;
            this.outputTextbox.WordWrap = false;
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Location = new System.Drawing.Point(403, 261);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(75, 23);
            this.compileButton.TabIndex = 2;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // outputDirectoryTextbox
            // 
            this.outputDirectoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputDirectoryTextbox.Location = new System.Drawing.Point(83, 12);
            this.outputDirectoryTextbox.Name = "outputDirectoryTextbox";
            this.outputDirectoryTextbox.Size = new System.Drawing.Size(395, 20);
            this.outputDirectoryTextbox.TabIndex = 3;
            this.outputDirectoryTextbox.Text = "MyScript";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Script Name";
            // 
            // ScriptCompilerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 365);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputDirectoryTextbox);
            this.Controls.Add(this.compileButton);
            this.Controls.Add(this.outputTextbox);
            this.Controls.Add(this.codeTextbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "ScriptCompilerForm";
            this.Text = "TaskerLight Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeTextbox;
        private System.Windows.Forms.TextBox outputTextbox;
        private System.Windows.Forms.Button compileButton;
        private System.Windows.Forms.TextBox outputDirectoryTextbox;
        private System.Windows.Forms.Label label1;
    }
}