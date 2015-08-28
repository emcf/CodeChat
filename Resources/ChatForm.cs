using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

/*
This chat program is designed for sending text, code, and images through a small
encrypted Peer-to-peer network. This is the unfinished GUI for the client.
The code is fully commented and organized with region tags.
Programmed & designed by Emmett McFarlane
*/

namespace ChatUI
{
    public partial class ChatForm : Form
    {
        #region Public Variables
        private string Username, TexttoSend = "";
        private List<string> Messages = new List<string>();
        private int EncryptKey = 5;
        private int SpaceUsedinPanel = 15;
        // Used for dragging the form
        private Point StartPoint;
        private bool IsDragging;
        // Regex for syntax highlighter
        public Regex SyntaxKeywords = new Regex(@"\b(async|abstract|as|base|break|case|catch|checked|const|continue|default|delegate|do|else|enum|event|explicit|extern|false|finally|fixed|for|foreach|goto|if|implicit|in|interface|internal|is|lock|new|null|operator|out|override|params|private|protected|public|readonly|ref|return|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|ulong|unchecked|unsafe|ushort|using|virtual|volatile|while|public|new|lines|this)\b", RegexOptions.IgnoreCase),
        ObjectKeywords = new Regex(@"\b(image|size|point|bitmap|var|font|string|int|bool|byte|char|class|decimal|double|float|namespace|object|uint|this)\b|long|sbyte", RegexOptions.IgnoreCase),
        FunctionKeywords = new Regex(@"\b(.text|.items|.tostring|.location|.hide|.show|convert)\b", RegexOptions.IgnoreCase);
        // Networking variables
        public static Socket Sock;
        public static EndPoint Local;
        public static EndPoint Remote;
        public static byte[] Buffer;
        public static string RemoteIP;
        public static bool ConnectShown = false;
        // Syntax Highlighting Colours
        public static Color Syntax, Objects, Functions;
        #endregion

        #region Form's Round Corners
        // Rounded corners on buttons for Stamina's UI
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse,
            int nHeightEllipse
        );
        #endregion

