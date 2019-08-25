using System;
using System.Collections.Generic;

namespace RwConsole.KeyActionContext
{
    public class ContextContainer
    {
        internal ContextContainer()
        {
            contextDict = new Dictionary<Type, object>();
        }

        public T Get<T>() where T : class, IContext
        {
            if (contextDict.TryGetValue(typeof(T), out var ctx))
            {
                return ctx as T;
            }
            return null;
        }

        public void Set<T>(T ctx) where T: class, IContext
        {
            contextDict[typeof(T)] = ctx;
        }

        private readonly Dictionary<Type, object> contextDict;
    }


    public interface IContext
    {
    }

}
