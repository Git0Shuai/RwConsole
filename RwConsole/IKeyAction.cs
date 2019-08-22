using System;
using RwConsole.KeyActionContext;

namespace RwConsole
{
    public interface IKeyAction
    {
        void OnReadKey(ConsoleKeyInfo cki, Context ctx);
    }
}