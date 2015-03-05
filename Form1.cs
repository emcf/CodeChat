using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using System.Diagnostics;

/*
This chat program is designed for sending text, code, and images through a small
encrypted Peer-to-peer network. This is the unfinished GUI for the client.
The code is fully commented and organized with region tags.
- Increment
- SL x TnT
- Stamiηa
- Rikka / James
*/

namespace ChatUI
{
    public partial class ChatForm : Form
    {
        #region Public Variables
        // Basic essential variables
        public static string Username;
        public static string TexttoSend = "";
        public static bool InCodeMode = false;
        // Helps detect if the oldest message needs to be deleted if the screen is too cluttered
        public static int SpaceUsedinPanel = 0;
        // Used for drawing the rectangle behind messages
        public static Rectangle TextRect;
        // Used for dragging the form
        public static Point start_point;
        public static bool dragging;
        // Used for checking if the console has already welcomed user
        public static bool ConsoleSpoken = false;
        // Regex for syntax highlighter
        public static Regex SyntaxKeywords = new Regex(@"\b(abstract|as|base|break|case|catch|checked|const|continue|default|delegate|do|else|enum|event|explicit|extern|false|finally|fixed|for|foreach|goto|if|implicit|in|interface|internal|is|lock|long|new|null|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|ulong|unchecked|unsafe|ushort|using|virtual|volatile|while|public|new|lines|this)\b", RegexOptions.IgnoreCase);
        public static Regex ObjectKeywords = new Regex(@"\b(image|bitmap|var|font|string|int|bool|byte|char|class|decimal|double|float|namespace|object|uint|this)\b", RegexOptions.IgnoreCase);
        public static Regex FunctionKeywords = new Regex(@"\b(.text|.items|.tostring|.location|.size|.hide|.show)\b", RegexOptions.IgnoreCase);
        #endregion

        #region Rounded Corners on Form
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Because I made this in the opening form (Form1), the program will be started with the IP address in the 
            listBox1.Items.Add("Welcome to the chat room.");
            listBox1.SelectedIndex = 0;
        }
        #endregion

        #region ChatPanel Paint
        private void ChatPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create a codebox incase we receive a code segment
            // I put the codebox here because it needs to be accessible from this entire function
            RichTextBox CodeBox = new RichTextBox();

            // Set Fonts
            Font TextFont = new Font("Segoe UI", 10, GraphicsUnit.Point);
            Font SarcFont = new Font("Comic Sans MS", 10, GraphicsUnit.Point);
            Font UsernameFont = new Font("Segoe UI", 10, FontStyle.Bold, GraphicsUnit.Point);

