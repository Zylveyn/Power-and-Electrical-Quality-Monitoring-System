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
    public partial class Graph_Parameter : Form
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

        public Graph_Parameter()
        {
            InitializeComponent();
        }

        public delegate void GraphParameterEvent();
        public event GraphParameterEvent OnGraphParamCloseClick;

        private void Graph_Parameter_Load(object sender, EventArgs e)
        {
            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
            switch (VarContainer.magnitude)
            {
                case "voltage":
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_ParamGraphVoltage;
                    button3.Visible = false;
                    switch (VarContainer.graph)
                    {
                        case "vmax" :
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_On;

                            ButtonEnable(23);

                            break;
                        default:
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(13);

                            break;
                    }
                    break;

                case "current":
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_ParamGraphCurrent;
                    button3.Visible = false;
                    switch (VarContainer.graph)
                    {
                        case "imax":
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_On;

                            ButtonEnable(23);

                            break;
                        default:
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(13);

                            break;
                    }
                    break;

                case "power":
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_ParamGraphPower;
                    button3.Visible = true;
                    switch (VarContainer.graph)
                    {
                        case "p":
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(2);

                            break;
                        case "q":
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_On;

                            ButtonEnable(3);

                            break;
                        default:
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(1);

                            break;
                    }
                    break;

                case "quality":
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_ParamGraphQuality;
                    button3.Visible = true;
                    switch (VarContainer.graph)
                    {
                        case "p":
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(2);

                            break;
                        case "q":
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_On;

                            ButtonEnable(3);

                            break;
                        default:
                            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
                            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
                            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

                            ButtonEnable(1);

                            break;
                    }
                    break;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (OnGraphParamCloseClick != null)
                OnGraphParamCloseClick();
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

        public event GraphParameterEvent OnGraphParamClick;

        private void button1_Click(object sender, EventArgs e)
        {
            switch (VarContainer.magnitude)
            {
                case "voltage":
                    VarContainer.graph = "vrms";
                    break;

                case "current":
                    VarContainer.graph = "irms";
                    break;

                case "power":
                    VarContainer.graph = "s";
                    break;

                case "quality":
                    VarContainer.graph = "thdv";
                    break;
            }
            button1.Image = Experiment7.Properties.Resources.Radio_Button_On;
            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

            ButtonEnable(1);

            if (OnGraphParamClick != null)
                OnGraphParamClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (VarContainer.magnitude)
            {
                case "voltage":
                    VarContainer.graph = "vmax";
                    break;

                case "current":
                    VarContainer.graph = "imax";
                    break;

                case "power":
                    VarContainer.graph = "p";
                    break;

                case "quality":
                    VarContainer.graph = "thdi";
                    break;
            }
            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            button2.Image = Experiment7.Properties.Resources.Radio_Button_On;
            button3.Image = Experiment7.Properties.Resources.Radio_Button_Off;

            ButtonEnable(2);

            if (OnGraphParamClick != null)
                OnGraphParamClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (VarContainer.magnitude)
            {
                case "power":
                    VarContainer.graph = "q";
                    break;

                case "quality":
                    VarContainer.graph = "pf";
                    break;
            }
            button1.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            button2.Image = Experiment7.Properties.Resources.Radio_Button_Off;
            button3.Image = Experiment7.Properties.Resources.Radio_Button_On;

            ButtonEnable(3);

            if (OnGraphParamClick != null)
                OnGraphParamClick();
        }

        private void ButtonEnable (int idx)
        {
            switch (idx)
            {
                case 1:
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    break;
                case 2:
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    break;
                case 3:
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    break;
                case 12:
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    break;
                case 13:
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    break;
                case 23:
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    break;
            }
        }
    }
}
