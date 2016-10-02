using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Timer
    {
        public int Timeout { get; set; }
        public Action Tick { get; set; }

        private Thread thread;
        private bool forceStop = false;

        private void Work()
        {
            while (true)
            {
                Thread.Sleep(Timeout);
                Tick();

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
            forceStop = true;
        }
    }
}
