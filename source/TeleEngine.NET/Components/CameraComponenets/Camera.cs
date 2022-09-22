using Silk.NET.OpenGL;
using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Components.Vertices.DefaultModels.Models;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.CameraComponenets
{
    internal sealed class Camera : VertexComponent
    {
        private static readonly int StaticTime = 10;
        private ImmutableArray<ShaderResult<Matrix4x4>> shaderResults;

        public int FieldOfView { get; set; } = 90;
        public float AspectRatio { get; set; } = 1.25f;

        public override VertexModel Model => UnknownModel.Shared.Model;
        public override Transform Transform { get; set; } = new()
        {
            Position = new Vector3(0, 0, 0),
            Rotation = Quaternion.Identity,
            Scale = 1
        };

        public Camera() 
        {
            shaderResults =
                ImmutableArray.Create
                (
                    new ShaderResult<Matrix4x4>("uModel", CalculateModelMatrix),
                    new ShaderResult<Matrix4x4>("uView", CalculateViewMatrix),
                    new ShaderResult<Matrix4x4>("uProjection", CalculateProjectionMatrix)
                );
        }

        public override Task RenderAsync(GL openGL)
        {
            return base.RenderAsync(openGL);
        }

        //This is going to be moved into some static math helper class
        private Matrix4x4 CalculateModelMatrix() => Matrix4x4.CreateRotationX(CalculateDegreesToRadians(StaticTime)) * Matrix4x4.CreateRotationY(CalculateDegreesToRadians(StaticTime));
        private Matrix4x4 CalculateViewMatrix() => Matrix4x4.CreateLookAt(Transform.Position, Transform.Position + (Vector3.UnitZ * -1), Vector3.UnitY);
        private Matrix4x4 CalculateProjectionMatrix() => Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, 0.5f, FieldOfView * 1.5f);

        private float CalculateDegreesToRadians(int degree) => MathF.PI / (180f * degree);
    }
}
