
namespace ChatUI
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.ButtonCode = new System.Windows.Forms.Button();
            this.ButtonSend = new System.Windows.Forms.Button();
            this.ChatPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ChatTextbox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonCode
            // 
            this.ButtonCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.ButtonCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCode.ForeColor = System.Drawing.Color.White;
            this.ButtonCode.Location = new System.Drawing.Point(12, 523);
            this.ButtonCode.Name = "ButtonCode";
            this.ButtonCode.Size = new System.Drawing.Size(460, 38);
            this.ButtonCode.TabIndex = 0;
            this.ButtonCode.Text = "Code";
            this.ButtonCode.UseVisualStyleBackColor = false;
            this.ButtonCode.Click += new System.EventHandler(this.ButtonCode_Click);
            // 
            // ButtonSend
            // 
            this.ButtonSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(75)))), ((int)(((byte)(100)))));
            this.ButtonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSend.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSend.ForeColor = System.Drawing.Color.White;
            this.ButtonSend.Location = new System.Drawing.Point(12, 479);
            this.ButtonSend.Name = "ButtonSend";
            this.ButtonSend.Size = new System.Drawing.Size(460, 38);
            this.ButtonSend.TabIndex = 1;
            this.ButtonSend.Text = "Send";
            this.ButtonSend.UseVisualStyleBackColor = false;
            this.ButtonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // ChatPanel
            // 
            this.ChatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.ChatPanel.Location = new System.Drawing.Point(12, 34);
            this.ChatPanel.Name = "ChatPanel";
            this.ChatPanel.Size = new System.Drawing.Size(460, 408);
            this.ChatPanel.TabIndex = 2;
            this.ChatPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ChatPanel_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chat Room 192.168.0.1";
            // 
            // ChatTextbox
            // 
            this.ChatTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.ChatTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatTextbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatTextbox.ForeColor = System.Drawing.Color.White;
            this.ChatTextbox.Location = new System.Drawing.Point(12, 448);
            this.ChatTextbox.Name = "ChatTextbox";
            this.ChatTextbox.Size = new System.Drawing.Size(460, 22);
            this.ChatTextbox.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(493, 185);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(234, 173);
            this.listBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(60)))));
            this.button1.Location = new System.Drawing.Point(444, -3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 19);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChatForm
            // 
            this.AcceptButton = this.ButtonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(64)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(484, 575);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ChatTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChatPanel);
            this.Controls.Add(this.ButtonSend);
            this.Controls.Add(this.ButtonCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Text = "ChatUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChatForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChatForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChatForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCode;
        private System.Windows.Forms.Button ButtonSend;
        private System.Windows.Forms.Panel ChatPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ChatTextbox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
    }
}

