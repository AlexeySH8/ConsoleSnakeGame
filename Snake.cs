using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnakeGame
{
    class Snake
    {
        private ConsoleColor HeadColor = ConsoleColor.Red;
        private ConsoleColor BodyColor = ConsoleColor.Yellow;
        public Pixel Head = new Pixel();
        public Queue<Pixel> Body = new Queue<Pixel>();


        public Snake(int x, int y, int bodyLemgth = 3)
        {
            Head = new Pixel(x, y, HeadColor);
            for (int i = bodyLemgth; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, Head.Y, BodyColor));
            }
            Draw();
        }

        public void Move(Direction direction, bool eat)
        {
            Clear();
            Body.Enqueue(new Pixel(Head.X, Head.Y, BodyColor));
            if (!eat)
                Body.Dequeue();

            switch (direction)
            {
                case Direction.Right:
                    Head = new Pixel(Head.X + 1, Head.Y, HeadColor);
                    break;
                case Direction.Left:
                    Head = new Pixel(Head.X - 1, Head.Y, HeadColor);
                    break;
                case Direction.Up:
                    Head = new Pixel(Head.X, Head.Y - 1, HeadColor);
                    break;
                case Direction.Down:
                    Head = new Pixel(Head.X, Head.Y + 1, HeadColor);
                    break;
            }
            Draw();
        }

        public void Draw()
        {
            Head.Draw();
            foreach (Pixel part in Body)
            {
                part.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();
            foreach (Pixel part in Body)
            {
                part.Clear();
            }
        }
    }
}
