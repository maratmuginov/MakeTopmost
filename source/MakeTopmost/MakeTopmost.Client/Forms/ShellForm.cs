using MakeTopmost.Client.Contracts;
using MakeTopmost.Client.Win32;
using System.Windows.Forms;

namespace MakeTopmost.Client.Forms
{
    public class ShellForm : Form
    {
        private readonly IWindowPositioner _windowPositioner;

        public ShellForm(IWindowPositioner windowPositioner, IHotKeyService hotKeyService, ITrayIcon trayIcon)
        {
            _windowPositioner = windowPositioner;

            trayIcon.ExitRequested += OnExitRequested;

            hotKeyService.RegisterHotKey(this.Handle, 1, FsModifier.Alt, (int) Keys.T);
            hotKeyService.RegisterHotKey(this.Handle, 2, FsModifier.Alt | FsModifier.Shift, (int) Keys.T);
        }

        private static void OnExitRequested()
        {
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg is 0x0312)
                RaiseHotKey(m.WParam.ToInt32());

            base.WndProc(ref m);
        }

        private void RaiseHotKey(int hotKeyId)
        {
            var insertAfter = hotKeyId switch
            {
                1 => InsertAfter.TopMost,
                2 or _ => InsertAfter.NoTopMost
            };
            _windowPositioner.SetForegroundWindowPos(insertAfter);
        }
    }
}
