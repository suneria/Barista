using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock
{
    public class Tick
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public long Amount { get; set; }

        public long Volume { get; set; }

        public double Rate { get; set; }
    }
}
