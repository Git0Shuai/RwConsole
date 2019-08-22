using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class UpArrow : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, Context ctx)
        {
            if (cki.Key != ConsoleKey.UpArrow)
            {
                return;
            }

            var current = ctx.InputBuffer.GetInput();
            if (current.Length != 0)
            {
                ctx.InputHistory.Update(current);
            }

            current = ctx.InputHistory.Pre();
            if (current == null)
            {
                return;
            }

            ctx.InputBuffer.ForceSetInput(current);
            ctx.InputBuffer.ForceSetCursorPos(current.Length);
        }
    }
}
