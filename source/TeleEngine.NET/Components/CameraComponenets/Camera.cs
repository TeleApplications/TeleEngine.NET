using Silk.NET.OpenGL;
using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Components.Vertices;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.CameraComponenets
{
    internal sealed class Camera : VertexComponent
    {
        private static readonly int StaticTime = 10;
        private ImmutableArray<ShaderResult<Matrix4x4>> shaderResults;

        public int FieldOfView { get; set; } = 90;
        public float AspectRatio { get; set; } = 1.25f;

        public override VertexModel Model => new()
        {
            Vertices = new float[] { 0f, 0f, 0f },
            Indexes = new uint[] {0, 1, 2 }
        };

        public override Transform Transform { get; set; } = new()
        {
            Position = new Vector3(0, 0f, 0),
            Rotation = Quaternion.Identity,
            Scale = 1 
        };

        public Camera() 
        {
            shaderResults =
                ImmutableArray.Create
                (
                    new ShaderResult<Matrix4x4>("uProjection", () => MatrixHelper.CalculateProjectionMatrix(FieldOfView, AspectRatio)),
                    new ShaderResult<Matrix4x4>("uView", () => MatrixHelper.CalculateViewMatrix(Transform)),
                    new ShaderResult<Matrix4x4>("uModel", () => Matrix4x4.CreateRotationY(MatrixHelper.CalculateDegreesToRadians(25)))
                );
        }

        private void UpdateShaders() 
        {
            for (int i = 0; i < shaderResults.Length; i++)
            {
                var shaderName = shaderResults[i].ShaderName;
                var shaderValue = shaderResults[i].Result.Invoke();

                vertexShader.SetValue(shaderName, shaderValue);
            }
        }

        public override Task RenderAsync(GL openGL)
        {
            UpdateShaders();
            return base.RenderAsync(openGL);
        }
    }
}
