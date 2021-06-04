using MakeTopmost.Client.Wpf.Contracts;
using MakeTopmost.Client.Wpf.Models;
using System.Runtime.InteropServices;

namespace MakeTopmost.Client.Wpf.Services
{
    public class Win32HotKeyService : IHotKeyService
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(nint hWnd, int id, int fsModifiers, int vlc);

        public bool RegisterHotKey(nint hWnd, int hotKeyId, FsModifier fsModifier, int key)
        {
            var hotKeyRegistered = RegisterHotKey(hWnd, hotKeyId, (int)fsModifier, key);
            return hotKeyRegistered;
        }
    }
}
