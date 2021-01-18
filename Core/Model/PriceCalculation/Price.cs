using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Ordering;

namespace Core.Model.PriceCalculation
{
    public class Price
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public double Cost { get; set; }
    }
}
