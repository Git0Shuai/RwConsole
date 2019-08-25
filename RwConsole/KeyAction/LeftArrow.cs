using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class LeftArrow: KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            ctx.Get<InputBuffer>().CursorLeftMove();
        }
    }
}
