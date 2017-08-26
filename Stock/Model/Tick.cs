using System;

namespace Stock.Model
{
    public class Tick
    {
        public DateTime Time { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public long Amount { get; set; }

        public long Volume { get; set; }

        public double Rate { get; set; }
    }
}
