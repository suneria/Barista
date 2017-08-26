using Stock;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace StockXing
{
    class TickDatabase : Feed<Tick, OneDay>
    {
        public override void request(OneDay request)
        {
            StockXing.dbml.TickRWDataContext db = new StockXing.dbml.TickRWDataContext(File.ReadAllText("db.txt"));
            IEnumerable<StockXing.dbml.Tick> dbTicks = db.Tick.Where(tick => tick.Name == request.Stock.Name && tick.Time.Date == request.Date);
            foreach (Tick tick in toPrimitive(dbTicks))
            {
                //
                Buffer.Post(tick);
            }
            Buffer.Complete();
        }

        public override void cancel()
        {
        }

        private IEnumerable<Tick> toPrimitive(IEnumerable<StockXing.dbml.Tick> persistentTicks)
        {
            IEnumerator<StockXing.dbml.Tick> enumerator = persistentTicks.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return new Tick
                {
                    Name = enumerator.Current.Name,
                    Time = enumerator.Current.Time,
                    Price = enumerator.Current.Price,
                    Quantity = enumerator.Current.Quantity,
                    Rate = enumerator.Current.Rate,
                    Amount = enumerator.Current.Amount,
                    Volume = enumerator.Current.Volume,
                };
            }
        }
    }
}
