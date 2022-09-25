
using System.Numerics;
using TeleEngine.NET.Components;

namespace TeleEngine.NET
{
    public static class MatrixHelper
    {
        public static float CalculateDegreesToRadians(int degree) =>
            degree * (MathF.PI / 180f);

        public static Matrix4x4 CalculateModelMatrix(int degree) 
        {
            var rotationX = Matrix4x4.CreateRotationX(CalculateDegreesToRadians(degree));
            var rotationY = Matrix4x4.CreateRotationY(CalculateDegreesToRadians(degree));

            return rotationX * rotationY;
        }

        public static Matrix4x4 CalculateViewMatrix(Transform transform) =>
            Matrix4x4.CreateLookAt(transform.Position, transform.Position + (Vector3.UnitZ * -1), Vector3.UnitY);
        public static Matrix4x4 CalculateProjectionMatrix(int fieldOfView, float aspectRatio, float minDistance = 0.1f, float maxDistance = 100f) 
        {
            var fieldRadians = CalculateDegreesToRadians(fieldOfView);
            return Matrix4x4.CreatePerspectiveFieldOfView(fieldRadians, aspectRatio, minDistance, maxDistance);
        }

    }
}
