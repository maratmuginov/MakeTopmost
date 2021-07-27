using MakeTopmost.Client.Wpf.Models;

namespace MakeTopmost.Client.Wpf.Contracts
{
    public interface IWindowPosService
    {
        bool SetForegroundWindowPosition(InsertAfter insertAfter = InsertAfter.NoTopMost);
    }
}