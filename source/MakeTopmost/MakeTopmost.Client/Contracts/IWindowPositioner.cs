using MakeTopmost.Client.Win32;

namespace MakeTopmost.Client.Contracts
{
    public interface IWindowPositioner
    {
        void SetForegroundWindowPos(InsertAfter insertAfter);
    }
}