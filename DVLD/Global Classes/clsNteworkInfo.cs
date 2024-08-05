using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal class clsNteworkInfo
    {

        //when you need to use it
        //  First define object from this class
        //  clsNteworkInfo _NteworkInfo;

        //then call in constructore of form
        //_NteworkInfo = new clsNteworkInfo(this);

        // _clsNtework = new clsNteworkInfo(this);

        //if (!_clsNtework.IsInternetAvailable())
        // {
        //MessageBox.Show("Internet connection has been lost. Please check your network connection.", "Disconnection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}
        //else
        //{
        //_LogIn();

        //}



private Timer internetCheckTimer;
        private Form waitForm;
        private Form parentForm;

        public clsNteworkInfo(Form parentForm)
        {
            this.parentForm = parentForm;
            InitializeInternetCheckTimer();
        }

        private void InitializeInternetCheckTimer()
        {
            internetCheckTimer = new Timer();
            internetCheckTimer.Interval = 5000; // تحقق كل 5 ثواني
            internetCheckTimer.Tick += new EventHandler(CheckInternetConnection);
            internetCheckTimer.Start();
        }

        private void CheckInternetConnection(object sender, EventArgs e)
        {
            if (!IsInternetAvailable())
            {
                if (waitForm == null || !waitForm.Visible)
                {
                    MessageBox.Show("Internet connection has been lost. Please check your network connection.", "Disconnection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowWaitForm();
                }
            }
            else
            {
                if (waitForm != null && waitForm.Visible)
                {
                    waitForm.Close();
                }
            }
        }

        public bool IsInternetAvailable()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ShowWaitForm()
        {
            waitForm = new frmWaitToConnectWifi();
            waitForm.ShowDialog(parentForm);
        }




    }
}
