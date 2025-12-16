using System;
using System.Windows.Forms;
using technology2;

namespace HealthCenterSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Login form starts first
            Application.Run(new Form1());
        }
    }
}
