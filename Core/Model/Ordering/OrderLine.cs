using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Model.Ordering
{
    class OrderLine
    {
        public Order Order { get; set; }
        public int Id { get; set; }
        public Product Product { get; set; }
        public string ProductName 
        {
            get => Product.Name;
            set { }
        }
        public int Count { get; set; }

    }
}
