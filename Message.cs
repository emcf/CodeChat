using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatUI
{
    public partial class Message : Form
    {
        public Message(string Message, string Button)
        {
            InitializeComponent();
            lblMessage.Text = Message;
            btnContinue.Text = Button;
            btnContinue.FlatAppearance.BorderSize = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonCode_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
