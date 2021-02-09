using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Ordering;

namespace Core.Model.Managing
{
    public class ProductManager
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly StoreDbContext _dbContext;

        /// <summary>
        /// Товары.
        /// </summary>
        public List<Product> Products
        {
            get => _dbContext.Products.ToList();
        }

        public ProductManager()
        {
            _dbContext = new StoreDbContext();
        }

        public void AddProduct(string name, string description, int price, Filter filter, Category category)
    }
}
