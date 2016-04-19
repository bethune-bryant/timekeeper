using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace TimeKeeper
{
    class Utilities
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
    }
}
