using System;
using System.Diagnostics;
using System.Windows.Forms;
using USC.GISResearchLab.Common.Forms.Utils.TextBoxes;

namespace USC.GISResearchLab.Common.Diagnostics.TraceListeners
{
    public class TextBoxTraceListener : TraceListener
    {

        #region Properties
        private string _DllPath;
        private TextBox _Target;
        private StringSendDelegate _InvokeWrite;
        private bool _AutoScroll;

        public bool AutoScroll
        {
            get { return _AutoScroll; }
            set { _AutoScroll = value; }
        }

        public string DllPath
        {
            get { return _DllPath; }
            set { _DllPath = value; }
        }

        public TextBox Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        public StringSendDelegate InvokeWrite
        {
            get { return _InvokeWrite; }
            set { _InvokeWrite = value; }
        }

        #endregion

        public TextBoxTraceListener(TextBox target)
        {
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = null;
            AutoScroll = false;
        }

        public TextBoxTraceListener(TextBox target, bool autoScroll)
        {
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = null;
            AutoScroll = autoScroll;
        }

        public TextBoxTraceListener(TextBox target, string name, bool autoScroll)
        {
            Name = name;
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = null;
            AutoScroll = autoScroll;
        }

        public TextBoxTraceListener(TextBox target, string dllPath)
        {
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = dllPath;
            AutoScroll = false;
        }

        public TextBoxTraceListener(TextBox target, bool autoScroll, string dllPath)
        {
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = dllPath;
            AutoScroll = autoScroll;
        }

        public TextBoxTraceListener(TextBox target, string name, bool autoScroll, string dllPath)
        {
            Name = name;
            Target = target;
            InvokeWrite = new StringSendDelegate(SendString);
            DllPath = dllPath;
            AutoScroll = autoScroll;
        }

        public override void Write(string s)
        {
            string message = GetMessageFromString(s);
            if (message != "")
            {
                if (Target != null)
                {
                    if (Target.IsHandleCreated)
                    {
                        if (Target.Created)
                        {
                            Target.BeginInvoke(InvokeWrite, new object[] { message, AutoScroll });
                        }
                    }
                }
                else
                {
                    throw new Exception("TextBoxTraceListner Error: Target is null");
                }
            }

        }

        public override void WriteLine(string s)
        {
            string message = GetMessageFromString(s);
            if (message != "")
            {
                if (Target != null)
                {
                    if (Target.IsHandleCreated)
                    {
                        if (Target.Created)
                        {
                            Target.BeginInvoke(InvokeWrite, new object[] { message + Environment.NewLine, AutoScroll });
                        }
                    }
                }
                else
                {
                    throw new Exception("TextBoxTraceListner Error: Target is null");
                }
            }
        }

        public delegate void StringSendDelegate(string message, bool autoScroll);
        private void SendString(string message, bool autoScroll)
        {
            if (Target != null)
            {
                if (Target.IsHandleCreated)
                {
                    if (Target.Created)
                    {
                        lock (Target.Text)
                        {
                            TextBoxUtils.Append(Target, message, autoScroll);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("TextBoxTraceListner Error: Target is null");
            }
        }

        public string GetMessageFromString(string s)
        {
            string ret = "";
            if (s != null)
            {
                if (DllPath != null)
                {
                    s = s.Replace(DllPath, "");
                }

                if (s.IndexOf("Information:") != -1)
                {
                    s = s.Replace("Information:", "");

                    s = s.Substring(s.IndexOf(":") + 1);
                }

                if (s.IndexOf("Error:") != -1)
                {
                    s = s.Replace("Error:", "");

                    s = s.Substring(s.IndexOf(":") + 1);
                }
                ret = s.Trim();
            }
            return ret;
        }

    }
}
