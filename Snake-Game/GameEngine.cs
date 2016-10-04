using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace SnakeGame
{
    public class GameEngine
    {
        public int ArenaSize { get; set; }

        private List<Point> snake = new List<Point>();
        private int direction = 0;

        static Random rnd = new Random();
        private Point food = new Point(rnd.Next(1,19), rnd.Next(1,19));

        
        private void RenderArena()
        {
            Console.Clear();

            if (snake.Contains(new Point(food.X, food.Y)))
            {
                food = new Point(rnd.Next(1, 19), rnd.Next(1, 19));
            }
            
            for (int i = 0; i < ArenaSize+2; i++)
            {
                
                for (int j = 0; j < ArenaSize+2; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == ArenaSize+1) || (i >= 0 && j == 0) || (i >= 0 && j == ArenaSize+1))
                    {
                        Console.Write("|");

                    }else if (i == 0 && j > 0 && j < ArenaSize+1 || i == ArenaSize+1 && j > 0 && j < ArenaSize+1)
                    {
                        Console.Write("-");
                    }
                    else if (snake.Contains(new Point(i, j)))
                    {
                        Console.Write("*");

                    }
                    else if (food.X == i && food.Y == j)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.CursorVisible = false;
                }
                Console.WriteLine();
            }
        }
        
        
        private void MakeMove()
        {
            Point p = snake[snake.Count - 1];
            snake.RemoveAt(0);
            
            if (direction == 0)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snake[i].X == p.X && snake[i].Y == p.Y+1)
                    {
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Death.wav";
                        player.PlaySync();

                        Environment.Exit(-1);
                    }
                }
                snake.Add(new Point(p.X, p.Y + 1));
            }  
            else if (direction == 1)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snake[i].X == p.X+1 && snake[i].Y == p.Y)
                    {
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Death.wav";
                        player.PlaySync();

                        Environment.Exit(-1);
                    }
                }

                snake.Add(new Point(p.X + 1, p.Y));
            } 
            else if (direction == 2)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snake[i].X == p.X && snake[i].Y == p.Y - 1)
                    {
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Death.wav";
                        player.PlaySync();

                        Environment.Exit(-1);
                    }
                }

                snake.Add(new Point(p.X, p.Y - 1));
            }
            else if (direction == 3)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    if (snake[i].X == p.X-1 && snake[i].Y == p.Y)
                    {
                        SoundPlayer player = new SoundPlayer();
                        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Death.wav";
                        player.PlaySync();

                        Environment.Exit(-1);
                    }
                }

                snake.Add(new Point(p.X - 1, p.Y));
            }
            

            if (snake[snake.Count - 1].X == food.X && snake[snake.Count - 1].Y == food.Y)
            {
                snake.Add(new Point(food.X, food.Y));
                System.Console.Beep(1000,50);
            }
            
            if (snake[snake.Count - 1].X < 1 || snake[snake.Count - 1].X > ArenaSize || snake[snake.Count - 1].Y < 1 || snake[snake.Count - 1].Y > ArenaSize)
            {
                SoundPlayer player = new SoundPlayer();
                player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Death.wav";
                player.PlaySync();

                Environment.Exit(-1);
                
            }
            RenderArena();
        }
        private void ChangeDirection(int direction)
        {
            this.direction = direction;
        }
        
        public void Run()
        {
            ArenaSize = 20;

            snake.Add(new Point(1, 1));
            snake.Add(new Point(1, 2));
            snake.Add(new Point(1, 3));

            Timer t = new Timer();
            t.Timeout = 200;
            t.Tick += new Action(MakeMove);

            MovementManager movementMgr = new MovementManager();
            movementMgr.MoveMade += new Action<int>(ChangeDirection);

            movementMgr.Start();
            t.Start();
        }
    }
}
