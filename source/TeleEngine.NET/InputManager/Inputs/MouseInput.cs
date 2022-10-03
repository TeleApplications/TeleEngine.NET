using Silk.NET.Windowing;
using System.Collections.Immutable;
using System.Drawing;
using System.Numerics;
using TeleEngine.NET.InputManager.Enums;
using TeleEngine.NET.InputManager.Interfaces;
using TeleEngine.NET.SharedObjects.Attributes;
using TeleEngine.NET.Views;

namespace TeleEngine.NET.InputManager.Inputs
{
    [Shared(typeof(MouseInput))]
    public sealed class MouseInput : IInput<MouseKeys>
    {
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

        public Vector2 CalculateRelativeMousePosition() 
        {
            var windowSize = currentWindow.Size;
            var mousePosition = CalculateMousePosition();

            Vector2 resultVector = new Vector2((mousePosition.X - (windowSize.X / 2)), (mousePosition.Y - (windowSize.Y / 2)));
            return new Vector2(resultVector.X / 38, resultVector.Y / 30);
        }

        public Vector2 CalculateMousePosition()
        {
            _ = InteropHelper.GetCursorPos(out Point mousePoint);

            var mousePosition = new Vector2(mousePoint.X, mousePoint.Y);
            var windowPosition = (Vector2)currentWindow.Position;
            int mouseIndex = GetMouseValidIndex(mousePosition, windowPosition, currentWindow.Size.X, currentWindow.Size.Y);

            return mouseIndex * (mousePosition - windowPosition);
        }

        private int GetMouseValidIndex(Vector2 mousePostion, Vector2 windowPosition, int width, int height) 
        {
            Vector2 extendWindowPosition = windowPosition + new Vector2(width, height);

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
