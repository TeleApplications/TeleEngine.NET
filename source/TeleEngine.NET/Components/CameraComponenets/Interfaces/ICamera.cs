using System.Collections.Immutable;
using System.Numerics;
using TeleEngine.NET.Shaders;

namespace TeleEngine.NET.Components.CameraComponenets.Interfaces
{
    public interface ICamera
    {
        public ImmutableArray<ShaderResult<ICamera, Matrix4x4>> ShaderResults { get; }

        public CameraVectorData VectorData { get; }
        public Transform Transform { get; set; }

        public int FieldOfView { get; set; }
        public float AspectRatio { get; set; }

        public float Pitch { get; set; }
        public float Yaw { get; set; }
    }
}
