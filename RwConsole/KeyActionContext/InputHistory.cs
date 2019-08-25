using System.Collections.Generic;

namespace RwConsole.KeyActionContext
{
    public class InputHistory: IContext
    {
        internal InputHistory(int maxSize = 50)
        {
            _maxSize = maxSize;
            _history = new LinkedList<string>();
        }

        public string Pre()
        {
            if (_current?.Previous == null)
            {
                return null;
            }

            _current = _current.Previous;
            return _current.Value;
        }

        public string Next()
        {
            if (_current?.Next == null)
            {
                return null;
            }

            _current = _current.Next;
            return _current.Value;
        }

        public string Last()
        {
            _current = _history.Last;
            return _current?.Value;
        }

        public string Update(string input)
        {
            if (_current == null || input == string.Empty)
            {
                return null;
            }

            _current.Value = input;
            return _current.Value;
        }

        public void Enqueue(string input)
        {
            _current = _history.Last;
            if (_current?.Value != string.Empty)
            {
                _current = _history.AddLast(input);
            }

            while (_history.Count > _maxSize)
            {
                _history.RemoveFirst();
            }
        }

        private readonly int _maxSize;
        private readonly LinkedList<string> _history;
        private LinkedListNode<string> _current;
    }
}
