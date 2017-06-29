using Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    public class TickFeed : Feed<Tick>
    {
        public void request(Request request, ITargetBlock<Tick> target)
        {
            ISourceBlock<Tick> t = new BufferBlock<Tick>();
            t.LinkTo(target);
            throw new NotImplementedException();
        }
    }
}
