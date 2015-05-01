using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ChatUI
{
    public partial class Colours : Form
    {
        Color Syntax, Objects, Functions;

        public Colours()
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
            File.WriteAllLines("Colours.txt", ColourScheme);

            // TODO: When ChatForm opens, load colour scheme, apply it.
        }

    }
}
