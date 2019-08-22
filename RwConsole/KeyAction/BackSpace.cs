using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    class BackSpace : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, Context ctx)
        {
            ctx.InputBuffer.Delete();
        }
    }
}
