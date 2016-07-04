using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Item
    {
        public decimal weight { get; set;}
        public decimal value { get; set; }
        public Item(decimal weight, decimal value)
        {
            this.weight = weight;
            this.value = value;
        }
    }
}
