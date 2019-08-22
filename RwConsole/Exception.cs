using System;

namespace RwConsole
{
    class UnInitException: Exception
    {
        public UnInitException(string type) : base(type)
        {
        }
    }
}