            // Whenever ChatPanel is refreshed, grab the items from the listbox and draw them in the panel 
            listBox1.SelectedIndex = 0;
            SpaceUsedinPanel = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i == 0 && ConsoleSpoken == false)
                {
                    Username = "Console";
                }
                else
                {
                    // Can someone please implement a username system?
                    Username = "Stamina";
                }

                int DisplacementY = (listBox1.SelectedIndex + SpaceUsedinPanel);
                Text = listBox1.SelectedItem.ToString();

                // Create the ChatPanel's graphics object
                System.Drawing.Graphics graphicsObj;
                graphicsObj = ChatPanel.CreateGraphics();
                // 63 Characters is the approximate length of a line of text
                Brush brush = new SolidBrush(Color.FromArgb(75, 90, 125));
                double AmountofLinesinText = Math.Ceiling(Text.Length / 63.0);
                int SizeofBox = Convert.ToInt32(AmountofLinesinText + 1) * 27;

                // Check if text is code or an image URL or plain text
                if ((Text.Contains("[Code]") && Text.Contains("[/Code]")) || IsUrl(Text) == true)
                {
                    if (Text.Contains("[Code]") && Text.Contains("[/Code]"))
                    {

                        #region Recognize code
                        Text = Text.Replace("[Code]", "").Replace("[/Code]", "");

                        // Count the amount of lines, only works with code because of wordwrapping
                        string[] lines = Regex.Split(Text.Trim(), "\r\n");
                        int SerialsCounter = lines.Length;
                        if (SerialsCounter <= 1)
                        {
                            AmountofLinesinText = Math.Ceiling(Text.Length / 63.0);
                            SizeofBox = Convert.ToInt32(AmountofLinesinText + 1) * 25;
                        }
                        else
                        {
                            SizeofBox = Convert.ToInt32(SerialsCounter) * 27;
                        }


                        // Create a new RichTextBox
                        CodeBox.TabIndex = 3;
                        CodeBox.BackColor = Color.FromArgb(75, 90, 125);
                        CodeBox.ForeColor = Color.White;
                        CodeBox.ScrollBars = RichTextBoxScrollBars.None;
                        CodeBox.SelectionColor = Color.FromArgb(50, 64, 100);
                        CodeBox.Font = TextFont;

                        // Draw the rectangle behind the text
                        Rectangle UsernameRect = new Rectangle(13, DisplacementY + 13, 439, 100);
                        graphicsObj.FillRectangle(brush, 10, DisplacementY + 10, 439, SizeofBox);

                        // Make the codebox look the same as the speech bubble rectangles
                        CodeBox.BorderStyle = BorderStyle.None;
                        CodeBox.Location = new Point(14, DisplacementY + 33);
                        CodeBox.Size = new Size(435, SizeofBox - 24);
                        CodeBox.ReadOnly = true;
                        Text = Text.Replace("\t", " ");
                        Text = Text.Replace("                        ", " ");
                        CodeBox.Text = Text;

                        ChatPanel.Controls.Add(CodeBox);
                        #endregion

                        #region Syntax Highlighter
                        // Select text from the begining
                        int selPos = CodeBox.SelectionStart;

                        // Find syntax keywords for C#
                        foreach (Match keyWordMatch in SyntaxKeywords.Matches(CodeBox.Text))
                        {
                            CodeBox.Select(keyWordMatch.Index, keyWordMatch.Length);
                            // Change colours
                            CodeBox.SelectionColor = Color.FromArgb(0, 170, 170);
                            CodeBox.SelectionFont = UsernameFont;
                            CodeBox.SelectionStart = selPos;
                            CodeBox.SelectionColor = Color.FromArgb(255, 75, 100);
                            CodeBox.SelectionFont = UsernameFont;
                        }

                        // Find object/variable keywords for C#
                        foreach (Match keyWordMatch in ObjectKeywords.Matches(CodeBox.Text))
                        {
                            CodeBox.Select(keyWordMatch.Index, keyWordMatch.Length);
                            // Change colours
                            CodeBox.SelectionColor = Color.FromArgb(255, 75, 100);
                            CodeBox.SelectionFont = UsernameFont;
                            CodeBox.SelectionStart = selPos;
                            CodeBox.SelectionFont = UsernameFont;
                        }

                        #endregion

                    }
                    else if (IsUrl(Text) == true)
                    {

                        #region Recognize URLs
                        string[] lines = Regex.Split(Text.Trim(), "\r\n");
                        int SerialsCounter = lines.Length;
                        if (SerialsCounter <= 1)
                        {
                            AmountofLinesinText = Math.Ceiling(Text.Length / 63.0);
                            SizeofBox = Convert.ToInt32(AmountofLinesinText + 1) * 25;
                        }
                        else
                        {
                            SizeofBox = Convert.ToInt32(SerialsCounter) * 27;
                        }

                        // Create a new RichTextBox to contain the image link
                        CodeBox.TabIndex = 3;
                        CodeBox.BackColor = Color.FromArgb(75, 90, 125);
                        CodeBox.ForeColor = Color.White;
                        CodeBox.ScrollBars = RichTextBoxScrollBars.None;
                        CodeBox.SelectionColor = Color.FromArgb(50, 64, 100);
                        CodeBox.Font = TextFont;
                        CodeBox.DetectUrls = false;

                        // Make the codebox look the same as the speech bubble rectangles
                        CodeBox.BorderStyle = BorderStyle.None;
                        CodeBox.Location = new Point(14, DisplacementY + 33);
                        CodeBox.Size = new Size(435, SizeofBox - 24);
                        CodeBox.ReadOnly = true;
                        Text = Text.Replace("\t", " ");
                        Text = Text.Replace("                        ", " ");
                        CodeBox.Text = Text;
                        CodeBox.Cursor = Cursors.Hand;

                        // Change the colour of the link, if there is a space, the link ends and text begins.
                        string str = CodeBox.Text;

                        CodeBox.Select(0, CodeBox.Text.Split(' ')[0].Length);
                        CodeBox.SelectionColor = Color.FromArgb(0, 170, 170);
                        CodeBox.Click += new EventHandler(CodeBox_Click);

                        // Draw the rectangle behind the text
                        Rectangle UsernameRect = new Rectangle(13, DisplacementY + 13, 439, 100);
                        graphicsObj.FillRectangle(brush, 10, DisplacementY + 10, 439, SizeofBox);

                        ChatPanel.Controls.Add(CodeBox);
                        #endregion
                    }

                    // Wordwrapping
                    TextFormatFlags flags = TextFormatFlags.WordBreak;
                    // Draw the text
                    Rectangle UsernameRect2 = new Rectangle(13, DisplacementY + 13, 439, 100);
                    TextRenderer.DrawText(e.Graphics, Username, UsernameFont, UsernameRect2, Color.FromArgb(0, 170, 170), flags);
                }
                else
                {
                    // Draw the rectangle behind the text
                    graphicsObj.FillRectangle(brush, 10, DisplacementY + 10, 439, SizeofBox);

                    // These are the boundaries for the text, they aren't drawn on the screen
                    Rectangle UsernameRect = new Rectangle(13, DisplacementY + 13, 439, 100);
                    TextRect = new Rectangle(13, DisplacementY + 33, 439, SizeofBox);

                    // Wordwrapping
                    TextFormatFlags flags = TextFormatFlags.WordBreak;

                    // Draw the text
                    if (Text.StartsWith(">"))
                    {
                        // Greentext
                        TextRenderer.DrawText(e.Graphics, Username, UsernameFont, UsernameRect, Color.FromArgb(0, 170, 170), flags);
                        TextRenderer.DrawText(e.Graphics, Text, TextFont, TextRect, Color.FromArgb(120, 153, 34), flags);
                    }
                    else if (Text.StartsWith("Sarc."))
                    {
                        // Sarcasm
                        TextRenderer.DrawText(e.Graphics, Username, UsernameFont, UsernameRect, Color.FromArgb(0, 170, 170), flags);
                        TextRenderer.DrawText(e.Graphics, Text.Replace("Sarc.", ""), SarcFont, TextRect, Color.White, flags);
                    }
                    else
                    {
                        // Normal
                        TextRenderer.DrawText(e.Graphics, Username, UsernameFont, UsernameRect, Color.FromArgb(0, 170, 170), flags);
                        TextRenderer.DrawText(e.Graphics, Text, TextFont, TextRect, Color.White, flags);
                    }
                }

                // This is the distance of the text from the top of the panel, ensures that new messages are 55 units below previous ones 
                SpaceUsedinPanel = SpaceUsedinPanel + SizeofBox + 3;

                if (SpaceUsedinPanel >= 370)
                {
                    if (SpaceUsedinPanel >= 300)
                    {
                        // Reload the ChatPanel when there is too much clutter in it
                        listBox1.Items.RemoveAt(0);
                        // The console's original message will be long gone
                        ConsoleSpoken = true;

                        foreach (Control p in ChatPanel.Controls)
                        {
                            p.Hide();
                        }

                        ChatPanel.Invalidate();
                    }
                }

                // Try and Catch because there isn't always a second item in the listbox
                try
                {
                    listBox1.SelectedIndex++;
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Clickable Links
        private void CodeBox_Click(object sender, EventArgs e)
        {
            Process.Start(listBox1.SelectedItem.ToString().Split(' ')[0]);
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

        #region Close Button
        private void button1_Click(object sender, EventArgs e)
        {
            // Rest in Peace
            Application.Exit();
        }
        #endregion

        #region Send Button
        private void ButtonSend_Click(object sender, EventArgs e)
        {
            if (ChatTextbox.Text == " ")
            {
                // It glitches the hell out when you send just spaces
            }
            else if (ChatTextbox.Text == "")
            {
                // It glitches the hell out when you send just spaces
            }
            else
            {
                if (InCodeMode == false) // Not in code mode
                {
                    // Send text here, I haven't added any code to actually send text through a network
                    TexttoSend = ChatTextbox.Text;
                    // For now, it stores the chat log in a textbox. We could use a string array instead, but listboxes are easier because we can see them at runtime.
                    listBox1.Items.Add(TexttoSend);
                    // Repaint the ChatPanel
                    this.Refresh();
                    // Clear textbox
                    ChatTextbox.Clear();
                }
                if (InCodeMode == true) // In code mode
                {
                    ChatTextbox.Size = new Size(460, 22);
                    ChatPanel.Size = new Size(460, 408);
                    ChatTextbox.Location = new Point(12, 448);
                    InCodeMode = false;
                    ChatTextbox.Multiline = false;
   
                    // Send text here, I haven't added any code to actually send text through a network
                    TexttoSend = "[Code]" + ChatTextbox.Text + "[/Code]";
                    // For now, it stores the chat log in a textbox. We could use a string array instead, but listboxes are easier because we can see them at runtime.
                    listBox1.Items.Add(TexttoSend);
                    // Repaint the ChatPanel
                    this.Refresh();
                    // Clear textbox
                    ChatTextbox.Clear();
                }
            }
        }
        #endregion

        #region Code Button
        private void ButtonCode_Click(object sender, EventArgs e)
        {
            if (InCodeMode == true)
            {
                // Code mode off
                ChatTextbox.Clear();
                ChatTextbox.Size = new Size(460, 22);
                ChatPanel.Size = new Size(460, 408);
                ChatTextbox.Location = new Point(12, 448);
                InCodeMode = false;
                ChatTextbox.Multiline = false;
                this.Refresh();
            }
            else if (InCodeMode == false)
            {
                // Code mode
                ChatPanel.Size = new Size(460, 251);
                InCodeMode = true;
                ChatTextbox.Multiline = true;
                ChatTextbox.Location = new Point(12, 291);
                ChatTextbox.Size = new Size(460, 177);
                ChatTextbox.Text = "";
            }
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
            catch (Exception ex)
            {
                
            }

            return result;
        }

        #endregion
    }
}
