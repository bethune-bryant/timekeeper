using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDesk.Options;

namespace TimeKeeper
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{5231FC65-183F-44B3-9227-E3D54EE12474}");

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

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {

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

                mutex.ReleaseMutex();
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                Utilities.NativeMethods.PostMessage(
                    (IntPtr)Utilities.NativeMethods.HWND_BROADCAST,
                    Utilities.NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }
    }
}
