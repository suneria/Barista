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
    class TickQuery : Feed<Tick, OneDay>
    {
        public void request(OneDay request, ITargetBlock<IEnumerable<Tick>> target)
        {
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                target.Post(new Tick[]
                {
                    new Tick(){ Price = 6 },
                    new Tick(){ Price = 7 },
                    new Tick(){ Price = 8 },
                    new Tick(){ Price = 9 },
                    new Tick(){ Price = 10 },
                });
            });
        }

        public void cancel()
        {
        }
    }
}
