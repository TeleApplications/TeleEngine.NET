using System.Numerics;

namespace TeleEngine.NET.Components.CameraComponenets
{
    public readonly struct CameraVectorData
    {
        public Vector3 Up { get; }
        public Vector3 Front { get; }

        public CameraVectorData(Vector3 up, Vector3 front) 
        {
            Up = up;
            Front = front;
        }
    }
}
