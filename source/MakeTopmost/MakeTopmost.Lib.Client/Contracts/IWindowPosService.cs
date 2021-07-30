using MakeTopmost.Lib.Client.Models;

namespace MakeTopmost.Lib.Client.Contracts
{
    public interface IWindowPosService
    {
        bool SetForegroundWindowPos(InsertAfter insertAfter);
    }
}