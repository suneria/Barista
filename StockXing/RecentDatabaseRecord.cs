using Stock;
using Stock.Model;
using System;
using System.IO;
using System.Linq;

namespace StockXing
{
    class RecentDatabaseRecord : RecentRecord<Tick, ListedStock>
    {
        public DateTime getTime(ListedStock request)
        {
            dbml.TickRWDataContext db = new dbml.TickRWDataContext(File.ReadAllText("db.txt"));
            return db.Tick.Where(t => t.Name == request.Name).OrderByDescending(t => t.Time).Select(t => t.Time).FirstOrDefault();
        }
    }
}
