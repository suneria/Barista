using Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    class TickDatabase : Feed<Tick, OneDay>
    {
        public void request(OneDay request, ITargetBlock<IEnumerable<Tick>> target)
        {
            Task.Run(() =>
            {
                Thread.Sleep(6000);
                target.Post(new Tick[]
                {
                    new Tick(){ Price = 1 },
                    new Tick(){ Price = 2 },
                    new Tick(){ Price = 3 },
                    new Tick(){ Price = 4 },
                    new Tick(){ Price = 5 },
                });
            });
        }

        public void cancel()
        {
        }
    }
}
