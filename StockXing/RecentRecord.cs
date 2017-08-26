using Stock;
using System;

namespace StockXing
{
    public interface RecentRecord<TType>
    {
        DateTime getTime(ListedStock stock);
    }
}
