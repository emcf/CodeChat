using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace ChatUI
{
    public partial class Connect : Form
    {
        // Used for dragging the form
        public static Point start_point;
        public static bool dragging;

        public Connect()
        {
            InitializeComponent();
            ChatForm.ConnectShown = true;
            btnConnect.FlatAppearance.BorderSize = 0;

            // Add contacts
            lstContacts.Items.Add("Local IP					-" + ChatForm.GetIP());

            try
            {
                foreach (string Contact in File.ReadAllLines("Contacts.txt"))
                {
                    lstContacts.Items.Add(Contact);
                }
            }
            catch
            {
                Message msg = new Message("Unable to read Contacts.txt", "Error");
            }
        }

        private void ButtonCode_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected IP
                string IP = lstContacts.GetItemText(lstContacts.SelectedItem).Split('-')[1];
                // Connect
                ChatForm.Remote = new IPEndPoint(IPAddress.Parse(IP), 3333);
                ChatForm.Sock.Connect(ChatForm.Remote);
                ChatForm.RemoteIP = IP;
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

        private void Connect_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            start_point = new Point(e.X, e.Y);
        }

        private void Connect_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag = !dragging;
            if (!flag)
            {
                Point p = base.PointToScreen(e.Location);
                base.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void Connect_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
