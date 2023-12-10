namespace TheSnake
{
    public class GameInterface // the part other than the snake and food
    {
        //have three color scheme, each game will change one color scheme
        
        public static void DrawLetter(char letter, int startX, int startY)
        {
            Console.SetCursorPosition(startX, startY);

            string output = "";
            switch (letter)
            {
                case 'S':
                    output = "■ ■ ■\n■   \n■ ■ ■\n    ■\n■ ■ ■";
                    break;
                case 'N':
                    output = "■   ■\n■■  ■\n■ ■ ■\n■  ■■\n■   ■";
                    break;
                case 'A':
                    output = " ■■■ \n■   ■\n■■■■■\n■   ■\n■   ■";
                    break;
                case 'K':
                    output = "■  ■■\n■ ■ \n■■  \n■ ■ \n■  ■■";
                    break;
                case 'E':
                    output = "■ ■ ■\n■    \n■ ■  \n■    \n■ ■ ■";
                    break;
            }

            foreach (var line in output.Split('\n'))
            {
                Console.SetCursorPosition(startX, startY++);
                Console.Write(line);
            }
            Console.SetCursorPosition(2, 6);
            
        }
        public static void SnakeTitle()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            int startX = 3;
            int startY = 1;

            // Draw each letter
            GameInterface.DrawLetter('S', startX, startY);
            GameInterface.DrawLetter('N', startX + 7, startY);
            GameInterface.DrawLetter('A', startX + 14, startY);
            GameInterface.DrawLetter('K', startX + 21, startY);
            GameInterface.DrawLetter('E', startX + 28, startY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------");
        }
    }

    public class OuterBoundary : GameInterface
    {
        private static int _height = 30;
        private static int _width = 40;

        public static int  Height 
        { get { return _height; }}

        public static int Width
        {get { return _width; }}

        public static void DrawOutBoundary()
        {
            //Console.WindowHeight = Height;
            //Console.WindowWidth = Width;
            //Console.BufferHeight = Height;
            //Console.BufferWidth = Width;

            // Horizontal Boundary

            //               COLOR              //
            Console.ForegroundColor= ConsoleColor.Cyan;
            //               COLOR              //


            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("▀");
                Console.SetCursorPosition(i, Height - 1);
                Console.Write("▄");
            }
            // Vertical Boundary
            for (int j = 0; j < Height; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("█");
                Console.SetCursorPosition(Width - 1, j);
                Console.Write("█");
            }
        }
    }
    public class InnerBoundary : GameInterface //the game area. game will end if snake hit this boundary
    {
        private static int _distanceToTop = 13;
        private static int _distanceToBottom = 2;
        private static int _distanceToLeft = 2;
        private static int _distanceToRight = 2;

        public static int Top = _distanceToTop;
        public static int Bottom = OuterBoundary.Height - _distanceToBottom;
        public static int Left = _distanceToLeft;
        public static int Right = OuterBoundary.Width - _distanceToRight;

        private static int _height = OuterBoundary.Height-_distanceToTop-_distanceToBottom; // 25-10-2=13
        private static int _width = OuterBoundary.Width-_distanceToLeft-_distanceToRight; // 40-2-2=36
        public static void DrawInnerBoundary()
        {
            //               COLOR              //
            Console.ForegroundColor = ConsoleColor.Magenta;
            //               COLOR              //

            // Horizontal Boundary
            for (int x = _distanceToLeft; x < _width+_distanceToLeft; x++)
            {
                Console.SetCursorPosition(x, _distanceToTop-2);
                if (x==_distanceToLeft)
                    Console.WriteLine('╔');
                else if (x== OuterBoundary.Width - _distanceToRight-1)
                    Console.WriteLine('╗');
                else
                    Console.WriteLine('═');

                Console.SetCursorPosition(x, OuterBoundary.Height - _distanceToBottom);
                if (x == _distanceToLeft)
                    Console.WriteLine('╚');
                else if (x == OuterBoundary.Width - _distanceToRight - 1)
                    Console.WriteLine('╝');
                else
                    Console.WriteLine('═');
            }
            // Vertical Boundary
            for (int y = _distanceToTop-1; y < OuterBoundary.Height-_distanceToBottom; y++)
            {
                Console.SetCursorPosition(_distanceToLeft, y);
                Console.Write("║");
                Console.SetCursorPosition(OuterBoundary.Width-_distanceToRight-1, y);
                Console.Write("║");
            }
        }
        
    }
    public class Information : GameInterface //display score, time etc. some visual appealing element.
    {
        private static void DrawScore(int score)
        {
            int startX = 20; 
            int startY = 8;   
            for (int i = 0; i < score; i++)
            {
                Console.SetCursorPosition(startX + i % 17, startY + i / 17); // Adjust layout as needed
                Console.Write('■');
            }
        }
        public static void DisplayInformation(Snake snake)
        {
            var length = snake.Length;
            var time = GameRunTime.stopwatch.Elapsed.TotalSeconds;
            var speed = GameRunTime.CurrentSpeed;

            Console.SetCursorPosition(2, 7);
            Console.WriteLine(DateTime.Now.ToString("M/dd/yyyy"));
            Console.SetCursorPosition(2, 8);
            Console.WriteLine("Lenght: " + length);
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("Time: "+time.ToString("0") + 's');
            Console.SetCursorPosition(2, 10);    //clear the line
            Console.Write("             ");     //clear the line
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("Speed: "+speed);
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("Score: ");
            DrawScore(snake.Score);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(17, 7);
            Console.WriteLine("|");
            Console.SetCursorPosition(17, 8);
            Console.WriteLine("|");
            Console.SetCursorPosition(17, 9);
            Console.WriteLine("|");
            Console.SetCursorPosition(17, 10);
            Console.WriteLine("|");

        }
    }
}