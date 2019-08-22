using System;
using RwConsole.KeyActionContext;

namespace RwConsole
{
    public abstract class KeyActionBase : IKeyAction
    {
        public abstract void OnReadKey(ConsoleKeyInfo cki, Context ctx);
    }
}
