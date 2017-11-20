using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Net;
using Experiment7.Properties;

namespace Experiment7
{
    public class DBThread
    {
        private MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;
        private string table;
        
        System.Threading.Timer timer;

        private Thread t;

        public event EventHandler<MyDataEventArgs> DataReceived;
        public event EventHandler ConnectionOff;
        public event ElapsedEventHandler OnTime;

        public DBThread()
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

        private bool closed = false;
        private readonly object e;

        public void Close()
        {
            closed = true;
            VarContainer.start = false;
        }

        public void Open()
        {
            closed = false;
            //dataContainer.start = true;
        }

        private void RunMethod()
        {
            timer = new System.Threading.Timer(OnTimedEvent, null, 0, 100);

            if (Settings.Default.Database == "server")
            {
                server = "167.205.104.225";
                database = "u6239901_data";
                uid = "u6239901_arduino";
                password = "arduino";
                table = "hasildata";
            }
            else if (Settings.Default.Database == "localhost")
            {
                server = "localhost";
                database = "datapengukuran";
                uid = "root";
                password = "";
                table = "hasildata";
            }

            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            while (!closed)
            {
                if (VarContainer.start)
                {
                    if (this.OpenConnection("Show"))
                    {
                        //string query = "SELECT * FROM " + table + " ORDER BY ID DESC LIMIT 1";
                        string query = "SELECT * FROM " + table + " WHERE ID = " + VarContainer.idx;

                        //Create Command
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Create a data reader and Execute the command
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
                            object[] dataBuffer = new object[34];
                            dataBuffer[0] = dataReader["ID"];

                            double dummy = VarContainer.milisecs * 0.1;
                            dataBuffer[1] = Math.Round(dummy, 2);

                            //DateTime foo = (DateTime)dataReader["timeStamp"];
                            //dataBuffer[1] = foo.Ticks;
                    
                            dataBuffer[2] = dataReader["VrmsA"];
                            dataBuffer[3] = dataReader["VrmsB"];
                            dataBuffer[4] = dataReader["VrmsC"];

                            dataBuffer[5] = dataReader["VmaxA"];
                            dataBuffer[6] = dataReader["VmaxB"];
                            dataBuffer[7] = dataReader["VmaxC"];

                            dataBuffer[8] = dataReader["THDVA"];
                            dataBuffer[9] = dataReader["THDVB"];
                            dataBuffer[10] = dataReader["THDVC"];

                            dataBuffer[11] = dataReader["IrmsA"];
                            dataBuffer[12] = dataReader["IrmsB"];
                            dataBuffer[13] = dataReader["IrmsC"];

                            dataBuffer[14] = dataReader["ImaxA"];
                            dataBuffer[15] = dataReader["ImaxB"];
                            dataBuffer[16] = dataReader["ImaxC"];

                            dataBuffer[17] = dataReader["THDIA"];
                            dataBuffer[18] = dataReader["THDIB"];
                            dataBuffer[19] = dataReader["THDIC"];

                            dataBuffer[20] = dataReader["SA"];
                            dataBuffer[21] = dataReader["SB"];
                            dataBuffer[22] = dataReader["SC"];

                            dataBuffer[23] = dataReader["PA"];
                            dataBuffer[24] = dataReader["PB"];
                            dataBuffer[25] = dataReader["PC"];

                            dataBuffer[26] = dataReader["QA"];
                            dataBuffer[27] = dataReader["QB"];
                            dataBuffer[28] = dataReader["QC"];

                            dataBuffer[29] = dataReader["PFA"];
                            dataBuffer[30] = dataReader["PFB"];
                            dataBuffer[31] = dataReader["PFC"];

                            dataBuffer[32] = dataReader["timeStamp"];

                            if (DataReceived != null)
                                DataReceived(this, new MyDataEventArgs(dataBuffer));
                        }

                        // Close Data Reader
                        dataReader.Close();

                        this.CloseConnection();

                        if (VarContainer.milisecs >= VarContainer.idx * 10)
                            VarContainer.idx++;
                    }                    
                }
            }
        }

        public event EventHandler<ConnDataEventArgs> DBConnCheck;
        private bool DBConn = false;

        public bool OpenConnection(string foo)
        {
            try
            {
                connection.Open();

                DBConn = true;
                if (DBConnCheck != null)
                    DBConnCheck(this, new ConnDataEventArgs(DBConn));

                return true;
            }
            catch (MySqlException ex)
            {
                if (foo == "Show")
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Cannot connect to server. Contact administrator", "Error");
                            break;

                        case 1045:
                            MessageBox.Show("Invalid username/password, please try again", "Error");
                            break;

                        default:
                            MessageBox.Show(ex.Message, "Error");
                            break;
                    }
                }

                DBConn = false;
                if (DBConnCheck != null)
                    DBConnCheck(this, new ConnDataEventArgs(DBConn));

                return false;
            }            
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        
        private void OnTimedEvent(object state)
        {
            if (VarContainer.start)
            {
                VarContainer.milisecs++;
            }
        }

        //public void IsConnected()
        //{
        //    if (connection != null && connection.State == System.Data.ConnectionState.Open)
        //        dataContainer.connected = true;
        //    else
        //        dataContainer.connected = false;
        //}
    }

    public class MyDataEventArgs : EventArgs
    {
        public object[] Data { get; private set; }

        public MyDataEventArgs(object[] data)
        {
            Data = data;
        }
    }

    public class ConnDataEventArgs : EventArgs
    {
        public readonly bool connected;

        public ConnDataEventArgs(bool _connected)
        {
            connected = _connected;
        }
    }
}
