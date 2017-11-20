namespace Experiment7
{
    partial class About
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
            this.minButton = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.PictureBox();
            this.leftButton = new System.Windows.Forms.PictureBox();
            this.rightArrow = new System.Windows.Forms.PictureBox();
            this.backButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.minButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backButton)).BeginInit();
            this.SuspendLayout();
            // 
            // minButton
            // 
            this.minButton.BackColor = System.Drawing.Color.Transparent;
            this.minButton.Image = global::Experiment7.Properties.Resources.Min_Button;
            this.minButton.Location = new System.Drawing.Point(835, 6);
            this.minButton.Name = "minButton";
            this.minButton.Size = new System.Drawing.Size(25, 25);
            this.minButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minButton.TabIndex = 20;
            this.minButton.TabStop = false;
            this.minButton.Click += new System.EventHandler(this.minButton_Click);
            this.minButton.MouseLeave += new System.EventHandler(this.minButton_MouseLeave);
            this.minButton.MouseHover += new System.EventHandler(this.minButton_MouseHover);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.Image = global::Experiment7.Properties.Resources.Close_Button;
            this.closeButton.Location = new System.Drawing.Point(868, 6);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(25, 25);
            this.closeButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeButton.TabIndex = 19;
            this.closeButton.TabStop = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            this.closeButton.MouseHover += new System.EventHandler(this.closeButton_MouseHover);
            // 
            // leftButton
            // 
            this.leftButton.BackColor = System.Drawing.Color.Transparent;
            this.leftButton.Image = global::Experiment7.Properties.Resources.Left_Arrow;
            this.leftButton.Location = new System.Drawing.Point(342, 626);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(29, 29);
            this.leftButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.leftButton.TabIndex = 21;
            this.leftButton.TabStop = false;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightArrow
            // 
            this.rightArrow.BackColor = System.Drawing.Color.Transparent;
            this.rightArrow.Image = global::Experiment7.Properties.Resources.Right_Arrow;
            this.rightArrow.Location = new System.Drawing.Point(529, 626);
            this.rightArrow.Name = "rightArrow";
            this.rightArrow.Size = new System.Drawing.Size(29, 29);
            this.rightArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rightArrow.TabIndex = 22;
            this.rightArrow.TabStop = false;
            this.rightArrow.Click += new System.EventHandler(this.rightArrow_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.Image = global::Experiment7.Properties.Resources.Back_Button;
            this.backButton.Location = new System.Drawing.Point(691, 625);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(128, 30);
            this.backButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.backButton.TabIndex = 24;
            this.backButton.TabStop = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Experiment7.Properties.Resources.BG_About1;
            this.ClientSize = new System.Drawing.Size(900, 675);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.rightArrow);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.minButton);
            this.Controls.Add(this.closeButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Experiment7.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = global::Experiment7.Properties.Settings.Default.WindowLocation;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.LocationChanged += new System.EventHandler(this.Form_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.About_VisibleChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.minButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox minButton;
        private System.Windows.Forms.PictureBox closeButton;
        private System.Windows.Forms.PictureBox leftButton;
        private System.Windows.Forms.PictureBox rightArrow;
        private System.Windows.Forms.PictureBox backButton;
    }
}