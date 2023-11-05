using static System.Console;

namespace MySnakeGame
{
    class Game
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;
        private const int ScreenWidth = MapWidth * 2;
        private const int ScreenHeight = MapHeight * 2;
        private static int Score = 0;
        private const ConsoleColor BorderColor = ConsoleColor.DarkGray;
        private const ConsoleColor FoodColor = ConsoleColor.Green;
        private static int DifficultyGame = 200;
        private static Random Random = new Random();

        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;
            DifficultyGame = StartMenu();
            while (true)
            {
                Start();
                Clear();
                SetCursorPosition(ScreenWidth / 3, ScreenHeight / 3);
                Write(@$"    GAME OVER!
                        YOUR SCORE - {Score}");
                Task.Run(() => Beep(400, 800));
                Thread.Sleep(3500);
            }
        }

        public static void Start()
        {
            Clear();
            CreateBorder(MapWidth, MapHeight);
            var snake = new Snake(10, 10);
            var currentDirection = Direction.Right;
            bool eat = false;
            var food = GenerationFood(snake);
            food.Draw();
            while (true)
            {
                if (snake.Head.X > MapWidth - 2 || snake.Head.Y > MapHeight - 2 ||
                    snake.Head.X < 1 || snake.Head.Y < 1 ||
                    IsSomthingInBodySnake(snake.Head.X, snake.Head.Y, snake))
                    break;
                Thread.Sleep(DifficultyGame);
                currentDirection = ReadMove(currentDirection);
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    Task.Run(() => Beep(500, 500));
                    snake.Move(currentDirection, true);
                    food = GenerationFood(snake);
                    Score++;
                }
                food.Draw();
                snake.Move(currentDirection, false);
            }
        }

        static Direction ReadMove(Direction currentDirection)
        {

            if (!KeyAvailable) return currentDirection;

            ConsoleKey key = ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    if (currentDirection != Direction.Left)
                        currentDirection = Direction.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentDirection != Direction.Right)
                        currentDirection = Direction.Left;
                    break;
                case ConsoleKey.UpArrow:
                    if (currentDirection != Direction.Down)
                        currentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (currentDirection != Direction.Up)
                        currentDirection = Direction.Down;
                    break;
            }
            return currentDirection;
        }

        public static Pixel GenerationFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(Random.Next(2, MapWidth - 2),
                    Random.Next(2, MapHeight - 2), FoodColor);
            }
            while (food.X == snake.Head.X && food.Y == snake.Head.Y ||
                IsSomthingInBodySnake(food.X, food.Y, snake));

            return food;
        }

        public static bool IsSomthingInBodySnake(int x, int y, Snake snake)
        {
            foreach (var partBody in snake.Body)
            {
                if (partBody.X == x && partBody.Y == y)
                    return true;
            }
            return false;
        }

        public static void CreateBorder(int wigth, int height)
        {
            for (int i = 0; i < wigth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, height - 1, BorderColor).Draw();
            }

            for (int i = 0; i < height; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(wigth - 1, i, BorderColor).Draw();
            }
        }

        public static int StartMenu()
        {
            SetCursorPosition(ScreenWidth / 4, ScreenHeight / 3);
            WriteLine(@"CHOOSE GAME DIFFICULTY 
                     500 - EASY
                     200 - MEDIUM
                     50 - HARD");
            SetCursorPosition(ScreenWidth / 3, ScreenHeight / 2);
            return int.Parse(ReadLine());
        }
    }
}