using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;


namespace Core.Model.Ordering
{
    public class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product
        {
            get
            {
                using (StoreDbContext dbContext = new StoreDbContext())
                {
                    return dbContext.Products.Find(ProductId);
                }
            }
        }
        public string ProductName => Product.Name;

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                if (value <= 0) throw new ArgumentException("Count can't be less 0");
                _count = value;
            }
        }

        //Значение берётся из прайса.
        public double Price { get; }

        public OrderLine()
        {
            Price = Count * Product.Price;
        }

        public OrderLine(Product product, int count)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Count = count;
            Price = Count * Product.Price;
        }

    }
}
