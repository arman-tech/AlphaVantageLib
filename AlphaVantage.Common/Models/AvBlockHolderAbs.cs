using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models
{
    public abstract class AvBlockHolderAbs<T>
    {
        // the name of the block holder
        public string Name { get; set; }

        public IEnumerable<T> Blocks { get; set; }
    }
}
