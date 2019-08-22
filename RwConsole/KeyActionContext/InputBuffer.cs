using System;
using System.Collections;
using System.Collections.Generic;

namespace RwConsole.KeyActionContext
{
    public class InputBuffer : IReadOnlyList<char>
    {
        public string Prompt { get; }

        public int Cursor { get; private set; }

        internal InputBuffer(string prompt)
        {
            Prompt = prompt;
            Cursor = 0;
            _input = new List<char>();
        }

        public bool CursorLeftMove()
        {
            if (Cursor == 0)
            {
                return false;
            }

            --Cursor;
            return true;
        }

        public bool CursorRightMove()
        {
            if (Cursor == Count)
            {
                return false;
            }

            ++Cursor;
            return true;
        }

        public void ForceSetCursorPos(int newPos)
        {
            if (newPos < 0 || newPos > Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            Cursor = newPos;
        }

        public void ForceSetInput(string input)
        {
            _input = new List<char>(input);
            if (Cursor > _input.Count)
            {
                Cursor = _input.Count;
            }
        }

        public void Insert(char c)
        {
            _input.Insert(Cursor, c);
            ++Cursor;
        }

        public void Delete()
        {
            if (Cursor == 0)
            {
                return;
            }

            _input.RemoveAt(Cursor - 1);
            --Cursor;
        }

        public void Clear()
        {
            Cursor = 0;
            _input.Clear();
        }

        public int TotalLength() => Count + Prompt.Length;

        public string GetInput() => new string(_input.ToArray());

        #region IReadOnlyList

        public IEnumerator<char> GetEnumerator() => _input.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _input.Count;

        public char this[int index] => _input[index];

        #endregion

        private List<char> _input;
    }
}
