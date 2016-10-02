using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace SnakeGame
{
    public class MovementManager
    {
        public Action<int> MoveMade;

        private int direction = 0;
        private Thread thread;
        private bool forceStop = false;

        private void Work()
        {
            while (true)
            {
                ConsoleKeyInfo move = Console.ReadKey(true);
                int direction = 0;
                if (move.Key == ConsoleKey.RightArrow)
                    direction = 0;
                else if (move.Key == ConsoleKey.DownArrow)
                    direction = 1;
                else if (move.Key == ConsoleKey.LeftArrow)
                    direction = 2;
                else if (move.Key == ConsoleKey.UpArrow)
                    direction = 3;

                MoveMade(direction);

                if (forceStop)
                    break;
            }
        }
        public void Start()
        {
            thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
            forceStop = true;
        }
    }
}
