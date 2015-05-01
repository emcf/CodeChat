using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

/*
This chat program is designed for sending text, code, and images through a small
encrypted Peer-to-peer network. This is the unfinished GUI for the client.
The code is fully commented and organized with region tags.
Coded & designed by Emmett McFarlane
*/

namespace ChatUI
{
    public partial class ChatForm : Form
    {
        #region Public Variables
        // Basic variables
        public static string Username;
        public static string TexttoSend = "";
        private static int EncryptKey = 5;
        public static bool InCodeMode = false;
        // Helps detect if the oldest message needs to be deleted if the screen is too cluttered
        public static int SpaceUsedinPanel = 15;
        // Used for drawing the rectangle behind messages
        public static Rectangle TextRect;
        // Used for dragging the form
        public static Point start_point;
        public static bool dragging;
        // Used for checking if the console has already welcomed user
        public static bool ConsoleSpoken = false;
        // Regex for syntax highlighter
        public static Regex SyntaxKeywords =new Regex(@"\b(async|abstract|as|base|break|case|catch|checked|const|continue|default|delegate|do|else|enum|event|explicit|extern|false|finally|fixed|for|foreach|goto|if|implicit|in|interface|internal|is|lock|new|null|operator|out|override|params|private|protected|public|readonly|ref|return|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|ulong|unchecked|unsafe|ushort|using|virtual|volatile|while|public|new|lines|this)\b", RegexOptions.IgnoreCase);
        public static Regex ObjectKeywords = new Regex(@"\b(image|size|point|bitmap|var|font|string|int|bool|byte|char|class|decimal|double|float|namespace|object|uint|this)\b|long|sbyte", RegexOptions.IgnoreCase);
        public static Regex FunctionKeywords = new Regex(@"\b(.text|.items|.tostring|.location|.hide|.show|convert)\b", RegexOptions.IgnoreCase);
        // Variables for networking
        public static Socket Sock;
        public static EndPoint Local;
        public static EndPoint Remote;
        public static byte[] Buffer;
        public static string RemoteIP;
        public static bool ConnectShown = false;
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

            (new Colours()).Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnClose.FlatAppearance.BorderSize = 0;
            btnCode.FlatAppearance.BorderSize = 0;
            btnSend.FlatAppearance.BorderSize = 0;
            btnConnect.FlatAppearance.BorderSize = 0;
            ChatPanel.Hide();
            this.Size = new Size(484, 87);
            btnConnect.Location = new Point(12, 36);
            // Add "Welcome to the chat room." encrypted
            listBox1.Items.Add(Encrypt("Welcome to the chat room.", EncryptKey));
            listBox1.SelectedIndex = 0;
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        #endregion

