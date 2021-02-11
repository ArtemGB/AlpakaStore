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


        /// <summary>
        /// Добавление нового продукта.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="description">Описание</param>
        /// <param name="price">Цена</param>
        /// <param name="filter">Фильтры</param>
        /// <param name="category">Категория</param>
        /// <returns></returns>
        public Product AddProduct(string name, string description, int price, Filter filter, Category category)
        {
            try
            {
                Product newProduct = new Product()
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Filter = filter,
                    Category = category,
                };
                _dbContext.Add(newProduct);
                _dbContext.SaveChanges();
                return newProduct;
            }
            catch (Exception e)
            {
                throw new Exception("Product adding error. Watch inner exceptions.");
            }

        }

        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (_dbContext.Products.Any(p => p.Id == product.Id))
                _dbContext.Products.Remove(product);
            else
                throw new ArgumentException("Products doesn't contains this product", nameof(product));
        }

        /// <summary>
        /// Удаление продукта по Id.
        /// </summary>
        /// <param name="productId">Id продукта.</param>
        public void RemoveProduct(int productId)
        {
            Product product = _dbContext.Products.Find(productId);
            if (product == null)
                throw new ArgumentException($"There is no product with id = {productId}");
            _dbContext.Remove(product);
        }
    }
}
