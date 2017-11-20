using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Experiment7.Properties;

namespace Experiment7
{
    static class Program
    {
        public static Monitoring f1;
        public static Setting f2;
        public static About f3;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            f1 = new Monitoring();
            f2 = new Setting();
            f3 = new About();

            f1.VisibleChanged += OnForm1Changed;
            f2.VisibleChanged += OnForm2Changed;
            f3.VisibleChanged += OnForm3Changed;

            f2.OnClick += ChangeMethod;

            Application.Run(f1);
        }

        private static void ChangeMethod()
        {
            f1.labelGraph.Image = Experiment7.Properties.Resources.GraphLabel_IMAX;
        }

        // Form location is determined by Properties -> Application Bindings -> Location
        private static void OnForm3Changed(object sender, EventArgs e)
        {
            if (!f3.Visible)
            {
                if (VarContainer.tab == 1)
                {
                    f2.Hide();
                    f1.Show();
                }
                else
                {
                    f1.Hide();
                    f2.Show();
                }
            }
        }

        private static void OnForm2Changed(object sender, EventArgs e)
        {
            if (!f2.Visible)
            {
                if (VarContainer.tab == 1)
                {
                    f3.Hide();
                    f1.Show();
                }
                else
                {
                    f1.Hide();
                    f3.Show();
                }
            }
        }

        private static void OnForm1Changed(object sender, EventArgs e)
        {
            if (!f1.Visible)
            {
                if (VarContainer.tab == 2)
                {
                    f3.Hide();
                    f2.Show();
                }
                else
                {
                    f1.Hide();
                    f3.Show();
                }
            }
        }
    }
}
