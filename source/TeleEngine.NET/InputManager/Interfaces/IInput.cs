using Silk.NET.GLFW;

namespace TeleEngine.NET.InputManager.Interfaces
{
    public interface IInput
    {
        public ReadOnlyMemory<uint> ValidKeys { get; }

        public async Task<uint> GetCurrentKeyAsync() { return 0; }
    }
}
