using Stock;
using Stock.Model;
using System;
using System.IO;
using System.Linq;

namespace StockXing
{
    public class RecentDatabaseRecord : RecentRecord<Tick>
    {
        public DateTime getTime(ListedStock stock)
        {
            using (dbml.TickRWDataContext db = new dbml.TickRWDataContext(File.ReadAllText("db.txt")))
            {
                return db.Tick.Where(t => t.Name == stock.Name).OrderByDescending(t => t.Time).Select(t => t.Time).FirstOrDefault();
            }
        }
    }
}
