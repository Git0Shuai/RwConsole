using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class DefaultAction : KeyActionBase
    {
        public static readonly DefaultAction Instance = new DefaultAction();

        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            ctx.Get<InputBuffer>()?.Insert(cki.KeyChar);
        }
    }
}
