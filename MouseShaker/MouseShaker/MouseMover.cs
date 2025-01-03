using System.Runtime.InteropServices;

namespace MouseShaker
{
    public static class MouseMover
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public static void ShakeMouse(int shakeDistance, int durationMilliseconds)
        {
            int startX = Cursor.Position.X;
            int startY = Cursor.Position.Y;

            int elapsedTime = 0;
            int stepDuration = 100; // Milliseconds per shake step

            while (elapsedTime < durationMilliseconds)
            {
                SetCursorPos(startX + shakeDistance, startY);
                Thread.Sleep(stepDuration / 2);
                SetCursorPos(startX - shakeDistance, startY);
                Thread.Sleep(stepDuration / 2);
                elapsedTime += stepDuration;
            }

            // Return to the original position
            SetCursorPos(startX, startY);
        }
    }
}