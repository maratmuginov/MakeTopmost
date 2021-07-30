using System;
using System.Drawing;
using System.Windows.Forms;

namespace MakeTopmost.Client.Windows
{
    public delegate void ExitRequested();

    public class TrayIcon
    {
        public TrayIcon(Icon icon)
        {
            _notifyIcon = GetNotifyIcon(icon);
        }

        public event ExitRequested ExitRequested;
        private readonly NotifyIcon _notifyIcon;

        private NotifyIcon GetNotifyIcon(Icon icon)
        {
            ContextMenuStrip contextMenuStrip = new();
            contextMenuStrip.Items.Add("Exit", null, OnExitClick);
            return new NotifyIcon
            {
                Visible = true,
                Text = "MakeTopmost",
                Icon = icon,
                ContextMenuStrip = contextMenuStrip
            };
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            ExitRequested?.Invoke();
        }
    }
}
