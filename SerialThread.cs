using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;
using System.Timers;
using Experiment7.Properties;

namespace Experiment7
{
    public class SerialThread
    {
        private Thread t;

        public SerialThread()
        {
            t = new Thread(RunMethod);
        }

        public void Sleep()
        {
            t.Suspend();
        }

        public void Awake()
        {
            t.Resume();
        }

        public bool IsAwake()
        {
            return t.IsAlive;
        }

        public void Start()
        {
            t.Start();
        }

        public void Stop()
        {
            t.Interrupt();
            if (!t.Join(1000))
                t.Abort();
        }

        public event EventHandler<DataEventArgs> DataReceived;
        public event ElapsedEventHandler OnTime;

        private bool closed = false;

        public void Close()
        {
            closed = true;
            VarContainer.start = false;
        }

        public void timeStop()
        {
            timer.Dispose();
        }

        SerialPort mySerialPort = new SerialPort();
        System.Threading.Timer timer;

        public EventHandler<ConnDataEventArgs> SerialConnCheck;
        private bool SerialConn = false;

        private void RunMethod()
        {
            mySerialPort.PortName = Settings.Default.Port;
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;

            timer = new System.Threading.Timer(OnTimedEvent, null, 0, 100);

            try
            {
                mySerialPort.Open();
                SerialConn = true;

                if (SerialConnCheck != null)
                    SerialConnCheck(this, new ConnDataEventArgs(SerialConn));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                closed = true;
                SerialConn = false;

                if (SerialConnCheck != null)
                    SerialConnCheck(this, new ConnDataEventArgs(SerialConn));
            }

            while (!closed)
            {
                string line = mySerialPort.ReadLine();
                if (VarContainer.check(line) == 30)
                {
                    object[] dataBuffer = new object[33];
                    dataBuffer[0] = VarContainer.split(line, 0);
                    double dummy = VarContainer.milisecs * 0.1;
                    dataBuffer[1] = Math.Round(dummy, 2);
                    dataBuffer[2] = VarContainer.split(line, 1);
                    dataBuffer[3] = VarContainer.split(line, 2);
                    dataBuffer[4] = VarContainer.split(line, 3);
                    dataBuffer[5] = VarContainer.split(line, 4);
                    dataBuffer[6] = VarContainer.split(line, 5);
                    dataBuffer[7] = VarContainer.split(line, 6);
                    dataBuffer[8] = VarContainer.split(line, 7);
                    dataBuffer[9] = VarContainer.split(line, 8);
                    dataBuffer[10] = VarContainer.split(line, 9);
                    dataBuffer[11] = VarContainer.split(line, 10);
                    dataBuffer[12] = VarContainer.split(line, 11);
                    dataBuffer[13] = VarContainer.split(line, 12);
                    dataBuffer[14] = VarContainer.split(line, 13);
                    dataBuffer[15] = VarContainer.split(line, 14);
                    dataBuffer[16] = VarContainer.split(line, 15);
                    dataBuffer[17] = VarContainer.split(line, 16);
                    dataBuffer[18] = VarContainer.split(line, 17);
                    dataBuffer[19] = VarContainer.split(line, 18);
                    dataBuffer[20] = VarContainer.split(line, 19);
                    dataBuffer[21] = VarContainer.split(line, 20);
                    dataBuffer[22] = VarContainer.split(line, 21);
                    dataBuffer[23] = VarContainer.split(line, 22);
                    dataBuffer[24] = VarContainer.split(line, 23);
                    dataBuffer[25] = VarContainer.split(line, 24);
                    dataBuffer[26] = VarContainer.split(line, 25);
                    dataBuffer[27] = VarContainer.split(line, 26);
                    dataBuffer[28] = VarContainer.split(line, 27);
                    dataBuffer[29] = VarContainer.split(line, 28);
                    dataBuffer[30] = VarContainer.split(line, 29);
                    dataBuffer[31] = VarContainer.split(line, 30);
                    dataBuffer[32] = DateTime.Now.ToShortDateString() + " " +DateTime.Now.ToLongTimeString();
                    //dataBuffer[32] = Math.Round(dummy, 2);
                    if (DataReceived != null)
                        DataReceived(this, new DataEventArgs(dataBuffer));
                }                
            }
        }

        private void OnTimedEvent(object state)
        {
            if (VarContainer.start)
                VarContainer.milisecs++;
        }
    }    

    public class DataEventArgs : EventArgs
    {
        public object[] Data { get; private set; }

        public DataEventArgs(object[] data)
        {
            Data = data;
        }
    }
}
