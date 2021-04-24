using MakeTopmost.Client.Win32;

namespace MakeTopmost.Client.Contracts
{
    public interface IHotKeyService
    {
        void RegisterHotKey(nint hWnd, int hotKeyId, FsModifier fsModifier, int key);
    }
}