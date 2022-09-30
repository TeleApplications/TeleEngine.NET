using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
using TeleEngine.NET.SharedObjects;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class TestComponent : VertexComponent
    {
        private static KeyboardInput keyboardState = Shared.GetInstance<KeyboardInput>()!;

        public override Transform Transform { get; set; } =
            new()
            {
                Position = System.Numerics.Vector3.Zero,
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };
        public override VertexModel Model => TriangleModel.Shared.Model;

        public override async Task UpdateAsync(GL openGL)
        {
            var currentKey = await Task.Run(() => keyboardState.GetCurrentKeyAsync());
            if (currentKey == Silk.NET.GLFW.Keys.W)
                Transform = new() { Position = Transform.Position, Rotation = new(Transform.Rotation.X + 0.01f, Transform.Rotation.Y, Transform.Rotation.Z, Transform.Rotation.W), Scale = Transform.Scale };
            if (currentKey == Silk.NET.GLFW.Keys.S)
                Transform = new() { Position = Transform.Position, Rotation = new(Transform.Rotation.X - 0.01f, Transform.Rotation.Y, Transform.Rotation.Z, Transform.Rotation.W), Scale = Transform.Scale };
            if (currentKey == Silk.NET.GLFW.Keys.A)
                Transform = new() { Position = Transform.Position, Rotation = new(Transform.Rotation.X, Transform.Rotation.Y + 0.01f, Transform.Rotation.Z, Transform.Rotation.W), Scale = Transform.Scale };
            if (currentKey == Silk.NET.GLFW.Keys.S)
                Transform = new() { Position = Transform.Position, Rotation = new(Transform.Rotation.X, Transform.Rotation.Y - 0.01f, Transform.Rotation.Z, Transform.Rotation.W), Scale = Transform.Scale };
        }
    }
}
