﻿using Silk.NET.GLFW;
using System.Collections.Immutable;

namespace TeleEngine.NET.InputManager.Interfaces
{
    public interface IInput
    {
        public ImmutableArray<int> ValidKeys { get; }

        public async Task<Keys> GetCurrentKeyAsync() { return 0; }
    }
}