using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    class DefaultAction : KeyActionBase
    {
        public static readonly DefaultAction Instance = new DefaultAction();

        public override void OnReadKey(ConsoleKeyInfo cki, Context ctx)
        {
            ctx.InputBuffer.Insert(cki.KeyChar);
        }
    }
}
