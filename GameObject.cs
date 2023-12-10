namespace TheSnake
{
    public class Snake 
    {
        public bool IsGameOver {  get; private set; }
        public LinkedList<(int X, int Y)> Body { get; set; } // This X and Y can be get in the future
        public int Length { get { return Body.Count; } }
        public enum MoveDirection
        {
            Up, Down, Left, Right
        }

        public int Score { get; private set; } = 0;

        
        public Snake()
        {
            Body = new LinkedList<(int X, int Y)> ();
            int startX = 10;
            int startY = 25;

            Body.AddFirst((startX, startY));
            Body.AddLast((startX, startY + 1));
            Body.AddLast((startX, startY+2));

            Direction = MoveDirection.Up;
            icon = '■';
        }

        public MoveDirection Direction { get; set; }

        public event EventHandler OnEat;//eat food, the food will refresh
        public char icon { get; set; }

        public delegate (int X, int Y) GetTail();
        public void ChangeDirection(MoveDirection newDirection) //EventHandler of Control
        {
            this.Direction = newDirection;
        }
        public void Grow()
        {
            var tail = Body.Last.Value;
            Body.AddLast (tail);
        }

        public void MovingDirection()
        {
            var head = Body.First.Value;
            (int x , int y) newHead = head; //(This is a tuple)
            switch (Direction)
            {
                case MoveDirection.Up:
                    newHead = (head.X, head.Y - 1);
                    break;
                case MoveDirection.Down:
                    newHead = (head.X, head.Y + 1);
                    break;
                case MoveDirection.Left:
                    newHead = (head.X - 1, head.Y);
                    break;
                case MoveDirection.Right:
                    newHead = (head.X + 1, head.Y);
                    break;
            }
            
            if (Body.Skip(1).Any(segment => segment == newHead)
            || newHead.x < InnerBoundary.Left+1 || newHead.x >= InnerBoundary.Right-1
            || newHead.y < InnerBoundary.Top-1 || newHead.y >= InnerBoundary.Bottom)
            {
                IsGameOver = true; 
                return;
            }
            if(this.Score >=51)
            {
                IsGameOver = true;
                return;
            }
            Body.RemoveLast();
            Body.AddFirst(newHead);
        }

        public void PressButton(ConsoleKeyInfo button)
        {
            switch (button.Key)
            {
                case ConsoleKey.UpArrow:
                    this.ChangeDirection(MoveDirection.Up); break;
                case ConsoleKey.DownArrow:
                    this.ChangeDirection(MoveDirection.Down); break;
                case ConsoleKey.LeftArrow:
                    this.ChangeDirection(MoveDirection.Left); break;
                case ConsoleKey.RightArrow:
                    this.ChangeDirection(MoveDirection.Right); break;
            }
        }

        public void CheckFood(Food food)
        {
            if (Body.First.Value == food.Position)
            {
                OnEat(this, EventArgs.Empty);
                Score++;
            }
        }
        public void RenderSnake()
        {
            //     COLOR    //
            Console.ForegroundColor = ConsoleColor.White;
            //     COLOR    //
            
            var tail = Body.Last.Value;
            foreach (var item in Body)
            {
                Console.SetCursorPosition(item.X,item.Y);
                Console.Write(icon);
            }
            Console.SetCursorPosition(tail.X, tail.Y);
            Console.Write(' ');
        }
    }

    public class Food
    {
        public (int X, int Y) Position { get; set; }
        public Food()
        {
            Position = (10,15);
            RenderFood();
        }

        public void RefreshFood(Object snake, EventArgs e)
        {
            Snake snake1 = (Snake)snake;
            snake1.Grow();

            var x = new Random();
            var y = new Random();
            int FoodX=0, FoodY=0;
            do
            {
                FoodX = x.Next(InnerBoundary.Left + 2, InnerBoundary.Right - 2);
                FoodY = y.Next(InnerBoundary.Top + 2, InnerBoundary.Bottom - 2);
            }
            while 
            (snake1.Body.Any(segment => segment.X == FoodX && segment.Y == FoodY));
            Position = (FoodX, FoodY);
            RenderFood();
        }
        public void RenderFood()
        {
            //     COLOR     //
            Console.ForegroundColor = ConsoleColor.White;
            //     COLOR     //
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write('Φ');
        }
    }
}