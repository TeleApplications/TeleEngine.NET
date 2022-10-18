using Silk.NET.OpenGL;
using System.Numerics;
using TeleEngine.NET.Components;
using TeleEngine.NET.Components.CameraComponenets.Interfaces;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.InputManager.Inputs;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.Models.Formats;
using TeleEngine.NET.SharedObjects;

namespace TeleEngine.NET.Examples.GettingStarted.Components
{
    internal sealed class TestComponent : VertexComponent
    {
        private static KeyboardInput keyboardState = Shared.GetInstance<KeyboardInput>()!;
        private static MouseInput mouseState = Shared.GetInstance<MouseInput>()!;

        public float Speed { get; set; } = 2f;

        public override Transform Transform { get; set; } =
            new()
            {
                Position = new(0, 0, 0),
                Rotation = System.Numerics.Quaternion.Identity,
                Scale = 1.25f
            };
        public override VertexModel Model =>
            new ObjFormat(@"C:\Users\uzivatel\source\repos\TeleEngine.NET\source\TeleEngine.NET.Examples.GettingStarted\Objects\cube.obj").CreateModel();

        public TestComponent() 
        {
        }

        public override async Task UpdateAsync(GL openGL, ICamera camera)
        {
            mouseState.MouseSensivity = 7;
            var mousePosition = mouseState.CalculateRelativeMousePosition();
            MainScene.CurrentViewWindow.Title = $"X: {mousePosition.X} Y: {mousePosition.Y}";

            var mouseDelta = mouseState.CalculateMouseDelta((Vector2D)Vector2.Zero);
            camera.Yaw += mouseDelta.X;
            camera.Pitch -= mouseDelta.Y;


            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.W))
                camera.Transform.Position += (camera.VectorData.Front * new Vector3D(1, 0, 1)) * Speed * MainScene.DeltaTime;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.S))
                camera.Transform.Position -= (camera.VectorData.Front * new Vector3D(1, 0, 1)) * Speed * MainScene.DeltaTime;

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.A))
                camera.Transform.Position -= (Vector3D)((Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up))) * Speed * MainScene.DeltaTime);
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.D))
                camera.Transform.Position += (Vector3D)((Vector3.Normalize(Vector3.Cross(camera.VectorData.Front, camera.VectorData.Up))) * Speed * MainScene.DeltaTime);

            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.Q))
                camera.Transform.Position -= camera.VectorData.Up * Speed * MainScene.DeltaTime;
            if (keyboardState.GetCurrentKeyState(Silk.NET.GLFW.Keys.Space))
                camera.Transform.Position += camera.VectorData.Up * Speed * MainScene.DeltaTime;

            mouseState.LockMouse();
        }
    }
}
