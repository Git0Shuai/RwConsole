namespace RwConsole.KeyActionContext
{
    public struct Context
    {
        public Context(InputBuffer inputBuffer, InputHistory inputHistory)
        {
            InputBuffer = inputBuffer;
            InputHistory = inputHistory;
        }

        public readonly InputBuffer InputBuffer;

        public readonly InputHistory InputHistory;
    }
}
