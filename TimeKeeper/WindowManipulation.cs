using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using System.Net;
using System.Text;

namespace TimeKeeper
{
    class WindowManipulation
    {

        [DllImportAttribute("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        private const int WS_SHOWNORMAL = 1;

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);


        public static bool BringForwardWindow(string Title)
        {
            //System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName(Title);
            //if (p.Length > 0)
            //{
            //    //SetForegroundWindow(p[0].MainWindowHandle);
            //    SystemParametersInfo((uint)0x2001, 0, 0, 0x0002 | 0x0001);
            //    ShowWindowAsync(p[0].MainWindowHandle, WS_SHOWNORMAL);
            //    SetForegroundWindow(p[0].MainWindowHandle);
            //    SystemParametersInfo((uint)0x2001, 200000, 200000, 0x0002 | 0x0001);
            //    return true;
            //}
            //else
            //{
            //    ShowError("Dispatch Not Found!", "Be sure that dispatch is running and then try running your macro again.");
            //    return false;
            //}

            try
            {
                IntPtr window = FindWindowByCaption(IntPtr.Zero, Title);
                if (window == IntPtr.Zero)
                {
                    throw new ArgumentException("Could not find window \"" + Title + "\"");
                }
                SetForegroundWindow(window);
                return true;
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Error:" + Environment.NewLine + exc.Message);
                return false;
            }
        }
    }
}
