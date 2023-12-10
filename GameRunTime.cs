using System.Diagnostics;

namespace TheSnake
{
    //1.Scene Render 2.Console setting
    public class GameRunTime
    {
        public static Stopwatch stopwatch = new Stopwatch();
        public enum GameSpeed
        {
            Slow = 100, Medium = 60, Fast   = 40, Super  = 30
        }
        public static bool GameRunning { get; set; }
        public static GameSpeed CurrentSpeed { get; set; } = GameSpeed.Slow;
        public static void ChangeSpeed()
        {
            switch (CurrentSpeed)
            {
                case GameSpeed.Medium:
                    CurrentSpeed = GameSpeed.Fast; break;
                case GameSpeed.Fast:
                    CurrentSpeed = GameSpeed.Super; break;
                case GameSpeed.Super:
                    CurrentSpeed = GameSpeed.Slow; break;
                case GameSpeed.Slow:
                    CurrentSpeed = GameSpeed.Medium; break;
            }
        }

        //loading GameInterface

        //SnakeMove
        public static void GameRun()
        {
            Console.CursorVisible = false;
            while (true)
            {
                GameRunning = true;
                stopwatch.Restart();
                var snake = new Snake();
                var food = new Food();

                snake.OnEat += food.RefreshFood;

                OuterBoundary.SnakeTitle();
                OuterBoundary.DrawOutBoundary();
                InnerBoundary.DrawInnerBoundary();

                snake.RenderSnake();
                food.RenderFood();

                //Test if ChangeSpeed (if the ctr button was pressed)

                while (GameRunning = true)
                {
                    Information.DisplayInformation(snake);
                    snake.MovingDirection();

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Spacebar)
                            ChangeSpeed();
                        else
                            snake.PressButton(key);
                    }
                    if (snake.IsGameOver)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(4, 17);
                        Console.WriteLine("--------------------------------");
                        Console.SetCursorPosition(15, 18);
                        Console.WriteLine("GAME OVER!");
                        Console.SetCursorPosition(7, 19);

                        Console.WriteLine("PRESS ANY KEY TO CONTINUE");
                        Console.SetCursorPosition(6, 20);

                        Console.WriteLine("PRESS SPACE TO CHANGE SPEED");
                        Console.SetCursorPosition(10, 21);

                        Console.WriteLine("PRESS ESC TO EXIT");

                        Console.SetCursorPosition(4, 22);
                        Console.WriteLine("--------------------------------");

                        var key = Console.ReadKey();
                        if(key.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                        Console.Clear();
                        break;
                    }
                    snake.RenderSnake();
                    snake.CheckFood(food);
                    Thread.Sleep((int)CurrentSpeed);
                }
            }
        }
    }
}