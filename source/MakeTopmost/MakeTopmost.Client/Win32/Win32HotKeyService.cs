using System.Runtime.InteropServices;
using MakeTopmost.Client.Contracts;

namespace MakeTopmost.Client.Win32
{
    public class Win32HotKeyService : IHotKeyService
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(nint hWnd, int id, int fsModifiers, int vlc);

        public void RegisterHotKey(nint hWnd, int hotKeyId, FsModifier fsModifier, int key)
        {
            RegisterHotKey(hWnd, hotKeyId, (int) fsModifier, key);
        }
    }
}
