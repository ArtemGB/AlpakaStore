using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Model.Ordering
{
    public class OrderLine
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public string ProductName => Product.Name;
        public int Count { get; set; }

        //Значение берётся из прайса.
        public double Price { get; }

        public OrderLine()
        {
            Price = Count * Product.Price;
        }

    }
}
