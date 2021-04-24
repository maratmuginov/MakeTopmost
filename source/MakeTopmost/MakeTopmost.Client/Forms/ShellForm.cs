using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MakeTopmost.Client.Contracts;

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

        private readonly ITrayIcon _trayIcon;

        public ShellForm()
        {
            _trayIcon = new TrayIcon(this.Icon);
            _trayIcon.ExitRequested += OnExitRequested;

            int alt = (int) FsModifier.Alt;
            int altShift = (int) (FsModifier.Alt | FsModifier.Shift);
            int t = (int) Keys.T;

            RegisterHotKey(this.Handle, 1, alt, t);
            RegisterHotKey(this.Handle, 2, altShift, t);
        }

        private void OnExitRequested()
        {
            this.Close();
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg is 0x0312)
                RaiseHotKey(m.WParam.ToInt32());

            base.WndProc(ref m);
        }

        private static void RaiseHotKey(int hotKeyId)
        {
            var hWndInsertAfter = hotKeyId switch
            {
                1 => new IntPtr((int) InsertAfter.TopMost),
                2 or _ => new IntPtr((int) InsertAfter.NoTopMost)
            };
            SetForegroundWindowPos(hWndInsertAfter);
        }

        private static void SetForegroundWindowPos(nint hWndInsertAfter)
        {
            nint activeWindowPointer = GetForegroundWindow();
            uint uFlags = (uint) (SwpFlags.NoSize | SwpFlags.NoMove | SwpFlags.ShowWindow);
            SetWindowPos(activeWindowPointer, hWndInsertAfter, 0, 0, 0, 0, uFlags);
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

        private enum InsertAfter
        {
            NoTopMost = -2,
            TopMost = -1,
            Top = 0,
            Bottom = 1
        }

        [Flags]
        private enum SwpFlags : uint
        {
            NoSize = 0x0001,
            NoMove = 0x0002,
            ShowWindow = 0x0040
        }
    }
}
