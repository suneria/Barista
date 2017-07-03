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
    class TickReal : Feed<Tick, OneDay>
    {
        public void request(OneDay request, ITargetBlock<IEnumerable<Tick>> target)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    target.Post(new Tick[]
                    {
                    new Tick(){ Price = 11 },
                    });
                }
            });
        }
    }
}
