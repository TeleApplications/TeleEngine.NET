using System.Numerics;
using TeleEngine.NET.MathComponents.Vectors;

namespace TeleEngine.NET.Components.CameraComponenets
{
    public readonly struct CameraVectorData
    {
        public Vector3D Up { get; }
        public Vector3D Front { get; }

        public CameraVectorData(Vector3D up, Vector3D front) 
        {
            Up = up;
            Front = front;
        }
    }
}
