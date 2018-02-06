using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Runtime.InteropServices;

namespace TimeKeeper
{
    public class Utilities
    {
        private static RegistryKey baseRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
        private static string subKey = "SOFTWARE\\bethune-bryant\\Timekeeper";

        public static bool WriteToRegistry(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e.Message, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        public static string ReadFromRegistry(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return "";
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    string val = (string)sk1.GetValue(KeyName.ToUpper());

                    if (val == null) return "";
                    else return val;
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e.Message, "Reading registry " + KeyName.ToUpper());
                    return "";
                }
            }
        }

        public static void ShowErrorMessage(string Message, string Caption)
        {
            MessageBox.Show(Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        
        public static void ShowErrorMessage(string Message)
        {
            ShowErrorMessage(Message, "Error!");
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetWeek(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        
        public static string ElapsedTimeString(DateTime start, DateTime stop)
        {
            return new TimeSpan(0, 0, (int)Math.Round((stop - start).Duration().TotalMinutes), 0).ToString("hh\\:mm");
        }


        public static TimeSpan GetTime(IEnumerable<TimeEntry> entries)
        {
            TimeSpan retval = new TimeSpan(0);

            foreach (TimeEntry entry in entries)
            {
                retval += (entry.Stop == TimeEntry.MIN_DATE ? DateTime.Now : entry.Stop) - entry.Start;
            }

            return retval;
        }

        // this class just wraps some Win32 stuff
        internal class NativeMethods
        {
            public const int HWND_BROADCAST = 0xffff;
            public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
            [DllImport("user32")]
            public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
            [DllImport("user32")]
            public static extern int RegisterWindowMessage(string message);
        }
    }
}
