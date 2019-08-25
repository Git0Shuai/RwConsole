using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class Tab : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            if (cki.Key != ConsoleKey.Tab)
            {
                return;
            }

            var inputBuffer = ctx.Get<InputBuffer>();

            var completeResult = ctx.Get<CompleteEngine>().Complete(inputBuffer.GetInput());
            inputBuffer.ForceSetInput(completeResult);
            inputBuffer.ForceSetCursorPos(inputBuffer.Count);
        }

        public override void OnRegist(ContextContainer ctx)
        {
            if (ctx.Get<CompleteEngine>() == null)
            {
                ctx.Set(new CompleteEngine());
            }
        }
    }
}
