using SharpGL;
using SharpGL.Enumerations;
using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Intefaces;

namespace TeleEngine.NET.Components.Vertices
{
    public abstract class VertexComponent : IComponent
    {
        public int ComponenetId { get; set; }
        public Vector3 DefaultColor { get; set; } = new(1f, 1f, 1f);

        protected abstract ImmutableArray<Vector3> vertices { get; }
        protected virtual BeginMode vertexMode { get; } = BeginMode.Lines;

        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get; set; } = Vector3.Zero;

        public virtual async Task StartAsync(OpenGL openGL)
        {
            openGL.Clear(OpenGL.GL_ACCUM_BUFFER_BIT);
            openGL.LoadIdentity();
            openGL.Translate(Position.X, Position.Y, Position.Z);
            openGL.Rotate(Rotation.X, Rotation.Y, Rotation.Z);

            for (int i = 0; i < vertices.Length; i++)
            {
                openGL.Begin(vertexMode);

                var currentVertex = vertices[i];
                openGL.Vertex4f(currentVertex.X, currentVertex.Y, currentVertex.Z, 1f);

                openGL.End();
            }
        }

        public virtual async Task UpdateAsync(OpenGL openGL) { }
    }
}
