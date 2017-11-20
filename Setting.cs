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
using System.IO;

namespace Experiment7
{
    public partial class Setting : Form
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

        private string connMode = Settings.Default.ConnectionMode;

        private void Form_LocationChanged(object sender, EventArgs e)
        {
            Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            VarContainer.tab = 2;
            path.Text = Settings.Default.Path;
            if (Settings.Default.ConnectionMode == "ethernet")
            {
                this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting;
                ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                serialButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                COMPortText.Visible = false;
                COMPortText.Enabled = false;
                localhostButton.Visible = true;
                localhostButton.Enabled = true;
                serverButton.Visible = true;
                serverButton.Enabled = true;

                if (Settings.Default.Database == "localhost")
                {
                    localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                    localhostButton.Enabled = false;
                    serverButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                    serverButton.Enabled = true;
                }
                else if (Settings.Default.Database == "server")
                {
                    serverButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                    serverButton.Enabled = false;
                    localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                    localhostButton.Enabled = true;
                }
            }
            else
            {
                this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting2;
                ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                serialButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                COMPortText.Visible = true;
                COMPortText.Enabled = true;
                localhostButton.Visible = false;
                localhostButton.Enabled = false;
                serverButton.Visible = false;
                serverButton.Enabled = false;
                COMPortText.Text = Settings.Default.Port;
            }
        }

        // Close Button
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (Settings.Default.ConnectionMode == "ethernet")
            {
                if (VarContainer.DBConnThread != null)
                    VarContainer.DBConnThread.Close();
            }
            else
            {
                if (VarContainer.SerialConnThread != null)
                    VarContainer.SerialConnThread.Close();
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            VarContainer.tab = 1;
            this.Visible = false;
        }

        public delegate void ChangeEventRaiser();
        public event ChangeEventRaiser OnClick;

        public void button2_Click(object sender, EventArgs e)
        {
            if (OnClick != null)
                OnClick();
        }

        private bool saved = false;

        // Apply Button
        private void applyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (connMode == "serial")
                {
                    if (string.IsNullOrEmpty(COMPortText.Text))
                    {
                        MessageBox.Show("COM Port must not be blank", "Error");
                        return;
                    }
                    else
                    {
                        Settings.Default.Port = COMPortText.Text;
                    }
                }                    

                if (connMode != Settings.Default.ConnectionMode)
                {
                    if (connMode == "serial" && VarContainer.SerialConnThread != null)
                    {
                        VarContainer.SerialConnThread.Close();
                        VarContainer.SerialConnThread = null;
                    }
                    else if (connMode == "ethernet" && VarContainer.DBConnThread != null)
                    {
                        VarContainer.DBConnThread.Close();
                        VarContainer.DBConnThread = null;
                    }
                    ResetArray();
                }

                if (ethDB != Settings.Default.Database && VarContainer.DBConnThread != null)
                {
                    VarContainer.DBConnThread.Close();
                    VarContainer.DBConnThread = null;
                    ResetArray();
                }

