using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Experiment7
{
    public static class VarContainer
    {
        // Variable Thread
        public static DBThread DBConnThread = null;
        public static SerialThread SerialConnThread = null;

        // Parameter to determine which form is showing
        public static int tab = 1;

        // Parameter to check whether the process is started
        public static bool start = false;

        // Parameter to determine which graphs to show
        public static string graph = "vrms";

        // Parameter to determine which graphs to show
        public static string magnitude = "voltage";

        // Parameter index for ID in Server
        public static int idx = 0;

        // Variable to check when to update data based on ID
        public static int mainID = 0;

        // Arrays to contain data
        public static float[] VmaxAdata = new float[10];
        public static float[] VmaxBdata = new float[10];
        public static float[] VmaxCdata = new float[10];

        public static float[] VrmsAdata = new float[10];
        public static float[] VrmsBdata = new float[10];
        public static float[] VrmsCdata = new float[10];

        public static float[] THDVAdata = new float[10];
        public static float[] THDVBdata = new float[10];
        public static float[] THDVCdata = new float[10];

        public static float[] ImaxAdata = new float[10];
        public static float[] ImaxBdata = new float[10];
        public static float[] ImaxCdata = new float[10];

        public static float[] IrmsAdata = new float[10];
        public static float[] IrmsBdata = new float[10];
        public static float[] IrmsCdata = new float[10];

        public static float[] THDIAdata = new float[10];
        public static float[] THDIBdata = new float[10];
        public static float[] THDICdata = new float[10];

        public static float[] SAdata = new float[10];
        public static float[] SBdata = new float[10];
        public static float[] SCdata = new float[10];

        public static float[] PAdata = new float[10];
        public static float[] PBdata = new float[10];
        public static float[] PCdata = new float[10];

        public static float[] QAdata = new float[10];
        public static float[] QBdata = new float[10];
        public static float[] QCdata = new float[10];

        public static float[] PFAdata = new float[10];
        public static float[] PFBdata = new float[10];
        public static float[] PFCdata = new float[10];

        public static int[] ID = new int[10];
        public static object[] real_timeStamp = new object[10];
        public static double[] timeStamp = new double[10];

        // Variable for time
        public static int milisecs = 0;

        // Variable to check/verification
        public static int dummyCounter = 0;

        // Variable to save data
        public static string savedData = null;

        // Variable to check whether the database connection opened
        public static bool DBConnOpened = false;

        public static string conn = "";

        // Function to split words from Serial Port
        public static object split(string line, int idx)
        {
            object[] words = new object[33];
            char[] delimiter = { ';' };

            words = line.Split(delimiter);

            return words[idx];
        }

        // Function to check the completeness data from Serial Port
        public static int check(string line)
        {
            int checkValue = 0;

            foreach (char delimiter in line){
                if (delimiter == ';')
                    checkValue++;
            }

            return checkValue;
        }
    }
}
