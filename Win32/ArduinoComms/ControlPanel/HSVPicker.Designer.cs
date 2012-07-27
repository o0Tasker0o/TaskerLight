namespace ControlPanel
{
    partial class HSVPicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hueSlider = new System.Windows.Forms.Panel();
            this.saturationValueBox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // hueSlider
            // 
            this.hueSlider.BackgroundImage = global::ControlPanel.Properties.Resources.Hue;
            this.hueSlider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hueSlider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hueSlider.Location = new System.Drawing.Point(187, 3);
            this.hueSlider.Name = "hueSlider";
            this.hueSlider.Size = new System.Drawing.Size(28, 180);
            this.hueSlider.TabIndex = 1;
            this.hueSlider.Paint += new System.Windows.Forms.PaintEventHandler(this.hueSlider_Paint);
            this.hueSlider.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hueSlider_MouseClick);
            this.hueSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hueSlider_MouseMove);
            // 
            // saturationValueBox
            // 
            this.saturationValueBox.BackColor = System.Drawing.Color.Red;
            this.saturationValueBox.BackgroundImage = global::ControlPanel.Properties.Resources.SaturationValue;
            this.saturationValueBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.saturationValueBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.saturationValueBox.Location = new System.Drawing.Point(3, 3);
            this.saturationValueBox.Name = "saturationValueBox";
            this.saturationValueBox.Size = new System.Drawing.Size(180, 180);
            this.saturationValueBox.TabIndex = 0;
            this.saturationValueBox.Paint += new System.Windows.Forms.PaintEventHandler(this.saturationValueBox_Paint);
            this.saturationValueBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.saturationValueBox_MouseClick);
            this.saturationValueBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.saturationValueBox_MouseMove);
            // 
            // HSVPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hueSlider);
            this.Controls.Add(this.saturationValueBox);
            this.DoubleBuffered = true;
            this.Name = "HSVPicker";
            this.Size = new System.Drawing.Size(221, 186);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel saturationValueBox;
        private System.Windows.Forms.Panel hueSlider;
    }
}