        #region Form Load
        public ChatForm()
        {
            InitializeComponent();
            // Remove border from form and create a 2 pixel rounded indent around each corner.
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 2, 2));
            Username = GetManagementItem("SerialNumber", "Win32_BaseBoard") + GetManagementItem("ProcessorId", "Win32_Processor");

            Syntax = Color.FromArgb(0, 170, 170);
            Objects = Color.FromArgb(255, 60, 200);
            Functions = Color.FromArgb(210, 182, 20);

            try
            {
                string[] Colours = File.ReadAllLines("Colours.txt");
                Syntax = Color.FromArgb(Convert.ToInt32(Colours[0]));
                Objects = Color.FromArgb(Convert.ToInt32(Colours[1]));
                Functions = Color.FromArgb(Convert.ToInt32(Colours[2]));
            }
            catch
            {
                Message Msg = new Message("Unable to read colour scheme", "Error");
                Msg.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnClose.FlatAppearance.BorderSize = 0;
            btnCode.FlatAppearance.BorderSize = 0;
            btnSend.FlatAppearance.BorderSize = 0;
            btnConnect.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.BorderSize = 0;
            ChatPanel.Hide();
            this.Size = new Size(484, 87);
            btnConnect.Location = new Point(12, 36);
            btnSettings.Location = new Point(247, 36);
            // Add "Welcome to the chat room." encrypted
            Messages.Add(Encrypt("Welcome to the chat room.", EncryptKey));
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        #endregion

        #region ChatPanel Paint
        private void ChatPanel_Paint(object sender, PaintEventArgs e)
        {
            if (SpaceUsedinPanel >= 400)
            {
                Messages.Remove(Messages[0]);
                ChatPanel.Controls.Clear();
                ChatPanel.Invalidate();
            }

            RichTextBox rtbCode = new RichTextBox();
            Font fntText = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fntSyntaxHighlighter = new Font("Segoe UI", 10, FontStyle.Bold);
            SolidBrush brshText = new SolidBrush(Color.White);
            Color clrMessage = Color.FromArgb(10, 82, 129);
            int intWidth = 468;
            SpaceUsedinPanel = 15;

            for (int i = 0; i < Messages.Count; i++)
            {
                string txtMessage = Decrypt(Messages[i], EncryptKey);
                SizeF SizeofMessage = e.Graphics.MeasureString(txtMessage, fntText);
                int intHeight = Convert.ToInt32(SizeofMessage.Width) / intWidth;

                int intLineCount = 0;
                string[] LineCount = txtMessage.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string Line in LineCount)
                {
                    intLineCount++;
                }

                Rectangle rctMessage = new Rectangle(15, SpaceUsedinPanel, intWidth, (intHeight * Convert.ToInt32(SizeofMessage.Height) + 30) + ((intLineCount - 1) * 17));
                Rectangle rctBox = new Rectangle(15 - 5, SpaceUsedinPanel - 5, intWidth - 5, (intHeight * Convert.ToInt32(SizeofMessage.Height)) + 30 + ((intLineCount - 1) * 17));

                UIComponents.DrawRoundedRectangle(e.Graphics, clrMessage, rctBox, 1, UIComponents.RoundedCorners.All);

                string Code = txtMessage.Replace("[Code]", "").Replace("[/Code]", "");

                rtbCode.Click += rtbCode_Click;
                rtbCode.TabIndex = 3;
                rtbCode.BackColor = clrMessage;
                rtbCode.ForeColor = Color.White;
                rtbCode.ScrollBars = RichTextBoxScrollBars.None;
                rtbCode.SelectionColor = Color.FromArgb(50, 64, 100);
                rtbCode.Font = fntText;
                rtbCode.BorderStyle = BorderStyle.None;
                rtbCode.Location = rctMessage.Location;
                rtbCode.Size = new Size(rctMessage.Size.Width - 15, rctMessage.Size.Height - 10);
                rtbCode.ReadOnly = true;
                rtbCode.Text = Code;
                ChatPanel.Controls.Add(rtbCode);

                rtbCode.SelectAll();
                rtbCode.SelectionColor = Color.FromArgb(255, 255, 255);
                rtbCode.SelectionFont = new Font("Segoe UI", 10, FontStyle.Regular);

                SpaceUsedinPanel += rctBox.Height + 7;

                if (txtMessage.Contains("[Code]") && txtMessage.Contains("[/Code]"))
                {
                    #region Regex Syntax Highlighter
                    // Select text from the begining
                    int selPos = rtbCode.SelectionStart;

                    // Syntax in blue
                    foreach (Match keyWordMatch in SyntaxKeywords.Matches(rtbCode.Text))
                    {
                        rtbCode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        // Change colours
                        rtbCode.SelectionColor = Syntax;
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }

                    // Objects in pink
                    foreach (Match keyWordMatch in ObjectKeywords.Matches(rtbCode.Text))
                    {
                        rtbCode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        // Change colours
                        rtbCode.SelectionColor = Objects;
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }

                    // Functions in yellow
                    foreach (Match keyWordMatch in FunctionKeywords.Matches(rtbCode.Text))
                    {
                        rtbCode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        // Change colours
                        rtbCode.SelectionColor = Functions;
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Clickable Links (Being reworked due to a list change)
        private void rtbCode_Click(object sender, EventArgs e)
        {
            /*if (IsUrl())
            {
                Process.Start(Decrypt(lstMessages.SelectedItem.ToString(), EncryptKey).Split(' ')[0]);
            }*/
        }
        #endregion

        #region Movable Form
        private void ChatForm_MouseDown(object sender, MouseEventArgs e)
        {
            IsDragging = true;
            StartPoint = new Point(e.X, e.Y);
        }

        private void ChatForm_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag = !IsDragging;
            if (!flag)
            {
                Point p = base.PointToScreen(e.Location);
                base.Location = new Point(p.X - StartPoint.X, p.Y - StartPoint.Y);
            }
        }

        private void ChatForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }
        #endregion

        #region Detect URLs
        private bool IsUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == "http" || uri.Scheme == "https")
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        #endregion

        #region Hardware Properties
        private string GetManagementItem(string ItemManagementProperty, string ItemManagementClass)
        {
            string Result = "";
            ManagementClass Class = new System.Management.ManagementClass(ItemManagementClass);
            ManagementObjectCollection ObjectCollection = Class.GetInstances();

            try
            {
                foreach (System.Management.ManagementObject Object in ObjectCollection)
                {
                    Result = Object[ItemManagementProperty].ToString();
                }
            }
            catch
            {
                MessageBox.Show("Error retreiving " + ItemManagementClass + ", " + ItemManagementProperty);
            }

            return Result;
        }
        #endregion

        #region Close
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Rest in Peace
            Application.Exit();
        }
        #endregion

        #region Networking
        public static string GetIP()
        {
            IPHostEntry Host;
            Host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IP.ToString();
                }
            }
            return "Error";
        }

        delegate void PanelRefresh();

        private void RefreshChatPanel()
        {
            // Allows panel refreshing from the networking thread
            if (this.ChatPanel.InvokeRequired)
            {
                this.Invoke(new PanelRefresh(RefreshChatPanel));
            }
            else
            {
                // Repaint the ChatPanel
                this.Refresh();
            }
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            byte[] ReceivedData = new byte[1500];
            ReceivedData = (byte[])aResult.AsyncState;
            ASCIIEncoding AsciiEncoding = new ASCIIEncoding();
            string receivedMessage = AsciiEncoding.GetString(ReceivedData);
            Messages.Add(receivedMessage);
            // Reset buffer
            Buffer = new byte[1500];
            Sock.BeginReceiveFrom(Buffer, 0, Buffer.Length, SocketFlags.None, ref Remote, new AsyncCallback(MessageCallBack), Buffer);
            // Reload all messages
            RefreshChatPanel();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            Connect Cnct = new Connect();
            Cnct.Show();
            // Get local IP address
            string localIP = GetIP();
            // Socket Set up
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            // Bind Socket
            Local = new IPEndPoint(IPAddress.Parse(localIP), 80);
            Sock.Bind(Local);

            // Wait until Connect.cs is closed
            while (ConnectShown)
            {
                await Task.Delay(100);
            }

            try
            {
                // Connect
                Remote = new IPEndPoint(IPAddress.Parse(RemoteIP), 80);
                Sock.Connect(Remote);

                // Listening to Port
                Buffer = new byte[1500];
                Sock.BeginReceiveFrom(Buffer, 0, Buffer.Length, SocketFlags.None, ref Remote, new AsyncCallback(MessageCallBack), Buffer);
                
                // Change the UI
                this.Size = new Size(484, 616);
                btnConnect.Hide();
                btnSettings.Hide();
                ChatPanel.Show();
                lblTitle.Text = "Chatting in " + RemoteIP;
            }
            catch
            {
                Message Msg = new Message("Unable to Connect", "Continue");
                Msg.Show();
            }
        }
        #endregion

        #region Send Button
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (!IsNullOrWhiteSpace(txtChat.Text))
            {
                Send(Encrypt(txtChat.Text, EncryptKey));
            }
        }
        #endregion

        #region Send Function
        private void Send(string Text)
        {
            UTF8Encoding uEncoding = new UTF8Encoding();
            // ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            var Bytes = uEncoding.GetBytes(Text);

            // If you aren't connected to yourself, write the message on your own client
            if (!(RemoteIP == GetIP()))
            {
                Messages.Add(Text);
            }

            try
            {
                Sock.Send(Bytes);
                txtChat.Text = "";
                // Repaint the ChatPanel
                this.Refresh();
            }
            catch
            {
                Message m = new Message("You must connect before sending\na message.", "Continue");
                m.Show();
                return;
            }
        }
        #endregion

        #region Code Button
        private void btnCode_Click(object sender, EventArgs e)
        {
            Send(Encrypt("[Code]" + txtChat.Text + "[/Code]", EncryptKey));
        }
        #endregion

        #region Xor Encryption
        private string Encrypt(string Input, int Key)
        {
            string Output = "";
            // XOR each character value by the Key
            foreach (char c in Input)
            {
                Output += (Convert.ToInt32(c) ^ Key) + " ";
            }
            return Output;
        }

        private string Decrypt(string Input, int Key)
        {
            string[] Characters = Input.Split(' ');
            string Output = "";
            // XOR each encrypted character value by the Key
            for (int i = 0; i < Characters.Length - 1; i++)
            {
                Characters[i] = Characters[i].Replace(" ", "");
                Output += Convert.ToChar(Convert.ToInt32(Characters[i]) ^ Key);
            }

            return Output;
        }
        #endregion

        #region Settings Function
        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings Stngs = new Settings();
            Stngs.Show();
        }
        #endregion
    }
}
