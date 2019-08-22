using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    class Enter: KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, Context ctx)
        {
            if (cki.Key != ConsoleKey.Enter)
            {
                return;
            }

            var current = ctx.InputBuffer.GetInput();
            if (current.Length == 0)
            {
                return;
            }

            ctx.InputHistory.Last();
            if (ctx.InputHistory.Pre() == current)
            {
                return;
            }

            ctx.InputHistory.Next();
            ctx.InputHistory.Update(current);
        }
    }
}
