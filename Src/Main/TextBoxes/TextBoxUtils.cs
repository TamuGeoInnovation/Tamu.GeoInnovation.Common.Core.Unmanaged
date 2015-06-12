using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace USC.GISResearchLab.Common.Forms.Utils.TextBoxes
{
    public class TextBoxUtils
    {

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_VSCROLL = 277;
        private const uint SB_BOTTOM = 7;

        private IntPtr _Handle;
        private TextBox _TextBox;

        public IntPtr Handle
        {
            get { return _Handle; }
            set { _Handle = value; }
        }

        public TextBox textBox
        {
            get { return _TextBox; }
            set { _TextBox = value; }
        }

        public TextBoxUtils(IntPtr handle, TextBox txtBox)
        {
            _Handle = handle;
            _TextBox = txtBox;
        }

        public static void ScrollToBottom(TextBox txtBox)
        {
            try
            {
                IntPtr ptrWparam = new IntPtr(SB_BOTTOM);
                IntPtr ptrLparam = new IntPtr(0);
                SendMessage(txtBox.Handle, WM_VSCROLL, ptrWparam, ptrLparam);
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred scrolling to bottom", e);
            }
        }

        public static void Append(TextBox textBox, string s)
        {
            Append(textBox, s, false);
        }

		// Kaveh: I've added a control if here to avoid OutOfMemory exceptions where the textbox gets overloaded with trace messages.
		public static void Append(TextBox textBox, string s, bool autoScroll)
		{
			if (s != null)
			{
				if ((s.Length + textBox.TextLength) > textBox.MaxLength)
				{
					textBox.Text = "Textbox messages truncated from here..." + Environment.NewLine + s;
					if (autoScroll) ScrollToBottom(textBox);

				}
				else
				{
					if (autoScroll)
					{
						textBox.AppendText(s);
						ScrollToBottom(textBox);
					}
					else
					{
						textBox.Text += s;
					}
				}
			}
		}

        public static void AppendFormat(TextBox textBox, string s, object arg0)
        {
            AppendFormat(textBox, s, new object[] { arg0 });
        }

        public static void AppendFormat(TextBox textBox, string s, object arg0, object arg1)
        {
            AppendFormat(textBox, s, new object[] { arg0, arg1 });
        }

        public static void AppendFormat(TextBox textBox, string s, object arg0, object arg1, object arg2)
        {
            AppendFormat(textBox, s, new object[] { arg0, arg1, arg2 });
        }

        public static void AppendFormat(TextBox textBox, string s, object[] args)
        {
            AppendFormat(textBox, s, args, false);
        }

        public static void AppendFormat(TextBox textBox, string s, object []args, bool autoScroll)
        {
            if (s != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(s, args);

                if (autoScroll)
                {
                    textBox.AppendText(sb.ToString());
                    ScrollToBottom(textBox);
                }
                else
                {
                    textBox.Text += sb.ToString();
                }
            }

        }

        public static void AppendLine(TextBox textBox)
        {
            AppendLine(textBox, "");
        }

        public static void AppendLine(TextBox textBox, string s)
        {
            AppendLine(textBox, s, false);
        }

        public static void AppendLine(TextBox textBox, string s, bool autoScroll)
        {
            if (s != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(s);
                if (textBox.Text != "")
                {
                    sb.AppendLine();
                }
                if (autoScroll)
                {
                    textBox.AppendText(sb.ToString());
                    ScrollToBottom(textBox);
                }
                else
                {
                    textBox.Text += sb.ToString();
                }
            }
        }

        public static void AppendFormattedLine(TextBox textBox, string s, object arg0)
        {
            AppendFormattedLine(textBox, s, new object[] { arg0 });
        }

        public static void AppendFormattedLine(TextBox textBox, string s, object arg0, object arg1)
        {
            AppendFormattedLine(textBox, s, new object[] { arg0, arg1 });
        }

        public static void AppendFormattedLine(TextBox textBox, string s, object arg0, object arg1, object arg2)
        {
            AppendFormattedLine(textBox, s, new object[] { arg0, arg1, arg2 });
        }

        public static void AppendFormattedLine(TextBox textBox, string s, object[] args)
        {
            AppendFormattedLine(textBox, s, args, false);
        }

        public static void AppendFormattedLine(TextBox textBox, string s, object[] args, bool autoScroll)
        {
            if (s != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(s, args);
                if (textBox.Text != "")
                {
                    sb.AppendLine();
                }
                if (autoScroll)
                {
                    textBox.AppendText(sb.ToString());
                    ScrollToBottom(textBox);
                }
                else
                {
                    textBox.Text += sb.ToString();
                }
            }
        }
    }
}
