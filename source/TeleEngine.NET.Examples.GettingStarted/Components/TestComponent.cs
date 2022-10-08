using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Cameras;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.SharedObjects;
using TeleEngine.NET.Views;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class TestComponent : VertexComponent
    {
        private static KeyboardInput keyboardState = Shared.GetInstance<KeyboardInput>()!;
        private static MouseInput mouseState = Shared.GetInstance<MouseInput>()!;

        public float Speed { get; set; } = 0.1f;

        public override Transform Transform { get; set; } =
            new()
            {
                Position = new(0, 0, -2),
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };
        public override VertexModel Model => TriangleModel.Shared.Model;

        public TestComponent() 
        {
        }

        public override async Task UpdateAsync(GL openGL, ICamera camera)
        {
            var mousePosition = mouseState.CalculateRelativeMousePosition();
            MainScene.CurrentViewWindow.Title = $"X: {mousePosition.X} Y: {mousePosition.Y}";
            //Transform = new() { Position = new Vector3(-mousePosition.X, mousePosition.Y, Transform.Position.Z), Rotation = Transform.Rotation, Scale = Transform.Scale };

            int rotationValue = 0;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.Q)) 
                rotationValue = 1;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.E))
                rotationValue = -1;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.W))
                camera.Transform.Position += camera.VectorData.Front * Speed;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.S))
                camera.Transform.Position -= camera.VectorData.Front * Speed;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.A))
                camera.Transform.Position += (Vector3D)(Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up)) * Speed);
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.D))
                camera.Transform.Position -= (Vector3D)Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up)) * Speed;

            camera.Yaw += rotationValue;
        }
    }
}
