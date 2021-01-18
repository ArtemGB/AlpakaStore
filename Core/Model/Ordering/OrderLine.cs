using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Model.Ordering
{
    class OrderLine
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public string ProductName 
        {
            get => Product.Name;
            set { }
        }
        public int Count { get; set; }

        //Значение берётся из прайса.
        public double Price { get; set; }

    }
}
