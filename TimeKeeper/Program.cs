using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDesk.Options;

namespace TimeKeeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool start_minimized = false;

            var p = new OptionSet() {
    { "m|minimized",  "Start minimized.",
       v => start_minimized = v != null },
};

            List<string> extra;
            try
            {
                extra = p.Parse(args);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain(start_minimized));
            }
            catch (OptionException e)
            {
                Console.Write("Timekeeper: ");
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
