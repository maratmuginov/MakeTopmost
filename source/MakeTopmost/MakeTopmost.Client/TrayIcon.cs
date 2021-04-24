using MakeTopmost.Client.Contracts;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MakeTopmost.Client
{
    public class TrayIcon : ITrayIcon
    {
        private readonly NotifyIcon _notifyIcon;
        public event ExitRequested ExitRequested;
        public TrayIcon(Icon icon)
        {
            _notifyIcon = BuildNotifyIcon(icon);
        }

        private NotifyIcon BuildNotifyIcon(Icon icon)
        {
            var contextMenuStrip = new ContextMenuStrip();

            var toolStripButton = new ToolStripButton("Exit", null, OnExitClick);
            contextMenuStrip.Items.Add(toolStripButton);
            contextMenuStrip.PerformLayout();

            return new NotifyIcon
            {
                ContextMenuStrip = contextMenuStrip,
                Visible = true,
                Icon = icon
            };
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            ExitRequested?.Invoke();
        }
    }
}