                Settings.Default.ConnectionMode = connMode;
                Settings.Default.Database = ethDB;
                Settings.Default.Save();
                MessageBox.Show("Settings Saved", "Saved");
                saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                saved = false;
            }
        }

        // Back Button
        private void backButton_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                if (Settings.Default.ConnectionMode == "serial")
                {
                    COMPortText.Visible = true;
                    COMPortText.Enabled = true;
                    localhostButton.Visible = false;
                    localhostButton.Enabled = false;
                    serverButton.Visible = false;
                    serverButton.Enabled = false;
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting2;
                    ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                    serialButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                    ethernetButton.Enabled = true;
                    serialButton.Enabled = false;
                }
                else if (Settings.Default.ConnectionMode == "ethernet")
                {
                    COMPortText.Visible = false;
                    COMPortText.Enabled = false;
                    localhostButton.Visible = true;
                    localhostButton.Enabled = true;
                    serverButton.Visible = true;
                    serverButton.Enabled = true;
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting;
                    ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                    serialButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                    ethernetButton.Enabled = false;
                    serialButton.Enabled = true;
                    if (Settings.Default.Database == "server")
                    {
                        serverButton.Enabled = false;
                        serverButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                        localhostButton.Enabled = true;
                        localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                    }
                    else if (Settings.Default.Database == "localhost")
                    {
                        serverButton.Enabled = true;
                        serverButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                        localhostButton.Enabled = false;
                        localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
                    }
                }
            }
            VarContainer.tab = 1;
            this.Visible = false;
        }

        // Ethernet Button
        private void ethernetButton_Click(object sender, EventArgs e)
        {
            connMode = "ethernet";
            VarContainer.conn = connMode;
            this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting;            
            ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
            serialButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            ethernetButton.Enabled = false;
            serialButton.Enabled = true;
            COMPortText.Visible = false;
            COMPortText.Enabled = false;
            localhostButton.Visible = true;
            localhostButton.Enabled = true;
            serverButton.Visible = true;
            serverButton.Enabled = true;

            ethDB = "localhost";
            localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
            localhostButton.Enabled = false;
            serverButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            serverButton.Enabled = true;
        }

        // Serial Button
        private void serialButton_Click(object sender, EventArgs e)
        {            
            connMode = "serial";
            VarContainer.conn = connMode;
            this.BackgroundImage = Experiment7.Properties.Resources.BG_Setting2;            
            ethernetButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            serialButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
            ethernetButton.Enabled = true;
            serialButton.Enabled = false;
            COMPortText.Visible = true;
            COMPortText.Enabled = true;
            localhostButton.Visible = false;
            localhostButton.Enabled = false;
            serverButton.Visible = false;
            serverButton.Enabled = false;
        }

        // Browse Button
        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog filedialog = new FolderBrowserDialog();
            if (filedialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                Settings.Default.Path = filedialog.SelectedPath;
                path.Text = Settings.Default.Path;
                MessageBox.Show(Settings.Default.Path);
            }
        }

        private void ResetArray()
        {
            VarContainer.VmaxAdata = new float[10];
            VarContainer.VmaxBdata = new float[10];
            VarContainer.VmaxCdata = new float[10];
                                     
            VarContainer.VrmsAdata = new float[10];
            VarContainer.VrmsBdata = new float[10];
            VarContainer.VrmsCdata = new float[10];
                                     
            VarContainer.THDVAdata = new float[10];
            VarContainer.THDVBdata = new float[10];
            VarContainer.THDVCdata = new float[10];
                                     
            VarContainer.ImaxAdata = new float[10];
            VarContainer.ImaxBdata = new float[10];
            VarContainer.ImaxCdata = new float[10];
                                     
            VarContainer.IrmsAdata = new float[10];
            VarContainer.IrmsBdata = new float[10];
            VarContainer.IrmsCdata = new float[10];
                                     
            VarContainer.THDIAdata = new float[10];
            VarContainer.THDIBdata = new float[10];
            VarContainer.THDICdata = new float[10];
            
            VarContainer.SAdata = new float[10];
            VarContainer.SBdata = new float[10];
            VarContainer.SCdata = new float[10];
                                  
            VarContainer.PAdata = new float[10];
            VarContainer.PBdata = new float[10];
            VarContainer.PCdata = new float[10];
                                  
            VarContainer.QAdata = new float[10];
            VarContainer.QBdata = new float[10];
            VarContainer.QCdata = new float[10];
            
            VarContainer.PFAdata = new float[10];
            VarContainer.PFBdata = new float[10];
            VarContainer.PFCdata = new float[10];

            VarContainer.ID = new int[10];
            VarContainer.real_timeStamp = new object[10];
            VarContainer.timeStamp = new double[10];

            VarContainer.idx = 1;
            VarContainer.milisecs = 0;

            VarContainer.savedData = "ID\tTime\t\t\t" +
                                      "VrmsA\tVrmsB\tVrmsC\t" +
                                      "VmaxA\tVmaxB\tVmaxC\t" +
                                      "THDVA\tTHDVB\tTHDVC\t" +
                                      "IrmsA\tIrmsB\tIrmsC\t" +
                                      "ImaxA\tImaxB\tImaxC\t" +
                                      "THDIA\tTHDIB\tTHDIC\t" +
                                      "SA\tSB\tSC\t" +
                                      "PA\tPB\tPC\t" +
                                      "QA\tQB\tQC\t" +
                                      "PFA\tPFB\tPFC" + Environment.NewLine;
        }

        private string ethDB;

        private void localhostButton_Click(object sender, EventArgs e)
        {
            ethDB = "localhost";
            localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
            localhostButton.Enabled = false;
            serverButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            serverButton.Enabled = true;
        }

        private void serverButton_Click(object sender, EventArgs e)
        {
            ethDB = "server";
            serverButton.Image = Experiment7.Properties.Resources.Radio_Button_On;
            serverButton.Enabled = false;
            localhostButton.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            localhostButton.Enabled = true;
        }
    }
}