        #region ChatPanel Paint
        private void ChatPanel_Paint(object sender, PaintEventArgs e)
        {
            if (SpaceUsedinPanel >= 400)
            {
                listBox1.Items.Clear(); // Fix this later
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

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                string txtMessage = Decrypt(listBox1.SelectedItem.ToString(), EncryptKey);
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

                string txtCode = txtMessage.Replace("[Code]", "").Replace("[/Code]", "");

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
                rtbCode.Text = txtCode;
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
                        rtbCode.SelectionColor = Color.FromArgb(0, 170, 170);
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }

                    // Objects in pink
                    foreach (Match keyWordMatch in ObjectKeywords.Matches(rtbCode.Text))
                    {
                        rtbCode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        // Change colours
                        rtbCode.SelectionColor = Color.FromArgb(255, 60, 200);
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }

                    // Functions in yellow
                    foreach (Match keyWordMatch in FunctionKeywords.Matches(rtbCode.Text))
                    {
                        rtbCode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        // Change colours
                        rtbCode.SelectionColor = Color.FromArgb(210, 182, 20);
                        rtbCode.SelectionFont = fntSyntaxHighlighter;
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Clickable Links
        private void rtbCode_Click(object sender, EventArgs e)
        {
            if (IsUrl(Decrypt(listBox1.SelectedItem.ToString(), EncryptKey).Split(' ')[0]))
            {
                Process.Start(Decrypt(listBox1.SelectedItem.ToString(), EncryptKey).Split(' ')[0]);
            }
        }
        #endregion

        #region Movable Form
        private void ChatForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            start_point = new Point(e.X, e.Y);
        }

        private void ChatForm_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag = !dragging;
            if (!flag)
            {
                Point p = base.PointToScreen(e.Location);
                base.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void ChatForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        #region Detect URLs

        private bool IsUrl(string url)
        {
            bool result = false;

            try
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == "http" || uri.Scheme == "https")
                {
                    result = true;
                }
            }
            catch
            {
                
            }

            return result;
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

        delegate void SetTextCallback(string text);

        private void MessageListAdd(string TextToAdd)
        {
            if (this.listBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(MessageListAdd);
                this.Invoke(d, new object[] { TextToAdd });
            }
            else
            {
                listBox1.Items.Add(TextToAdd);
            }
        }

        delegate void PanelRefresh();

        private void RefreshChatPanel()
        {
            if (this.ChatPanel.InvokeRequired)
            {
                PanelRefresh d = new PanelRefresh(RefreshChatPanel);
                this.Invoke(d);
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
            // Convert Byte[] to string and decrypt
            ASCIIEncoding AsciiEncoding = new ASCIIEncoding();
            string receivedMessage = AsciiEncoding.GetString(ReceivedData);
            // Ensure the text is also shown on our client as well
            MessageListAdd(receivedMessage);
            // Reset buffer
            Buffer = new byte[1500];
            Sock.BeginReceiveFrom(Buffer, 0, Buffer.Length, SocketFlags.None, ref Remote, new AsyncCallback(MessageCallBack), Buffer);
            // Reload all messages
            RefreshChatPanel();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            Connect cnct = new Connect();
            cnct.Show();
            // Get local IP address
            string localIP = GetIP();
            // Socket Set up
            Sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            // Bind Socket
            Local = new IPEndPoint(IPAddress.Parse(localIP), 3333);
            Sock.Bind(Local);

            // Wait until Connect.cs is closed
            while (ConnectShown)
            {
                await Task.Delay(100);
            }

            try
            {
                // Connect
                Remote = new IPEndPoint(IPAddress.Parse(RemoteIP), 3333);
                Sock.Connect(Remote);

                // Listening to Port
                Buffer = new byte[1500];
                Sock.BeginReceiveFrom(Buffer, 0, Buffer.Length, SocketFlags.None, ref Remote, new AsyncCallback(MessageCallBack), Buffer);
                
                // Change the UI
                this.Size = new Size(484, 616);
                btnConnect.Hide();
                ChatPanel.Show();
                lblTitle.Text = "Chatting in " + RemoteIP;
            }
            catch
            {
                Message m = new Message("Unable to Connect", "Continue");
                m.Show();
            }
        }
        #endregion

        #region Send Button
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            Send(Encrypt(txtChat.Text, EncryptKey));
        }
        #endregion

        #region Send Function
        private void Send(string Text)
        {
            TexttoSend = Text;
            // Convert text to 1500 byte array
            UTF8Encoding uEncoding = new UTF8Encoding();
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            var bytes = uEncoding.GetBytes(TexttoSend);

            // If you aren't connected to yourself, write the message on your own client
            if (!(RemoteIP == GetIP()))
            {
                listBox1.Items.Add(TexttoSend);
            }

            try
            {
                Sock.Send(bytes);
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
            string[] test = Input.Split(' ');
            string Output = "";
            // XOR each encrypted character value by the Key
            for (int i = 0; i < test.Length - 1; i++)
            {
                test[i] = test[i].Replace(" ", "");
                Output += Convert.ToChar(Convert.ToInt32(test[i]) ^ Key);
            }

            return Output;
        }
        #endregion
    }
}
