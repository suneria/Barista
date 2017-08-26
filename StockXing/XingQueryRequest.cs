using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockXing
{
    class XingQueryRequest
    {
        public void invalidateXingQuery()
        {
            InvalidEventHandler.Invoke();
        }

        public void requestXingQuery()
        {
            RequestableEventHandler.Invoke();
        }

        public Requestable RequestableEventHandler { get; set; }
        public delegate void Requestable();

        public Invalid InvalidEventHandler { get; set; }
        public delegate void Invalid();
    }
}
