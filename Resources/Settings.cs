using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ChatUI
{
    public partial class Settings : Form
    {
        Color Syntax, Objects, Functions;
        // Used for dragging the form
        public static Point StartPoint;
        public static bool Dragging;

        public Settings()
        {
            InitializeComponent();
            tmrColourChanger.Start();

            // Set default colour scheme
            tbSyntaxR.Value = 0; tbSyntaxG.Value = 170; tbSyntaxB.Value = 170;
            tbObjectsR.Value = 255; tbObjectsG.Value = 60; tbObjectsB.Value = 200;
            tbFunctionsR.Value = 210; tbFunctionsG.Value = 182; tbFunctionsB.Value = 20;
        }

        private void tmrColourChanger_Tick(object sender, EventArgs e)
        {
            // A timer is the most responsive and clean solution to the colour changes.
            // Using events to track slider changes is simpler but inferior.

            Syntax = Color.FromArgb(tbSyntaxR.Value, tbSyntaxG.Value, tbSyntaxB.Value);
            tbSyntaxR.BackColor = Syntax;
            tbSyntaxG.BackColor = Syntax;
            tbSyntaxB.BackColor = Syntax;

            Objects = Color.FromArgb(tbObjectsR.Value, tbObjectsG.Value, tbObjectsB.Value);
            tbObjectsR.BackColor = Objects;
            tbObjectsG.BackColor = Objects;
            tbObjectsB.BackColor = Objects;

            Functions = Color.FromArgb(tbFunctionsR.Value, tbFunctionsG.Value, tbFunctionsB.Value);
            tbFunctionsR.BackColor = Functions;
            tbFunctionsG.BackColor = Functions;
            tbFunctionsB.BackColor = Functions;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] ColourScheme = { Syntax.ToArgb().ToString(), Objects.ToArgb().ToString(), Functions.ToArgb().ToString() };
            try
            {
                File.WriteAllLines("Colours.txt", ColourScheme);
            }
            catch
            {
                Message Msg = new Message("Unable to write colour scheme", "Error");
                Msg.Show();
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
           btnAdd.FlatAppearance.BorderSize = 0;
           btnSave.FlatAppearance.BorderSize = 0;

           try
           {
               foreach (string Contact in File.ReadAllLines("Contacts.txt"))
               {
                   lstContacts.Items.Add(Contact);
               }
           }
           catch
           {
               Message Msg = new Message("Unable to read Contacts.txt", "Continue");
               Msg.Show();
           }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText("Contacts.txt", File.ReadAllText("Contacts.txt") + Environment.NewLine + txtAddContact.Text + "					-" + txtAddContactIP.Text);
            }
            catch
            {
                Message Msg = new Message("Unable to write to Contacts.txt", "Continue");
                Msg.Show();
            }
        }

        private void Settings_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            StartPoint = new Point(e.X, e.Y);
        }

        private void Settings_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag = !Dragging;
            if (!flag)
            {
                Point p = base.PointToScreen(e.Location);
                base.Location = new Point(p.X - StartPoint.X, p.Y - StartPoint.Y);
            }
        }

        private void Settings_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

    }
}
