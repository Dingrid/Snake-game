using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication1
{
    public enum Direction { Up, Down, Left, Right, };

    class Settings
    {
        public static int Width { get; set; }         public static int Height { get; set; }         public static int Speed { get; set; }         public static int Score { get; set; }         public static bool gameOver { get; set; }         public static Direction direction { get; set; }         public static int Points { get; set; }

        public Settings()
        {
            Width = 16; Height = 16; Speed = 5;
            Score = 0; Points = 100; direction = Direction.Down; gameOver = false;
        }
    }
}