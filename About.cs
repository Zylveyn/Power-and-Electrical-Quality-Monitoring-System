using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Experiment7.Properties;

namespace Experiment7
{
    public partial class About : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form_LocationChanged(object sender, EventArgs e)
        {
            Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        private int aboutTab = 1;

        private delegate void Navigate();
        private event Navigate OnClickArrow;

        public About()
        {
            InitializeComponent();

            
        }

        private void About_OnClickArrow()
        {
            if (aboutTab != 6)
            {
                backButton.Visible = false;
                backButton.Enabled = false;
                rightArrow.Visible = true;
                rightArrow.Enabled = true;
            }
            else
            {
                backButton.Visible = true;
                backButton.Enabled = true;
                rightArrow.Visible = false;
                rightArrow.Enabled = false;
            }

            if (aboutTab == 1)
            {
                leftButton.Visible = false;
                leftButton.Enabled = false;
            }
            else
            {
                leftButton.Visible = true;
                leftButton.Enabled = true;
            }

            switch (aboutTab)
            {
                case 1:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About1;
                    break;
                case 2:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About2;
                    break;
                case 3:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About3;
                    break;
                case 4:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About4;
                    break;
                case 5:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About5;
                    break;
                case 6:
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_About6;
                    break;
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            VarContainer.tab = 3;            
        }

        // Close Button
        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseHover(object sender, EventArgs e)
        {
            closeButton.Image = Experiment7.Properties.Resources.Close_Button_On;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.Image = Experiment7.Properties.Resources.Close_Button;
        }

        // Minimize Button
        private void minButton_MouseHover(object sender, EventArgs e)
        {
            minButton.Image = Experiment7.Properties.Resources.Min_Button_On;
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minButton_MouseLeave(object sender, EventArgs e)
        {
            minButton.Image = Experiment7.Properties.Resources.Min_Button;
        }

        // Back Button
        private void backButton_Click(object sender, EventArgs e)
        {
            aboutTab = 1;
            VarContainer.tab = 1;
            this.Visible = false;
        }

        // Right Arrow
        private void rightArrow_Click(object sender, EventArgs e)
        {
            aboutTab++;
            if (OnClickArrow != null)
                OnClickArrow();
        }

        // Left Arrow
        private void leftButton_Click(object sender, EventArgs e)
        {
            aboutTab--;
            if (OnClickArrow != null)
                OnClickArrow();
        }

        private void About_VisibleChanged(object sender, EventArgs e)
        {
            this.OnClickArrow += About_OnClickArrow;

            if (OnClickArrow != null)
                OnClickArrow();
        }
    }
}
