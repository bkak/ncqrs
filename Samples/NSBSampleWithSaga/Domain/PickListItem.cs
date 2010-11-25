using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    internal class PickListItem 
    {
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
