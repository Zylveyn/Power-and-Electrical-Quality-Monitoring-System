namespace Experiment7
{
    partial class Magnitude_Parameter
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
            this.buttonVoltage = new System.Windows.Forms.PictureBox();
            this.buttonQuality = new System.Windows.Forms.PictureBox();
            this.buttonCurrent = new System.Windows.Forms.PictureBox();
            this.buttonPower = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.buttonVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonVoltage
            // 
            this.buttonVoltage.BackColor = System.Drawing.Color.Transparent;
            this.buttonVoltage.Image = global::Experiment7.Properties.Resources.Radio_Button_Off;
            this.buttonVoltage.Location = new System.Drawing.Point(232, 123);
            this.buttonVoltage.Name = "buttonVoltage";
            this.buttonVoltage.Size = new System.Drawing.Size(16, 16);
            this.buttonVoltage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.buttonVoltage.TabIndex = 0;
            this.buttonVoltage.TabStop = false;
            this.buttonVoltage.Click += new System.EventHandler(this.buttonVoltage_Click);
            // 
            // buttonQuality
            // 
            this.buttonQuality.BackColor = System.Drawing.Color.Transparent;
            this.buttonQuality.Image = global::Experiment7.Properties.Resources.Radio_Button_Off;
            this.buttonQuality.Location = new System.Drawing.Point(232, 252);
            this.buttonQuality.Name = "buttonQuality";
            this.buttonQuality.Size = new System.Drawing.Size(16, 16);
            this.buttonQuality.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.buttonQuality.TabIndex = 1;
            this.buttonQuality.TabStop = false;
            this.buttonQuality.Click += new System.EventHandler(this.buttonQuality_Click);
            // 
            // buttonCurrent
            // 
            this.buttonCurrent.BackColor = System.Drawing.Color.Transparent;
            this.buttonCurrent.Image = global::Experiment7.Properties.Resources.Radio_Button_Off;
            this.buttonCurrent.Location = new System.Drawing.Point(232, 166);
            this.buttonCurrent.Name = "buttonCurrent";
            this.buttonCurrent.Size = new System.Drawing.Size(16, 16);
            this.buttonCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.buttonCurrent.TabIndex = 2;
            this.buttonCurrent.TabStop = false;
            this.buttonCurrent.Click += new System.EventHandler(this.buttonCurrent_Click);
            // 
            // buttonPower
            // 
            this.buttonPower.BackColor = System.Drawing.Color.Transparent;
            this.buttonPower.Image = global::Experiment7.Properties.Resources.Radio_Button_Off;
            this.buttonPower.Location = new System.Drawing.Point(232, 209);
            this.buttonPower.Name = "buttonPower";
            this.buttonPower.Size = new System.Drawing.Size(16, 16);
            this.buttonPower.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.buttonPower.TabIndex = 3;
            this.buttonPower.TabStop = false;
            this.buttonPower.Click += new System.EventHandler(this.buttonPower_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.Image = global::Experiment7.Properties.Resources.Close_Button;
            this.closeButton.Location = new System.Drawing.Point(363, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(25, 25);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeButton.TabIndex = 14;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            this.closeButton.MouseHover += new System.EventHandler(this.closeButton_MouseHover);
            // 
            // Magnitude_Parameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Experiment7.Properties.Resources.BG_ParameterMagnitude;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.buttonPower);
            this.Controls.Add(this.buttonCurrent);
            this.Controls.Add(this.buttonQuality);
            this.Controls.Add(this.buttonVoltage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Magnitude_Parameter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Magnitude_Parameter";
            this.Load += new System.EventHandler(this.Magnitude_Parameter_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.buttonVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox buttonVoltage;
        private System.Windows.Forms.PictureBox buttonQuality;
        private System.Windows.Forms.PictureBox buttonCurrent;
        private System.Windows.Forms.PictureBox buttonPower;
        private System.Windows.Forms.PictureBox closeButton;
    }
}