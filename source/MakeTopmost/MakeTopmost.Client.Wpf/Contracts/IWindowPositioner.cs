using MakeTopmost.Client.Wpf.Models;

namespace MakeTopmost.Client.Wpf.Contracts
{
    public interface IWindowPositioner
    {
        bool SetForegroundWindowPosition(InsertAfter insertAfter = InsertAfter.NoTopMost);
    }
}