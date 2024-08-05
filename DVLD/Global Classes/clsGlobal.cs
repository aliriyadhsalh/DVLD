using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDUserNameAndPassWord";

            try
            {
                Registry.SetValue(keyPath, "DVLDUserNameAndPassWord", Username + "#//#" +Password, RegistryValueKind.String);
                return true; 
            }
            catch (Exception ex)
            {
               MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDUserNameAndPassWord";

            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string value = Registry.GetValue(keyPath, "DVLDUserNameAndPassWord", null) as string;

                string [] arr;
                if (value != null)
                {
                    arr = value.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    Username = arr[0];
                    Password = arr[1];

                    return true;
                }
                else
                {
                    return false;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }



    }
}
