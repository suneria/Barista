using Stock;
using Stock.Model;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    public class TickFeed : Feed<Tick, OneDay>
    {
        public TickFeed(RecentRecord<Tick> recentRecord)
        {
            RecentRecord = recentRecord;
        }

        public override void request(OneDay oneDay)
        {
            TickDatabase db = new TickDatabase();
            TickQuery query = new TickQuery();
            TickReal real = new TickReal();
            switch (whatNeeded())
            {
                case Need.Past:
                    break;
                case Need.PastAndFuture:
                    Tick lastTick = null;
                    var filter = new ActionBlock<Tick>(async tick =>
                    {
                        if (lastTick == null || lastTick.Time < tick.Time || (lastTick.Time == tick.Time && lastTick.Volume < tick.Volume))
                        {
                            //
                            Buffer.Post(tick);
                            await Buffer.SendAsync(tick);
                        }
                        lastTick = tick;
                    });
                    db.Target = filter;
                    db.Completion.ContinueWith(a =>
                    {
                        query.Target = filter;
                        query.Completion.ContinueWith(b =>
                        {
                            real.Target = filter;
                        });
                    });
                    var since = new Since { Stock = oneDay.Stock, After = RecentRecord.getTime(oneDay.Stock), Base = oneDay.Date };
                    real.request(oneDay);
                    query.request(since);
                    db.request(oneDay);
                    break;
                case Need.Future:
                    // Real의 결과를 target으로 넣는다.
                    real.request(oneDay);
                    break;
            }
        }

        public override void cancel()
        {
        }

        private Need whatNeeded()
        {
            return Need.PastAndFuture;
        }

        private RecentRecord<Tick> RecentRecord { get; set; }
    }
}
