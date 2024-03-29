﻿using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class RightArrow: KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            ctx.Get<InputBuffer>().CursorRightMove();
        }
    }
}
