using System;
using System.Collections.Generic;
using RwConsole.KeyAction;
using RwConsole.KeyActionContext;

namespace RwConsole
{
    public static class ConsoleIoManager
    {
        private static readonly object Lock = new object();

        #region MyRegion Guard by Lock

        private static ContextContainer ctx;
        private static InputBuffer InputBuffer => ctx.Get<InputBuffer>();
        private static InputCursor _inputCursor;
        private static bool _waitingInputKey;
        // System.Console is also required to be guarded by Lock

        #endregion

        private static bool _init;

        private static readonly Dictionary<ConsoleKey, IKeyAction>
            KeyActions = new Dictionary<ConsoleKey, IKeyAction>();

        public static void StaticInit(string prompt)
        {
            if (_init)
            {
                return;
            }

            ctx = new ContextContainer();
            ctx.Set(new InputBuffer(prompt));

            _inputCursor = new InputCursor();

            RegisterKeyAction(ConsoleKey.Enter, new Enter());
            RegisterKeyAction(ConsoleKey.Backspace, new BackSpace());
            RegisterKeyAction(ConsoleKey.LeftArrow, new LeftArrow());
            RegisterKeyAction(ConsoleKey.RightArrow, new RightArrow());
            RegisterKeyAction(ConsoleKey.UpArrow, new UpArrow());
            RegisterKeyAction(ConsoleKey.DownArrow, new DownArrow());

            _init = true;
        }

        /// <summary>
        /// Custom key-map
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void RegisterKeyAction(ConsoleKey key, IKeyAction action)
        {
            KeyActions[key] = action ?? throw new NullReferenceException("Key action is required NOT NULL");
            action?.OnRegist(ctx);
        }

        /// <summary>
        /// Render input
        /// </summary>
        /// <param name="preTotalLength"></param>
        private static void RenderInputOnCurrentLine(int preTotalLength)
        {
            Console.Write(InputBuffer.Prompt + InputBuffer.GetInput());

            var cursorOffset = InputBuffer.Count - InputBuffer.Cursor;
            var paddingCount = preTotalLength - InputBuffer.TotalLength();

            if (Environment.OSVersion.Platform == PlatformID.Unix
                && InputBuffer.TotalLength() % Console.BufferWidth == 0)
            {
                // on unix platform, if is cursor is going to stay at the left, 
                // write an extra space to ensure BufferArea move up one line.
                Console.Write(' ');
                --paddingCount;
                ++cursorOffset;
            }

            if (paddingCount > 0)
            {
                cursorOffset += paddingCount;
                Console.Write(new string(' ', paddingCount));
            }


            if (cursorOffset <= Console.CursorLeft)
            {
                Console.SetCursorPosition(Console.CursorLeft - cursorOffset, Console.CursorTop);
            }
            else
            {
                cursorOffset -= Console.CursorLeft;
                var newLeft = Console.BufferWidth - (cursorOffset % Console.BufferWidth + 1);
                var newTop = Console.CursorTop - ((cursorOffset - 1) / Console.BufferWidth + 1);
                newTop = newTop < 0 ? 0 : newTop;
                Console.SetCursorPosition(newLeft, newTop);
            }
        }

        /// <summary>
        /// expected to be called in a single thread
        /// </summary>
        /// <returns></returns>
        public static string ReadLine()
        {
            if (!_init)
            {
                throw new UnInitException(nameof(ConsoleIoManager));
            }

            lock (Lock)
            {
                Console.WriteLine();
                _inputCursor.SetCurrentAsStart();
                RenderInputOnCurrentLine(0);
                _waitingInputKey = true;
            }

            while (true)
            {
                var k = Console.ReadKey();
                lock (Lock)
                {
                    var preTotalLength = InputBuffer.TotalLength();
                    var consoleCursorOffset = InputBuffer.Prompt.Length + InputBuffer.Cursor;

                    if (k.Key == ConsoleKey.Enter)
                    {
                        _waitingInputKey = false;
                        if (KeyActions.TryGetValue(ConsoleKey.Enter, out var enterAction))
                        {
                            enterAction.OnReadKey(k, ctx);
                        }

                        var input = InputBuffer.GetInput();
                        InputBuffer.Clear();
                        Console.WriteLine();
                        return input;
                    }


                    if (KeyActions.ContainsKey(k.Key))
                    {
                        KeyActions[k.Key].OnReadKey(k, ctx);
                    }
                    else
                    {
                        if (k.KeyChar > 31)
                        {
                            DefaultAction.Instance.OnReadKey(k, ctx);
                        }
                    }

                    if (k.KeyChar > 0 && k.KeyChar < 26)
                    {
                        if (k.KeyChar == '\b')
                        {
                            --consoleCursorOffset;
                        }

                        if (Environment.OSVersion.Platform < PlatformID.Unix)
                        {
                            if (k.KeyChar == '\t')
                            {
                                consoleCursorOffset += 4;
                                while (consoleCursorOffset % 4 != 0)
                                {
                                    --consoleCursorOffset;
                                }
                            }
                            else
                            {
                                consoleCursorOffset += 2;
                            }
                        }
                    }
                    else
                    {
                        ++consoleCursorOffset;
                    }
                    
                    _inputCursor.BackToStart(preTotalLength, consoleCursorOffset);
                    RenderInputOnCurrentLine(preTotalLength);
                }
            }
        }

        public static void WriteLine(string output)
        {
            if (!_init)
            {
                throw new UnInitException(nameof(ConsoleIoManager));
            }

            lock (Lock)
            {
                if (_waitingInputKey)
                {
                    // first, erase input
                    _inputCursor.BackToStart(InputBuffer.TotalLength(), 0);
                    Console.Write(new string(' ', InputBuffer.TotalLength()));

                    _inputCursor.BackToStart(InputBuffer.TotalLength(), 0);
                    Console.WriteLine(output);

                    _inputCursor.SetCurrentAsStart();
                    RenderInputOnCurrentLine(0);
                }
                else
                {
                    Console.WriteLine(output);
                }
            }
        }
    }
}
