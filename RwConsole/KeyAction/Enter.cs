using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class Enter : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            if (cki.Key != ConsoleKey.Enter)
            {
                return;
            }

            var current = ctx.Get<InputBuffer>().GetInput();
            if (current.Length == 0)
            {
                return;
            }

            var inputHistory = ctx.Get<InputHistory>();
            inputHistory.Last();
            if (inputHistory.Pre() == current)
            {
                inputHistory.Next();
                return;
            }

            inputHistory.Next();
            inputHistory.Update(current);
            inputHistory.Enqueue(string.Empty);
        }

        public override void OnRegist(ContextContainer ctx)
        {
            if (ctx.Get<InputHistory>() == null)
            {
                ctx.Set(new InputHistory(100));
            }
        }
    }
}
