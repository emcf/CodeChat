using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ChatUI
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
            ChatForm.ConnectShown = true;
            btnConnect.FlatAppearance.BorderSize = 0;
            txtAddress.Text = ChatForm.GetIP();
        }

        private void ButtonCode_Click(object sender, EventArgs e)
        {
            try
            {
                ChatForm.Remote = new IPEndPoint(IPAddress.Parse(txtAddress.Text), 3333);
                ChatForm.Sock.Connect(ChatForm.Remote);
                ChatForm.RemoteIP = txtAddress.Text;
                ChatForm.ConnectShown = true;
                // Connect
                this.Close();
            }
            catch
            {
                ChatForm.ConnectShown = false;
                // Unable to connect to that IP
                ChatForm.RemoteIP = "Error";
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Connect_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChatForm.ConnectShown = false;
        }
    }
}
