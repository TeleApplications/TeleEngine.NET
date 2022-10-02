using Silk.NET.GLFW;
using Silk.NET.Windowing;
using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.InputManager.Interfaces;
using TeleEngine.NET.SharedObjects.Attributes;
using TeleEngine.NET.Views;

namespace TeleEngine.NET.InputManager.Inputs
{
    //This is going to a multiplaform implementation, however it will be done until
    //fully working windows platform
    [Shared(typeof(KeyboardInput))]
    public sealed class KeyboardInput : IInput<Keys>
    {
        private int vectorDifference;
        private WindowState _windowState => View.CurrentViewWindow.WindowState;

        public ImmutableArray<int> ValidKeys { get; set; } =
            ImmutableArray.Create
            (
                typeof(Keys).GetEnumValues() as int[]
            );

        public bool GetCurrentKeyState(Keys key)
        {
            if (_windowState == WindowState.Minimized)
                return false;

            int keyIndex = (int)key;
            return IsInputValid(keyIndex) ?
                InteropHelper.GetAsyncKeyState(keyIndex) != 0 : false;
        }

        public bool IsInputValid(int inputKey) 
        {
            int vectorCount = Vector<int>.Count;
            if (vectorDifference == 0)
                vectorDifference = ValidKeys.Length - vectorCount;

            var inputVector = new Vector<int>(inputKey);
            var keysArray = ValidKeys.ToArray();

            for (int i = 0; i < vectorDifference; i += vectorCount) 
            {
                var keyVector = new Vector<int>(keysArray, i);
                if (Vector.EqualsAny(inputVector, keyVector))
                    return true;
            }

            for (int i = vectorDifference; i < vectorCount; i++)
            {
                var currentKey = keysArray[i];
                if (inputKey == currentKey)
                    return true;
            }
            return false;
        }
    }
}
