using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class BackSpace : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            ctx.Get<InputBuffer>()?.Delete();
        }
    }
}
