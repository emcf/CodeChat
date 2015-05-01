namespace ChatUI
{
    partial class Colours
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSyntaxR = new System.Windows.Forms.TrackBar();
            this.ChatPanel = new System.Windows.Forms.Panel();
            this.tbSyntaxG = new System.Windows.Forms.TrackBar();
            this.tbSyntaxB = new System.Windows.Forms.TrackBar();
            this.tbObjectsB = new System.Windows.Forms.TrackBar();
            this.tbObjectsG = new System.Windows.Forms.TrackBar();
            this.tbObjectsR = new System.Windows.Forms.TrackBar();
            this.tbFunctionsB = new System.Windows.Forms.TrackBar();
            this.tbFunctionsG = new System.Windows.Forms.TrackBar();
            this.tbFunctionsR = new System.Windows.Forms.TrackBar();
            this.tmrColourChanger = new System.Windows.Forms.Timer(this.components);
            this.UICoverPanel = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.trackBar7 = new System.Windows.Forms.TrackBar();
            this.trackBar8 = new System.Windows.Forms.TrackBar();
            this.trackBar9 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxR)).BeginInit();
            this.ChatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsR)).BeginInit();
            this.UICoverPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar9)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(149)))), ((int)(((byte)(87)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(12, 375);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(294, 38);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.btnClose.Location = new System.Drawing.Point(278, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 19);
            this.btnClose.TabIndex = 13;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Colours";
            // 
            // tbSyntaxR
            // 
            this.tbSyntaxR.Location = new System.Drawing.Point(101, 19);
            this.tbSyntaxR.Maximum = 255;
            this.tbSyntaxR.Name = "tbSyntaxR";
            this.tbSyntaxR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSyntaxR.Size = new System.Drawing.Size(45, 91);
            this.tbSyntaxR.TabIndex = 14;
            this.tbSyntaxR.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ChatPanel
            // 
            this.ChatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(53)))));
            this.ChatPanel.Controls.Add(this.UICoverPanel);
            this.ChatPanel.Controls.Add(this.tbFunctionsB);
            this.ChatPanel.Controls.Add(this.tbFunctionsG);
            this.ChatPanel.Controls.Add(this.tbFunctionsR);
            this.ChatPanel.Controls.Add(this.tbObjectsB);
            this.ChatPanel.Controls.Add(this.tbObjectsG);
            this.ChatPanel.Controls.Add(this.tbObjectsR);
            this.ChatPanel.Controls.Add(this.tbSyntaxB);
            this.ChatPanel.Controls.Add(this.tbSyntaxG);
            this.ChatPanel.Controls.Add(this.tbSyntaxR);
            this.ChatPanel.Location = new System.Drawing.Point(-3, 33);
            this.ChatPanel.Name = "ChatPanel";
            this.ChatPanel.Size = new System.Drawing.Size(332, 331);
            this.ChatPanel.TabIndex = 15;
            // 
            // tbSyntaxG
            // 
            this.tbSyntaxG.Location = new System.Drawing.Point(142, 19);
            this.tbSyntaxG.Maximum = 255;
            this.tbSyntaxG.Name = "tbSyntaxG";
            this.tbSyntaxG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSyntaxG.Size = new System.Drawing.Size(45, 91);
            this.tbSyntaxG.TabIndex = 15;
            this.tbSyntaxG.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbSyntaxB
            // 
            this.tbSyntaxB.Location = new System.Drawing.Point(184, 19);
            this.tbSyntaxB.Maximum = 255;
            this.tbSyntaxB.Name = "tbSyntaxB";
            this.tbSyntaxB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbSyntaxB.Size = new System.Drawing.Size(45, 91);
            this.tbSyntaxB.TabIndex = 16;
            this.tbSyntaxB.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbObjectsB
            // 
            this.tbObjectsB.Location = new System.Drawing.Point(184, 116);
            this.tbObjectsB.Maximum = 255;
            this.tbObjectsB.Name = "tbObjectsB";
            this.tbObjectsB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbObjectsB.Size = new System.Drawing.Size(45, 91);
            this.tbObjectsB.TabIndex = 19;
            this.tbObjectsB.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbObjectsG
            // 
            this.tbObjectsG.Location = new System.Drawing.Point(142, 116);
            this.tbObjectsG.Maximum = 255;
            this.tbObjectsG.Name = "tbObjectsG";
            this.tbObjectsG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbObjectsG.Size = new System.Drawing.Size(45, 91);
            this.tbObjectsG.TabIndex = 18;
            this.tbObjectsG.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbObjectsR
            // 
            this.tbObjectsR.Location = new System.Drawing.Point(101, 116);
            this.tbObjectsR.Maximum = 255;
            this.tbObjectsR.Name = "tbObjectsR";
            this.tbObjectsR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbObjectsR.Size = new System.Drawing.Size(45, 91);
            this.tbObjectsR.TabIndex = 17;
            this.tbObjectsR.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbFunctionsB
            // 
            this.tbFunctionsB.Location = new System.Drawing.Point(184, 213);
            this.tbFunctionsB.Maximum = 255;
            this.tbFunctionsB.Name = "tbFunctionsB";
            this.tbFunctionsB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbFunctionsB.Size = new System.Drawing.Size(45, 91);
            this.tbFunctionsB.TabIndex = 22;
            this.tbFunctionsB.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbFunctionsG
            // 
            this.tbFunctionsG.Location = new System.Drawing.Point(142, 213);
            this.tbFunctionsG.Maximum = 255;
            this.tbFunctionsG.Name = "tbFunctionsG";
            this.tbFunctionsG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbFunctionsG.Size = new System.Drawing.Size(45, 91);
            this.tbFunctionsG.TabIndex = 21;
            this.tbFunctionsG.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tbFunctionsR
            // 
            this.tbFunctionsR.Location = new System.Drawing.Point(101, 213);
            this.tbFunctionsR.Maximum = 255;
            this.tbFunctionsR.Name = "tbFunctionsR";
            this.tbFunctionsR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbFunctionsR.Size = new System.Drawing.Size(45, 91);
            this.tbFunctionsR.TabIndex = 20;
            this.tbFunctionsR.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tmrColourChanger
            // 
            this.tmrColourChanger.Interval = 10;
            this.tmrColourChanger.Tick += new System.EventHandler(this.tmrColourChanger_Tick);
            // 
            // UICoverPanel
            // 
            this.UICoverPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(53)))));
            this.UICoverPanel.Controls.Add(this.trackBar1);
            this.UICoverPanel.Controls.Add(this.trackBar2);
            this.UICoverPanel.Controls.Add(this.trackBar3);
            this.UICoverPanel.Controls.Add(this.trackBar4);
            this.UICoverPanel.Controls.Add(this.trackBar5);
            this.UICoverPanel.Controls.Add(this.trackBar6);
            this.UICoverPanel.Controls.Add(this.trackBar7);
            this.UICoverPanel.Controls.Add(this.trackBar8);
            this.UICoverPanel.Controls.Add(this.trackBar9);
            this.UICoverPanel.Location = new System.Drawing.Point(212, 19);
            this.UICoverPanel.Name = "UICoverPanel";
            this.UICoverPanel.Size = new System.Drawing.Size(33, 296);
            this.UICoverPanel.TabIndex = 23;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(184, 213);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 91);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(142, 213);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 91);
            this.trackBar2.TabIndex = 21;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(101, 213);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 91);
            this.trackBar3.TabIndex = 20;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar4
            // 
            this.trackBar4.Location = new System.Drawing.Point(184, 116);
            this.trackBar4.Maximum = 255;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar4.Size = new System.Drawing.Size(45, 91);
            this.trackBar4.TabIndex = 19;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(142, 116);
            this.trackBar5.Maximum = 255;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar5.Size = new System.Drawing.Size(45, 91);
            this.trackBar5.TabIndex = 18;
            this.trackBar5.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar6
            // 
            this.trackBar6.Location = new System.Drawing.Point(101, 116);
            this.trackBar6.Maximum = 255;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar6.Size = new System.Drawing.Size(45, 91);
            this.trackBar6.TabIndex = 17;
            this.trackBar6.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar7
            // 
            this.trackBar7.Location = new System.Drawing.Point(184, 19);
            this.trackBar7.Maximum = 255;
            this.trackBar7.Name = "trackBar7";
            this.trackBar7.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar7.Size = new System.Drawing.Size(45, 91);
            this.trackBar7.TabIndex = 16;
            this.trackBar7.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar8
            // 
            this.trackBar8.Location = new System.Drawing.Point(142, 19);
            this.trackBar8.Maximum = 255;
            this.trackBar8.Name = "trackBar8";
            this.trackBar8.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar8.Size = new System.Drawing.Size(45, 91);
            this.trackBar8.TabIndex = 15;
            this.trackBar8.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // trackBar9
            // 
            this.trackBar9.Location = new System.Drawing.Point(101, 19);
            this.trackBar9.Maximum = 255;
            this.trackBar9.Name = "trackBar9";
            this.trackBar9.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar9.Size = new System.Drawing.Size(45, 91);
            this.trackBar9.TabIndex = 14;
            this.trackBar9.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // Colours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(68)))), ((int)(((byte)(89)))));
            this.ClientSize = new System.Drawing.Size(318, 425);
            this.Controls.Add(this.ChatPanel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Colours";
            this.Text = "Colours";
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxR)).EndInit();
            this.ChatPanel.ResumeLayout(false);
            this.ChatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSyntaxB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObjectsR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFunctionsR)).EndInit();
            this.UICoverPanel.ResumeLayout(false);
            this.UICoverPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbSyntaxR;
        private System.Windows.Forms.Panel ChatPanel;
        private System.Windows.Forms.TrackBar tbFunctionsB;
        private System.Windows.Forms.TrackBar tbFunctionsG;
        private System.Windows.Forms.TrackBar tbFunctionsR;
        private System.Windows.Forms.TrackBar tbObjectsB;
        private System.Windows.Forms.TrackBar tbObjectsG;
        private System.Windows.Forms.TrackBar tbObjectsR;
        private System.Windows.Forms.TrackBar tbSyntaxB;
        private System.Windows.Forms.TrackBar tbSyntaxG;
        private System.Windows.Forms.Timer tmrColourChanger;
        private System.Windows.Forms.Panel UICoverPanel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.TrackBar trackBar7;
        private System.Windows.Forms.TrackBar trackBar8;
        private System.Windows.Forms.TrackBar trackBar9;
    }
}