using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MakeTopmost.Client.Forms
{
    public class ShellForm : Form
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(nint hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter,
            int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern nint GetForegroundWindow();

        private readonly NotifyIcon _notifyIcon;

        public ShellForm()
        {
            _notifyIcon = BuildNotifyIcon();
            Visible = false;
            this.Closing += OnClosing;

            int alt = (int) FsModifier.Alt;
            int altShift = (int) (FsModifier.Alt | FsModifier.Shift);
            int t = (int) Keys.T;

            RegisterHotKey(this.Handle, 1, alt, t);
            RegisterHotKey(this.Handle, 2, altShift, t);
        }

        private NotifyIcon BuildNotifyIcon()
        {
            var contextMenuStrip = new ContextMenuStrip();
            var toolStripButton = new ToolStripButton("Exit", null, OnExitClick);
            contextMenuStrip.Items.Add(toolStripButton);

            return new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Visible = true,
                Icon = Icon
            };
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            this.Closing -= OnClosing;
            _notifyIcon.Dispose();
            this.Close();
            Application.Exit();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg is 0x0312)
                SetForegroundWindowPos(new IntPtr(-m.WParam.ToInt32()));

            base.WndProc(ref m);
        }

        private static void SetForegroundWindowPos(nint hWndInsertAfter)
        {
            nint activeWindowPointer = GetForegroundWindow();
            SetWindowPos(activeWindowPointer, hWndInsertAfter, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0040);
        }

        [Flags]
        private enum FsModifier
        {
            Alt = 0x0001,
            Control = 0x0002,
            NoRepeat = 0x4000,
            Shift = 0x0004,
            Win = 0x0008
        }
    }
}
