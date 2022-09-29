using Silk.NET.GLFW;
using Silk.NET.Windowing;
using System.Collections.Immutable;
using TeleEngine.NET.InputManager.Interfaces;
using TeleEngine.NET.SharedObjects.Attributes;

namespace TeleEngine.NET.InputManager.Inputs
{
    //This is going to a multiplaform implementation, however it will be done until
    //fully working windows platform
    [Shared(typeof(KeyboardInput))]
    public sealed class KeyboardInput : IInput
    {
        private WindowState _windowState;
        private int currentKey;

        public bool IsRecieving { get; private set; } = false;
        public ImmutableArray<int> ValidKeys =>
            ImmutableArray.Create(typeof(Keys).GetEnumValues() as int[]);

        public KeyboardInput(WindowState windowState) 
        {
            _windowState = windowState;
        }

        public async Task<Keys> GetCurrentKeyAsync() 
        {
            if (!IsRecieving)
                IsRecieving = await StartRecievingKeys();
            return (Keys)currentKey;
        }

        private async Task<bool> StartRecievingKeys() 
        {
            IsRecieving = true;
            while (_windowState != WindowState.Minimized) 
            {
                currentKey = await FindActiveKey();
            }
            return false;
        }

        private async Task<int> FindActiveKey() 
        {
            for (int i = 0; i < ValidKeys.Length; i++)
            {
                int currentKey = ValidKeys[i];
                var currentState = await Task.Run(() => InteropHelper.GetAsyncKeyState(currentKey));

                if (currentState != 0)
                    return currentKey;
            }
            return (int)Keys.Unknown;
        }
    }
}
