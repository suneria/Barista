using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockXing
{
    public class InBlock
    {
        public string Name { get; set; }
        public IEnumerable<InField> InFields { get; set; }
    }

    public class InField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class OutBlocks : List<OutBlock>
    {
    }

    public class OutBlock
    {
        public string Name { get; set; }
        public IEnumerable<OutField> OutFields { get; set; }
    }

    public class OutField
    {
        public string Name { get; set; }
    }

    public class RawData
    {
        public string[] Values
        {
            get
            {
                return _values;
            }
        }

        private string[] _values = new string[32];
    }
}
