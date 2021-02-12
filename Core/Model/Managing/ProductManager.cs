using Core.DbControl;
using Core.Model.Ordering;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <returns>Созданный продукт</returns>
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
        /// <param name="product">Продукт, который надо удалить.</param>
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

        /// <summary>
        /// Редактирование продукта.
        /// </summary>
        /// <param name="productId">Id продукта</param>
        /// <param name="name">Новое имя</param>
        /// <param name="description">Новое описание</param>
        /// <param name="price">Новая цена</param>
        /// <param name="filter">Новый фильтр</param>
        /// <param name="category">Новая категория</param>
        public void EditProduct(int productId, string name = "", string description = "", int price = -1, Filter filter = null, Category category = null)
        {
            Product product = _dbContext.Products.Find(productId);
            if (product == null)
                throw new ArgumentException($"There is no product with id = {productId}");
            if (product.Name != name && !String.IsNullOrEmpty(name) && !String.IsNullOrWhiteSpace(name))
                product.Name = name.Trim();

            if (product.Description != description && !String.IsNullOrEmpty(description) && !String.IsNullOrWhiteSpace(description))
                product.Description = description.Trim();

            if (product.Price != price && price >= 0)
                product.Price = price;

            if (product.Filter != filter && filter != null)
                product.Filter = filter;

            if (product.Category != category && category != null)
                product.Category = category;
        }
    }
}
