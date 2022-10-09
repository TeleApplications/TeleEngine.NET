using Silk.NET.Windowing;
using System.Collections.Immutable;
using System.Drawing;
using System.Numerics;
using TeleEngine.NET.InputManager.Enums;
using TeleEngine.NET.InputManager.Interfaces;
using TeleEngine.NET.MathComponents.Vectors;
using TeleEngine.NET.SharedObjects.Attributes;
using TeleEngine.NET.Views;

namespace TeleEngine.NET.InputManager.Inputs
{
    [Shared(typeof(MouseInput))]
    public sealed class MouseInput : IInput<MouseKeys>
    {
        public float MouseSensivity { get; set; }

        private static IWindow currentWindow => View.CurrentViewWindow;

        public ImmutableArray<int> ValidKeys { get; set; } =
            ImmutableArray.Create
            (
                typeof(MouseKeys).GetEnumValues() as int[]
            );

        public bool GetCurrentKeyState(MouseKeys key) 
        {
            int keyIndex = (int)key;
            if (!IsInputValid(keyIndex))
                return false;

            bool keyState = InteropHelper.GetAsyncKeyState(keyIndex) != 0;
            return keyState && currentWindow.WindowState != WindowState.Minimized;
        }

        public Vector2D CalculateRelativeMousePosition() 
        {
            var windowSize = currentWindow.Size;
            var mousePosition = CalculateMousePosition();

            Vector2D resultVector = new Vector2D((mousePosition.X - (windowSize.X / 2)), (mousePosition.Y - (windowSize.Y / 2)));
            return new Vector2D(resultVector.X / 38, resultVector.Y / 30);
        }

        public Vector2D CalculateMousePosition()
        {
            _ = InteropHelper.GetCursorPos(out Point mousePoint);

            var mousePosition = new Vector2D(mousePoint.X, mousePoint.Y);
            var windowPosition = (Vector2D)(Vector2)currentWindow.Position;
            int mouseIndex = GetMouseValidIndex(mousePosition, windowPosition, currentWindow.Size.X, currentWindow.Size.Y);

            return (mousePosition - windowPosition) * mouseIndex;
        }

        public Vector2D CalculateMouseDelta(Vector2D lastPosition)
        {
            Vector2D relativePosition = CalculateRelativeMousePosition();
            var positionDifference = relativePosition - lastPosition;

            return positionDifference * MouseSensivity;
        }

        public void LockMouse() 
        {
            var windowSize = currentWindow.Size;

            int xPosition = currentWindow.Position.X + (windowSize.X / 2);
            int yPosition = currentWindow.Position.Y + (windowSize.Y / 2);
            InteropHelper.SetCursorPos(xPosition, yPosition);
        }

        private int GetMouseValidIndex(Vector2D mousePostion, Vector2D windowPosition, int width, int height)
        {
            Vector2D extendWindowPosition = windowPosition + new Vector2D(width, height);

            var vectorDifference = mousePostion - windowPosition;
            var windowDifference = extendWindowPosition - mousePostion;

            return CalculateVectorIndex(vectorDifference) * CalculateVectorIndex(windowDifference);
        }


        private int CalculateVectorIndex(Vector2 indexVector) 
        {
            var xValue = (CalculateSqrtIndex(indexVector.X) - Math.Abs(indexVector.X - 1));
            var yValue = (CalculateSqrtIndex(indexVector.Y) - Math.Abs(indexVector.Y - 1));

            int xResult = (int)(CalculateSqrtIndex(xValue));
            int yResult = (int)(CalculateSqrtIndex(yValue));

            return xResult * yResult;
        }

        private float CalculateSqrtIndex(float value) => (value + (MathF.Abs(value))) / 2;

        public bool IsInputValid(int inputKey) => inputKey > 0x0 && inputKey < 0x5;
    }
}
