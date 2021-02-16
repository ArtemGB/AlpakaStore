using Core.DbControl;
using Core.Model.Ordering;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            if (IsAddProductParamsCorrect(name, description, price, filter, category))
                try
                {
                    Product newProduct = new Product()
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                        FilterId = filter.Id,
                       // Filter = filter,
                        CategoryId = category.Id,
                       // Category = category,
                    };
                   // _dbContext.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Categories] ON");
                    _dbContext.Add(newProduct);
                    _dbContext.SaveChanges();
                    //_dbContext.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Categories] OFF");
                    return newProduct;
                }
                catch (Exception e)
                {
                    throw new Exception("Product adding error. Watch inner exceptions.", e);
                }

            return null;
        }


        /// <summary>
        /// Проверка параметров добавления продукта.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="filter"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private bool IsAddProductParamsCorrect(string name, string description, int price, Filter filter, Category category)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                throw new ArgumentException("Incorrect param name");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Incorrect param description");
            if (price <= 0)
                throw new ArgumentException("Incorrect param price");
            if (filter != null)
            {
                if (!_dbContext.Filters.Any(f => f.Id == filter.Id))
                    throw new ArgumentException($"There is no filter with id = {filter.Id}");
            }
            else throw new ArgumentNullException(nameof(filter));

            if (category != null)
            {
                if (!_dbContext.Categories.Any(c => c.Id == category.Id))
                    throw new ArgumentException($"There is no filter with id = {category.Id}");
            }
            else throw new ArgumentNullException(nameof(category));

            return true;
        }

        /// <summary>
        /// Удаление продукта.
        /// </summary>
        /// <param name="product">Продукт, который надо удалить.</param>
        public void RemoveProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (_dbContext.Products.Any(p => p == product))
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
                product.FilterId = filter.Id;

            if (product.Category != category && category != null)
                product.CategoryId = category.Id;
        }
    }
}
