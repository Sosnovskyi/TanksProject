using System;
using System.Windows.Forms;

namespace TanksGame.DesktopUI
{
    static class Program
    {
        public static bool Play { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMenu());
            if (Play)
            {
                Application.Run(new frmDesktopGame());
            }
        }
    }
}
