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
using MySql.Data.MySqlClient;
using System.Timers;
using System.IO;

namespace Experiment7
{
    public partial class Monitoring : Form
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

        object[] dataBuffer = new object[34];
        BackgroundWorker ArrayOperator;
        BackgroundWorker WriteData;

        public Monitoring()
        {
            InitializeComponent();

            //Settings.Default.FileNumber = 0;
            //Settings.Default.Save();

            ArrayOperator = new BackgroundWorker();
            ArrayOperator.DoWork += ArrayOperator_DoWork;
            ArrayOperator.WorkerSupportsCancellation = true;

            WriteData= new BackgroundWorker();
            WriteData.DoWork += WriteData_DoWork;
            WriteData.WorkerSupportsCancellation = true;

            VarContainer.savedData += "ID\tTime\t\t\t" +
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

        private void WriteData_DoWork(object sender, DoWorkEventArgs e)
        {
            VarContainer.savedData += Environment.NewLine;
            VarContainer.savedData += VarContainer.ID[VarContainer.ID.Length - 1].ToString() + "\t" +
                                      VarContainer.real_timeStamp[VarContainer.real_timeStamp.Length - 1].ToString() + "\t" +

                                      VarContainer.VrmsAdata[VarContainer.VrmsAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.VrmsBdata[VarContainer.VrmsBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.VrmsCdata[VarContainer.VrmsCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.VmaxAdata[VarContainer.VmaxAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.VmaxBdata[VarContainer.VmaxBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.VmaxCdata[VarContainer.VmaxCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.THDVAdata[VarContainer.THDVAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.THDVBdata[VarContainer.THDVBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.THDVCdata[VarContainer.THDVCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.IrmsAdata[VarContainer.IrmsAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.IrmsBdata[VarContainer.IrmsBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.IrmsCdata[VarContainer.IrmsCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.ImaxAdata[VarContainer.ImaxAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.ImaxBdata[VarContainer.ImaxBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.ImaxCdata[VarContainer.ImaxCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.THDIAdata[VarContainer.THDIAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.THDIBdata[VarContainer.THDIBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.THDICdata[VarContainer.THDICdata.Length - 1].ToString() + "\t" +

                                      VarContainer.SAdata[VarContainer.SAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.SBdata[VarContainer.SBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.SCdata[VarContainer.SCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.PAdata[VarContainer.PAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.PBdata[VarContainer.PBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.PCdata[VarContainer.PCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.QAdata[VarContainer.QAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.QBdata[VarContainer.QBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.QCdata[VarContainer.QCdata.Length - 1].ToString() + "\t" +

                                      VarContainer.PFAdata[VarContainer.PFAdata.Length - 1].ToString() + "\t" +
                                      VarContainer.PFBdata[VarContainer.PFBdata.Length - 1].ToString() + "\t" +
                                      VarContainer.PFCdata[VarContainer.PFCdata.Length - 1].ToString() + "\t";

            if (WriteData.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void ArrayOperator_DoWork(object sender, DoWorkEventArgs e)
        {
            Array.Copy(VarContainer.ID, 1, VarContainer.ID, 0, VarContainer.ID.Length - 1);
            Array.Copy(VarContainer.real_timeStamp, 1, VarContainer.real_timeStamp, 0, VarContainer.real_timeStamp.Length - 1);

            Array.Copy(VarContainer.timeStamp, 1, VarContainer.timeStamp, 0, VarContainer.timeStamp.Length - 1);
            Array.Copy(VarContainer.VrmsAdata, 1, VarContainer.VrmsAdata, 0, VarContainer.VrmsAdata.Length - 1);
            Array.Copy(VarContainer.VrmsBdata, 1, VarContainer.VrmsBdata, 0, VarContainer.VrmsBdata.Length - 1);
            Array.Copy(VarContainer.VrmsCdata, 1, VarContainer.VrmsCdata, 0, VarContainer.VrmsCdata.Length - 1);

            Array.Copy(VarContainer.VmaxAdata, 1, VarContainer.VmaxAdata, 0, VarContainer.VmaxAdata.Length - 1);
            Array.Copy(VarContainer.VmaxBdata, 1, VarContainer.VmaxBdata, 0, VarContainer.VmaxBdata.Length - 1);
            Array.Copy(VarContainer.VmaxCdata, 1, VarContainer.VmaxCdata, 0, VarContainer.VmaxCdata.Length - 1);

            Array.Copy(VarContainer.THDVAdata, 1, VarContainer.THDVAdata, 0, VarContainer.THDVAdata.Length - 1);
            Array.Copy(VarContainer.THDVBdata, 1, VarContainer.THDVBdata, 0, VarContainer.THDVBdata.Length - 1);
            Array.Copy(VarContainer.THDVCdata, 1, VarContainer.THDVCdata, 0, VarContainer.THDVCdata.Length - 1);

            Array.Copy(VarContainer.IrmsAdata, 1, VarContainer.IrmsAdata, 0, VarContainer.IrmsAdata.Length - 1);
            Array.Copy(VarContainer.IrmsBdata, 1, VarContainer.IrmsBdata, 0, VarContainer.IrmsBdata.Length - 1);
            Array.Copy(VarContainer.IrmsCdata, 1, VarContainer.IrmsCdata, 0, VarContainer.IrmsCdata.Length - 1);

            Array.Copy(VarContainer.ImaxAdata, 1, VarContainer.ImaxAdata, 0, VarContainer.ImaxAdata.Length - 1);
            Array.Copy(VarContainer.ImaxBdata, 1, VarContainer.ImaxBdata, 0, VarContainer.ImaxBdata.Length - 1);
            Array.Copy(VarContainer.ImaxCdata, 1, VarContainer.ImaxCdata, 0, VarContainer.ImaxCdata.Length - 1);

            Array.Copy(VarContainer.THDIAdata, 1, VarContainer.THDIAdata, 0, VarContainer.THDIAdata.Length - 1);
            Array.Copy(VarContainer.THDIBdata, 1, VarContainer.THDIBdata, 0, VarContainer.THDIBdata.Length - 1);
            Array.Copy(VarContainer.THDICdata, 1, VarContainer.THDICdata, 0, VarContainer.THDICdata.Length - 1);

            Array.Copy(VarContainer.SAdata, 1, VarContainer.SAdata, 0, VarContainer.SAdata.Length - 1);
            Array.Copy(VarContainer.SBdata, 1, VarContainer.SBdata, 0, VarContainer.SBdata.Length - 1);
            Array.Copy(VarContainer.SCdata, 1, VarContainer.SCdata, 0, VarContainer.SCdata.Length - 1);

            Array.Copy(VarContainer.PAdata, 1, VarContainer.PAdata, 0, VarContainer.PAdata.Length - 1);
            Array.Copy(VarContainer.PBdata, 1, VarContainer.PBdata, 0, VarContainer.PBdata.Length - 1);
            Array.Copy(VarContainer.PCdata, 1, VarContainer.PCdata, 0, VarContainer.PCdata.Length - 1);

            Array.Copy(VarContainer.QAdata, 1, VarContainer.QAdata, 0, VarContainer.QAdata.Length - 1);
            Array.Copy(VarContainer.QBdata, 1, VarContainer.QBdata, 0, VarContainer.QBdata.Length - 1);
            Array.Copy(VarContainer.QCdata, 1, VarContainer.QCdata, 0, VarContainer.QCdata.Length - 1);

            Array.Copy(VarContainer.PFAdata, 1, VarContainer.PFAdata, 0, VarContainer.PFAdata.Length - 1);
            Array.Copy(VarContainer.PFBdata, 1, VarContainer.PFBdata, 0, VarContainer.PFBdata.Length - 1);
            Array.Copy(VarContainer.PFCdata, 1, VarContainer.PFCdata, 0, VarContainer.PFCdata.Length - 1);

            VarContainer.real_timeStamp[VarContainer.real_timeStamp.Length - 1] = dataBuffer[32];
            VarContainer.ID[VarContainer.ID.Length - 1] = Convert.ToInt32(dataBuffer[0]);

            VarContainer.timeStamp[VarContainer.timeStamp.Length - 1] = Convert.ToDouble(dataBuffer[1]);
            VarContainer.VrmsAdata[VarContainer.VrmsAdata.Length - 1] = Convert.ToSingle(dataBuffer[2]);
            VarContainer.VrmsBdata[VarContainer.VrmsBdata.Length - 1] = Convert.ToSingle(dataBuffer[3]);
            VarContainer.VrmsCdata[VarContainer.VrmsCdata.Length - 1] = Convert.ToSingle(dataBuffer[4]);
                                                                                     
            VarContainer.VmaxAdata[VarContainer.VmaxAdata.Length - 1] = Convert.ToSingle(dataBuffer[5]);
            VarContainer.VmaxBdata[VarContainer.VmaxBdata.Length - 1] = Convert.ToSingle(dataBuffer[6]);
            VarContainer.VmaxCdata[VarContainer.VmaxCdata.Length - 1] = Convert.ToSingle(dataBuffer[7]);
                                                                                     
            VarContainer.THDVAdata[VarContainer.THDVAdata.Length - 1] = Convert.ToSingle(dataBuffer[8]);
            VarContainer.THDVBdata[VarContainer.THDVBdata.Length - 1] = Convert.ToSingle(dataBuffer[9]);
            VarContainer.THDVCdata[VarContainer.THDVCdata.Length - 1] = Convert.ToSingle(dataBuffer[10]);
                                                                                      
            VarContainer.IrmsAdata[VarContainer.IrmsAdata.Length - 1] = Convert.ToSingle(dataBuffer[11]);
            VarContainer.IrmsBdata[VarContainer.IrmsBdata.Length - 1] = Convert.ToSingle(dataBuffer[12]);
            VarContainer.IrmsCdata[VarContainer.IrmsCdata.Length - 1] = Convert.ToSingle(dataBuffer[13]);
                                                                                      
            VarContainer.ImaxAdata[VarContainer.ImaxAdata.Length - 1] = Convert.ToSingle(dataBuffer[14]);
            VarContainer.ImaxBdata[VarContainer.ImaxBdata.Length - 1] = Convert.ToSingle(dataBuffer[15]);
            VarContainer.ImaxCdata[VarContainer.ImaxCdata.Length - 1] = Convert.ToSingle(dataBuffer[16]);
                                                                                     
            VarContainer.THDIAdata[VarContainer.THDIAdata.Length - 1] = Convert.ToSingle(dataBuffer[17]);
            VarContainer.THDIBdata[VarContainer.THDIBdata.Length - 1] = Convert.ToSingle(dataBuffer[18]);
            VarContainer.THDICdata[VarContainer.THDICdata.Length - 1] = Convert.ToSingle(dataBuffer[19]);

            VarContainer.SAdata[VarContainer.SAdata.Length - 1] = Convert.ToSingle(dataBuffer[20]);
            VarContainer.SBdata[VarContainer.SBdata.Length - 1] = Convert.ToSingle(dataBuffer[21]);
            VarContainer.SCdata[VarContainer.SCdata.Length - 1] = Convert.ToSingle(dataBuffer[22]);
                                                                                 
            VarContainer.PAdata[VarContainer.PAdata.Length - 1] = Convert.ToSingle(dataBuffer[23]);
            VarContainer.PBdata[VarContainer.PBdata.Length - 1] = Convert.ToSingle(dataBuffer[24]);
            VarContainer.PCdata[VarContainer.PCdata.Length - 1] = Convert.ToSingle(dataBuffer[25]);
                                                                                 
            VarContainer.QAdata[VarContainer.QAdata.Length - 1] = Convert.ToSingle(dataBuffer[26]);
            VarContainer.QBdata[VarContainer.QBdata.Length - 1] = Convert.ToSingle(dataBuffer[27]);
            VarContainer.QCdata[VarContainer.QCdata.Length - 1] = Convert.ToSingle(dataBuffer[28]);

            VarContainer.PFAdata[VarContainer.PFAdata.Length - 1] = Convert.ToSingle(dataBuffer[29]);
            VarContainer.PFBdata[VarContainer.PFBdata.Length - 1] = Convert.ToSingle(dataBuffer[30]);
            VarContainer.PFCdata[VarContainer.PFCdata.Length - 1] = Convert.ToSingle(dataBuffer[31]);            

            if (ArrayOperator.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Monitoring_Load(object sender, EventArgs e)
        {            
            VarContainer.tab = 1;

            if (VarContainer.start)
            {
                startButton.Image = Experiment7.Properties.Resources.Stop_Button;
            }
            else
            {
                startButton.Image = Experiment7.Properties.Resources.Start_Button;
            }

            if (VarContainer.magnitude != "voltage" || VarContainer.magnitude != "current")
            {
                label3A.Visible = false;
                label3B.Visible = false;
                label3C.Visible = false;
            }
            else
            {
                label3A.Visible = true;
                label3B.Visible = true;
                label3C.Visible = true;
            }
        }

        // Save Button
        private void saveButton_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString();
            string pathfile = @Settings.Default.Path + "\\";
            string filename = "v" + Settings.Default.FileNumber.ToString() + "_" + date +".txt";
            try
            {
                File.AppendAllText(pathfile + filename, VarContainer.savedData);
                MessageBox.Show("Data has been saved to " + pathfile, "File Saved");
                Settings.Default.FileNumber++;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving Data");
            }
        }

        // Close Button
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (VarContainer.DBConnThread != null)
                VarContainer.DBConnThread.Close();
            if (VarContainer.SerialConnThread != null)
                VarContainer.SerialConnThread.Close();
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

        // Setting Button
        private void settingButton_MouseHover(object sender, EventArgs e)
        {
            settingButton.Image = Experiment7.Properties.Resources.Settings_Button_On;
        }

        private void settingButton_MouseLeave(object sender, EventArgs e)
        {
            settingButton.Image = Experiment7.Properties.Resources.Settings_Button;
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            if (!VarContainer.start)
            {
                VarContainer.tab = 2;
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("The process is running. Turn off the process first!", "Error");
            }            
        }

        // Magntiude Parameter Button
        private void settingMagnitude_Click(object sender, EventArgs e)
        {
            settingMagnitude.Image = Experiment7.Properties.Resources.Settings_Magnitude_On;

            Magnitude_Parameter f1 = new Magnitude_Parameter();
            f1.OnMagnParamClick += F1_OnMagnParamClick;
            f1.OnMagnParamCloseClick += F1_OnMagnParamCloseClick;
            f1.ShowDialog();
        }

        private void F1_OnMagnParamCloseClick()
        {
            settingMagnitude.Image = Experiment7.Properties.Resources.Settings_Magnitude;
        }

        private void F1_OnMagnParamClick()
        {
            switch (VarContainer.magnitude)
            {
                case "voltage"  :
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Voltage;
                    label3A.Visible = false;
                    label3B.Visible = false;
                    label3C.Visible = false;
                    VarContainer.graph = "vrms";
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_VRMS;
                    
                    label1A.Text = VarContainer.VrmsAdata[VarContainer.VrmsAdata.Length - 1].ToString();
                    label1B.Text = VarContainer.VrmsBdata[VarContainer.VrmsBdata.Length - 1].ToString();
                    label1C.Text = VarContainer.VrmsCdata[VarContainer.VrmsCdata.Length - 1].ToString();

                    label2A.Text = VarContainer.VmaxAdata[VarContainer.VmaxAdata.Length - 1].ToString();
                    label2B.Text = VarContainer.VmaxBdata[VarContainer.VmaxBdata.Length - 1].ToString();
                    label2C.Text = VarContainer.VmaxCdata[VarContainer.VmaxCdata.Length - 1].ToString();

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsCdata[i]);
                    }

                    break;
                case "current"  :
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Current;
                    label3A.Visible = false;
                    label3B.Visible = false;
                    label3C.Visible = false;
                    VarContainer.graph = "irms";
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_IRMS;
                    
                    label1A.Text = VarContainer.IrmsAdata[VarContainer.IrmsAdata.Length - 1].ToString();
                    label1B.Text = VarContainer.IrmsBdata[VarContainer.IrmsBdata.Length - 1].ToString();
                    label1C.Text = VarContainer.IrmsCdata[VarContainer.IrmsCdata.Length - 1].ToString();
                                                                          
                    label2A.Text = VarContainer.ImaxAdata[VarContainer.ImaxAdata.Length - 1].ToString();
                    label2B.Text = VarContainer.ImaxBdata[VarContainer.ImaxBdata.Length - 1].ToString();
                    label2C.Text = VarContainer.ImaxCdata[VarContainer.ImaxCdata.Length - 1].ToString();

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsCdata[i]);
                    }
                    break;
                case "power"    :
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Power;
                    label3A.Visible = true;
                    label3B.Visible = true;
                    label3C.Visible = true;
                    VarContainer.graph = "s";
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_S;
                    
                    label1A.Text = VarContainer.SAdata[VarContainer.SAdata.Length - 1].ToString();
                    label1B.Text = VarContainer.SBdata[VarContainer.SBdata.Length - 1].ToString();
                    label1C.Text = VarContainer.SCdata[VarContainer.SCdata.Length - 1].ToString();

                    label2A.Text = VarContainer.PAdata[VarContainer.PAdata.Length - 1].ToString();
                    label2B.Text = VarContainer.PBdata[VarContainer.PBdata.Length - 1].ToString();
                    label2C.Text = VarContainer.PCdata[VarContainer.PCdata.Length - 1].ToString();

                    label3A.Text = VarContainer.QAdata[VarContainer.QAdata.Length - 1].ToString();
                    label3B.Text = VarContainer.QBdata[VarContainer.QBdata.Length - 1].ToString();
                    label3C.Text = VarContainer.QCdata[VarContainer.QCdata.Length - 1].ToString();

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SCdata[i]);
                    }

                    break;
                case "quality"  :
                    this.BackgroundImage = Experiment7.Properties.Resources.BG_Quality;
                    label3A.Visible = true;
                    label3B.Visible = true;
                    label3C.Visible = true;
                    VarContainer.graph = "thdv";
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_THDV;
                    
                    label1A.Text = VarContainer.THDVAdata[VarContainer.THDVAdata.Length - 1].ToString();
                    label1B.Text = VarContainer.THDVBdata[VarContainer.THDVBdata.Length - 1].ToString();
                    label1C.Text = VarContainer.THDVCdata[VarContainer.THDVCdata.Length - 1].ToString();

                    label2A.Text = VarContainer.THDIAdata[VarContainer.THDIAdata.Length - 1].ToString();
                    label2B.Text = VarContainer.THDIBdata[VarContainer.THDIBdata.Length - 1].ToString();
                    label2C.Text = VarContainer.THDICdata[VarContainer.THDICdata.Length - 1].ToString();

                    label3A.Text = VarContainer.PFAdata[VarContainer.PFAdata.Length - 1].ToString();
                    label3B.Text = VarContainer.PFBdata[VarContainer.PFBdata.Length - 1].ToString();
                    label3C.Text = VarContainer.PFCdata[VarContainer.PFCdata.Length - 1].ToString();

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVCdata[i]);
                    }
                    
                    break;
            }
        }

        // Graph Parameter Button
        private void settingGraph_Click(object sender, EventArgs e)
        {
            settingGraph.Image = Experiment7.Properties.Resources.Settings_Graph_On;

            Graph_Parameter f1 = new Graph_Parameter();
            f1.OnGraphParamCloseClick += F1_OnGraphParamCloseClick;
            f1.OnGraphParamClick += F1_OnGraphParamClick;
            f1.ShowDialog(); 
        }

        private void F1_OnGraphParamClick()
        {
            switch (VarContainer.graph)
            {
                case "vrms" :
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_VRMS;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsCdata[i]);
                    }
                    break;

                case "vmax":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_VMAX;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxCdata[i]);
                    }
                    break;

                case "thdv":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_THDV;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVCdata[i]);
                    }
                    break;

                case "irms":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_IRMS;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsCdata[i]);
                    }
                    break;

                case "imax":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_IMAX;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxCdata[i]);
                    }
                    break;

                case "thdi":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_THDI;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDIAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDIBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDICdata[i]);
                    }
                    break;

                case "s":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_S;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SCdata[i]);
                    }
                    break;

                case "p":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_P;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PCdata[i]);
                    }
                    break;

                case "q":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_Q;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QCdata[i]);
                    }
                    break;

                case "pf":
                    labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_PF;

                    graph.Series["Phase A"].Points.Clear();
                    graph.Series["Phase B"].Points.Clear();
                    graph.Series["Phase C"].Points.Clear();

                    for (int i = 0; i < 10; i++)
                    {
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFCdata[i]);
                    }
                    break;
            }
        }

        private void F1_OnGraphParamCloseClick()
        {
            settingGraph.Image = Experiment7.Properties.Resources.Settings_Graph;
        }

        // Start Button
        private void startButton_Click(object sender, EventArgs e)
        {
            if (!VarContainer.start)
            {
                startButton.Image = Experiment7.Properties.Resources.Stop_Button;
                VarContainer.start = true;
                
                if (Settings.Default.ConnectionMode == "ethernet")
                {
                    if (VarContainer.DBConnThread == null)
                    {
                        VarContainer.DBConnThread = new DBThread();
                        VarContainer.DBConnThread.DataReceived += DBThreadDataReceived;
                        VarContainer.DBConnThread.DBConnCheck += DBConnThread_DBConnCheck;
                        VarContainer.DBConnThread.Start();
                    }
                }
                else if (Settings.Default.ConnectionMode == "serial")
                {
                    if (VarContainer.SerialConnThread == null)
                    {
                        VarContainer.SerialConnThread = new SerialThread();
                        VarContainer.SerialConnThread.DataReceived += SerialThreadDataReceived;
                        VarContainer.SerialConnThread.SerialConnCheck += SerialConnThread_SerialConnCheck;
                        VarContainer.SerialConnThread.Start();
                    }
                }
                
            }
            else
            {
                startButton.Image = Experiment7.Properties.Resources.Start_Button;
                VarContainer.start = false;
                VarContainer.savedData += Environment.NewLine + Environment.NewLine;
                VarContainer.savedData += "*******************************************************" +
                                          "***********************************************************" +
                                          "***********************************************************" +
                                          "***********************************************************" +
                                          "****************************************\r";
            }
        }

        private void SerialConnThread_SerialConnCheck(object sender, ConnDataEventArgs e)
        {
            if (VarContainer.start)
            {
                Invoke((new EventHandler<ConnDataEventArgs>(SerialConnCheckMethod)), new object[] { sender, e });
            }
        }

        private void DBConnThread_DBConnCheck(object sender, ConnDataEventArgs e)
        {
            if (VarContainer.start)
            {
                Invoke((new EventHandler<ConnDataEventArgs>(DBConnCheckMethod)), new object[] { sender, e });
            }
        }

        private void SerialConnCheckMethod(object sender, ConnDataEventArgs e)
        {
            if (!e.connected)
            {
                startButton.Image = Experiment7.Properties.Resources.Start_Button;
                VarContainer.start = false;
                VarContainer.milisecs = 0;
                VarContainer.SerialConnThread.Close();
                VarContainer.SerialConnThread = null;
            }
        }

        private void DBConnCheckMethod(object sender, ConnDataEventArgs e)
        {
            if (!e.connected)
            {
                startButton.Image = Experiment7.Properties.Resources.Start_Button;
                VarContainer.start = false;
                VarContainer.milisecs = 0;
                VarContainer.DBConnThread.Close();
                VarContainer.DBConnThread = null;
            }
        }

        private void SerialThreadDataReceived (object sender, DataEventArgs e)
        {
            if (VarContainer.start)
                Invoke((new EventHandler<DataEventArgs>(ThreadDataReceivedSync)), new object[] { sender, e });
        }

        private void ThreadDataReceivedSync(object sender, DataEventArgs e)
        {
            dataBuffer = e.Data;

            //label1A.Text = dataBuffer[4].ToString();

            ArrayOperator.RunWorkerAsync();

            LabelandGraph();

            WriteData.RunWorkerAsync();
        }

        private void DBThreadDataReceived(object sender, MyDataEventArgs e)
        {
            // butuh validasi koneksi udah open
            if (VarContainer.start)
            {
                Invoke((new EventHandler<MyDataEventArgs>(DBThreadDataReceivedSync)), new object[] { sender, e });
            }
        }        

        private void DBThreadDataReceivedSync(object sender, MyDataEventArgs e)
        {
            if ((int)e.Data[0] != VarContainer.mainID)
            {
                VarContainer.mainID = (int)e.Data[0];
                          
                dataBuffer = e.Data;

                ArrayOperator.RunWorkerAsync();

                LabelandGraph();

                WriteData.RunWorkerAsync();

                //VarContainer.dummyCounter++;
            }            
        }
        
        private void LabelandGraph()
        {
            graph.Series["Phase A"].Points.Clear();
            graph.Series["Phase B"].Points.Clear();
            graph.Series["Phase C"].Points.Clear();

            for (int i = 0; i < 10; ++i)
            {
                switch (VarContainer.graph)
                {
                    case "vrms":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VrmsCdata[i]);
                        break;
                    case "vmax":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.VmaxCdata[i]);
                        break;
                    case "thdv":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDVCdata[i]);
                        break;
                    case "irms":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.IrmsCdata[i]);
                        break;
                    case "imax":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.ImaxCdata[i]);
                        break;
                    case "thdi":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDIAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDIBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.THDICdata[i]);
                        break;
                    case "s":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.SCdata[i]);
                        break;
                    case "p":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PCdata[i]);
                        break;
                    case "q":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.QCdata[i]);
                        break;
                    case "pf":
                        graph.Series["Phase A"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFAdata[i]);
                        graph.Series["Phase B"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFBdata[i]);
                        graph.Series["Phase C"].Points.AddXY(VarContainer.timeStamp[i], VarContainer.PFCdata[i]);
                        break;
                }
            }

            switch (VarContainer.magnitude)
            {
                case "voltage":
                    label1A.Text = dataBuffer[2].ToString();
                    label1B.Text = dataBuffer[3].ToString();
                    label1C.Text = dataBuffer[4].ToString();

                    label2A.Text = dataBuffer[5].ToString();
                    label2B.Text = dataBuffer[6].ToString();
                    label2C.Text = dataBuffer[7].ToString();
                    break;

                case "current":
                    label1A.Text = dataBuffer[11].ToString();
                    label1B.Text = dataBuffer[12].ToString();
                    label1C.Text = dataBuffer[13].ToString();

                    label2A.Text = dataBuffer[14].ToString();
                    label2B.Text = dataBuffer[15].ToString();
                    label2C.Text = dataBuffer[16].ToString();
                    break;

                case "power":
                    label1A.Text = dataBuffer[20].ToString();
                    label1B.Text = dataBuffer[21].ToString();
                    label1C.Text = dataBuffer[22].ToString();

                    label2A.Text = dataBuffer[23].ToString();
                    label2B.Text = dataBuffer[24].ToString();
                    label2C.Text = dataBuffer[25].ToString();

                    label3A.Text = dataBuffer[26].ToString();
                    label3B.Text = dataBuffer[27].ToString();
                    label3C.Text = dataBuffer[28].ToString();
                    break;

                case "quality":
                    label1A.Text = dataBuffer[8].ToString();
                    label1B.Text = dataBuffer[9].ToString();
                    label1C.Text = dataBuffer[10].ToString();

                    label2A.Text = dataBuffer[17].ToString();
                    label2B.Text = dataBuffer[18].ToString();
                    label2C.Text = dataBuffer[19].ToString();

                    label3A.Text = dataBuffer[29].ToString();
                    label3B.Text = dataBuffer[30].ToString();
                    label3C.Text = dataBuffer[31].ToString();
                    break;
            }
        }

        private void logo_Click(object sender, EventArgs e)
        {
            VarContainer.tab = 3;
            this.Visible = false;
        }
    }
}
