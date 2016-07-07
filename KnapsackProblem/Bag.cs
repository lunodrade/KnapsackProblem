using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Bag
    {
        public string chromosomes { get; set; }
        public decimal fitness { get; set; }
        public decimal peso { get; set; }
        public bool overflow { get; set; }
        public Bag(string chromosomes, decimal fitness, decimal peso, bool overflow)
        {
            this.chromosomes = chromosomes;
            this.fitness = fitness;
            this.peso = peso;
            this.overflow = overflow;
        }
    }
}
