using Silk.NET.GLFW;
using System.Collections.Immutable;

namespace TeleEngine.NET.InputManager.Interfaces
{
    public interface IInput<T> where T : Enum
    {
        public ImmutableArray<int> ValidKeys { get; set; }

        public bool GetCurrentKeyState(T key);
        public bool IsInputValid(int inputKey);
    }
}
