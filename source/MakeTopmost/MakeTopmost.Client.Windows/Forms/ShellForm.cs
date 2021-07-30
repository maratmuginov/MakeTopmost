using MakeTopmost.Lib.Client.Contracts;
using MakeTopmost.Lib.Client.Models;
using System.Windows.Forms;

namespace MakeTopmost.Client.Windows.Forms
{
    public partial class ShellForm : Form
    {
        public ShellForm(TrayIcon trayIcon, IWindowPosService windowPosService, IHotKeyService hotKeyService)
        {
            _windowPosService = windowPosService;
            _hotKeyService = hotKeyService;
            trayIcon.ExitRequested += Close;
            RegisterHotKeys();

            ShowInTaskbar = false;
            Opacity = 0;
            InitializeComponent();
            Hide();
        }

        private readonly IWindowPosService _windowPosService;
        private readonly IHotKeyService _hotKeyService;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg is 0x0312)
                RaiseHotKey(m.WParam.ToInt32());

            base.WndProc(ref m);
        }

        private void RegisterHotKeys()
        {
            const int tKey = (int)Keys.T;
            _hotKeyService.RegisterHotKey(Handle, 1, 1, tKey);
            _hotKeyService.RegisterHotKey(Handle, 2, 1 | 4, tKey);
        }

        private void RaiseHotKey(int hotKeyId)
        {
            InsertAfter insertAfter = hotKeyId switch
            {
                1 => InsertAfter.TopMost,
                _ => InsertAfter.NoTopMost
            };
            _windowPosService.SetForegroundWindowPos(insertAfter);
        }
    }
}
