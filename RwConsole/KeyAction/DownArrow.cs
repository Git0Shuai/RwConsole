using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class DownArrow : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, Context ctx)
        {
            if (cki.Key != ConsoleKey.DownArrow)
            {
                return;
            }

            var current = ctx.InputBuffer.GetInput();
            if (current.Length != 0)
            {
                ctx.InputHistory.Update(current);
            }

            current = ctx.InputHistory.Next();
            if (current == null)
            {
                return;
            }

            ctx.InputBuffer.ForceSetInput(current);
            ctx.InputBuffer.ForceSetCursorPos(current.Length);
        }
    }
}
