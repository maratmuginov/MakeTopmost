using System;
using System.Drawing;
using System.Windows.Forms;
using MakeTopmost.Client.Contracts;

namespace MakeTopmost.Client
{
    public class TrayIcon : ITrayIcon, IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly Icon _icon;
        public TrayIcon(Icon icon)
        {
            _icon = icon;
            _notifyIcon = BuildNotifyIcon();
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
                Icon = _icon
            };
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            ExitRequested?.Invoke();
        }

        public event ExitRequested ExitRequested;

        public void Dispose()
        {
            _notifyIcon?.Dispose();
        }
    }
}
