using Stock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    public class TickFeed : Feed<Tick, OneDay>
    {
        public void request(OneDay oneDay, ITargetBlock<IEnumerable<Tick>> target)
        {
            TickDatabase db = new TickDatabase();
            TickQuery query = new TickQuery();
            TickReal real = new TickReal();
            switch (whatNeeded())
            {
                case Need.Past:
                    break;
                case Need.Present:
                    var realBuffer = new BufferBlock<IEnumerable<Tick>>();
                    real.request(oneDay, realBuffer);
                    var queryLayerAction = new ActionBlock<Tuple<IEnumerable<Tick>, IEnumerable<Tick>>>(dbAndQueryTicks =>
                    {
                        target.Post(dbAndQueryTicks.Item1);
                        target.Post(dbAndQueryTicks.Item2);
                        // Completion
                        realBuffer.LinkTo(target);
                    });
                    //
                    var dbAndQueryJoin = new JoinBlock<IEnumerable<Tick>, IEnumerable<Tick>>();
                    dbAndQueryJoin.LinkTo(queryLayerAction);
                    db.request(oneDay, dbAndQueryJoin.Target1);
                    query.request(oneDay, dbAndQueryJoin.Target2);
                    break;
                case Need.Future:
                    // Real의 결과를 target으로 넣는다.
                    real.request(oneDay, target);
                    break;
            }
        }

        private Need whatNeeded()
        {
            return Need.Present;
        }
    }
}
