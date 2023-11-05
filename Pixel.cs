using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnakeGame
{
    struct Pixel
    {
        private const char TexturePixel = '0';
        public int PixelSize { get; }
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }

        public Pixel(int x, int y, ConsoleColor color, int pixelSize = 2)
        {
            PixelSize = pixelSize;
            X = x;
            Y = y;
            Color = color;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(TexturePixel);
                }
            }
        }

        public void Clear()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
