using System;

namespace StockXing
{
    interface RecentRecord<TResult, TRequest>
    {
        DateTime getTime(TRequest request);
    }
}
