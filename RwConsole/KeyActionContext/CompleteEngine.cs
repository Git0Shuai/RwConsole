using System;
using System.Collections;
using System.Collections.Generic;

namespace RwConsole.KeyActionContext
{
    internal class CompleteEngine : IContext
    {
        public CompleteEngine(List<string> candidates = null)
        {

        }

        public string Complete(string prefix)
        {
            // TODO: complete
            return prefix;
            
        }

        public void AddCandidate(string candidate)
        {

        }
    }

    class DoubleArrayTrie : ICollection<string>
    {
        int ICollection<string>.Count => throw new NotImplementedException();

        bool ICollection<string>.IsReadOnly => throw new NotImplementedException();

        void ICollection<string>.Add(string item)
        {
            throw new NotImplementedException();
        }

        void ICollection<string>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<string>.Contains(string item)
        {
            throw new NotImplementedException();
        }

        void ICollection<string>.CopyTo(string[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<string>.Remove(string item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
