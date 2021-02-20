using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DbControl;

namespace Core.Model.Ordering
{
    [Table("Completed Order Lines")]
    public class CompletedOrderLine : IOrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Orders.Find(OrderId);
            }
        }

        public int ProductId { get; init; }

        public Product Product
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Products.Find(ProductId);
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

        public double Price { get; init; }

        //Значение берётся из прайса.

        public CompletedOrderLine()
        {
            //Price = Count * Product.Price;
        }

        public CompletedOrderLine(Product product, int count)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Param product can't be null");
            ProductId = product.Id;
            Count = count;
            Price = Count * product.Price;
        }
    }
}
