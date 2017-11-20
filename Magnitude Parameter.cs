using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Experiment7
{
    public partial class Magnitude_Parameter : Form
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

        public Magnitude_Parameter()
        {
            InitializeComponent();
            buttonVoltage.Image = Experiment7.Properties.Resources.Radio_Button_On;
        }

        public delegate void MagnitudeParameterEvent();
        public event MagnitudeParameterEvent OnMagnParamCloseClick;

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (OnMagnParamCloseClick != null)
                OnMagnParamCloseClick();
            this.Close();
        }

        private void closeButton_MouseHover(object sender, EventArgs e)
        {
            closeButton.Image = Experiment7.Properties.Resources.Close_Button_On;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.Image = Experiment7.Properties.Resources.Close_Button;
        }
        
        public event MagnitudeParameterEvent OnMagnParamClick;

        private void buttonVoltage_Click(object sender, EventArgs e)
        {
            VarContainer.magnitude = "voltage";
            VoltageOn();
            if (OnMagnParamClick != null)
                OnMagnParamClick();
        }

        private void buttonCurrent_Click(object sender, EventArgs e)
        {
            VarContainer.magnitude = "current";
            CurrentOn();
            if (OnMagnParamClick != null)
                OnMagnParamClick();
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            VarContainer.magnitude = "power";
            PowerOn();
            if (OnMagnParamClick != null)
                OnMagnParamClick();
        }

        private void buttonQuality_Click(object sender, EventArgs e)
        {
            VarContainer.magnitude = "quality";
            QualityOn();
            if (OnMagnParamClick != null)
                OnMagnParamClick();
        }

        private void Magnitude_Parameter_Load(object sender, EventArgs e)
        {
            switch (VarContainer.magnitude)
            {
                case "voltage" :
                    VoltageOn();
                    break;
                case "current":
                    CurrentOn();
                    break;
                case "power":
                    PowerOn();
                    break;
                case "quality":
                    QualityOn();
                    break;
            }
        }

        private void VoltageOn()
        {
            buttonVoltage.Image = Experiment7.Properties.Resources.Radio_Button_On;
            buttonCurrent.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonPower.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonQuality.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonVoltage.Enabled = false;
            buttonCurrent.Enabled = true;            
            buttonPower.Enabled = true;
            buttonQuality.Enabled = true;
        }

        private void CurrentOn()
        {
            buttonVoltage.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonCurrent.Image = Experiment7.Properties.Resources.Radio_Button_On;
            buttonPower.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonQuality.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonVoltage.Enabled = true;
            buttonCurrent.Enabled = false;
            buttonPower.Enabled = true;
            buttonQuality.Enabled = true;
        }

        private void PowerOn()
        {
            buttonVoltage.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonCurrent.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonPower.Image = Experiment7.Properties.Resources.Radio_Button_On;
            buttonQuality.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonVoltage.Enabled = true;
            buttonCurrent.Enabled = true;
            buttonPower.Enabled = false;
            buttonQuality.Enabled = true;
        }

        private void QualityOn()
        {
            buttonVoltage.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonCurrent.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonPower.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            buttonQuality.Image = Experiment7.Properties.Resources.Radio_Button_On;
            buttonVoltage.Enabled = true;
            buttonCurrent.Enabled = true;
            buttonPower.Enabled = true;
            buttonQuality.Enabled = false;
        }
    }
}
