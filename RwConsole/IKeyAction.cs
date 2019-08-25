using System;
using RwConsole.KeyActionContext;

namespace RwConsole
{
    public interface IKeyAction
    {
        void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx);

        void OnRegist(ContextContainer ctx);
    }
}