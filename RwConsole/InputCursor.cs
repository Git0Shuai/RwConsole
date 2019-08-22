using System;

namespace RwConsole
{
    internal class InputCursor
    {
        internal void SetCurrentAsStart()
        {
            Left = Console.CursorLeft;
            Top = Console.CursorTop;
        }

        internal void BackToStart(int preTotalLength, int consoleCursorOffset)
        {
            var preInputLines = Math.Max(preTotalLength, consoleCursorOffset) / Console.BufferWidth + 1;

            // top is no longer the start line, recalculate the start line.
            if (Top + preInputLines > Console.BufferHeight)
            {
                if (preInputLines > 1 && consoleCursorOffset % Console.BufferWidth == 0
                    && consoleCursorOffset > preTotalLength && Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    // unix only issue.
                    --preInputLines;
                }

                var newTop = Console.BufferHeight - preInputLines;
                newTop = newTop < 0 ? 0 : newTop;
                Console.SetCursorPosition(Left, newTop);
                SetCurrentAsStart();
            }
            else
            {
                Console.SetCursorPosition(Left, Top);
            }
        }

        private int Left { get; set; }
        private int Top { get; set; }

    }
}
