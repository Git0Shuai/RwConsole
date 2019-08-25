﻿using System;
using RwConsole.KeyActionContext;

namespace RwConsole.KeyAction
{
    public class DownArrow : KeyActionBase
    {
        public override void OnReadKey(ConsoleKeyInfo cki, ContextContainer ctx)
        {
            if (cki.Key != ConsoleKey.DownArrow)
            {
                return;
            }
            var inputBuffer = ctx.Get<InputBuffer>();
            var inputHistory = ctx.Get<InputHistory>();

            var current = inputBuffer.GetInput();
            if (current.Length != 0)
            {
                inputHistory.Update(current);
            }

            current = inputHistory.Next();
            if (current == null)
            {
                return;
            }

            inputBuffer.ForceSetInput(current);
            inputBuffer.ForceSetCursorPos(current.Length);
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
