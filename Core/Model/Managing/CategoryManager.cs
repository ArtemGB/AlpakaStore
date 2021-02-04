using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Ordering;

namespace Core.Model.Managing
{
    public class CategoryManager
    {
        /// <summary>
        /// Список всех категорий.
        /// </summary>
        public List<Category> Categories
        {
            get
            {
                using (StoreDbContext dbContext = new StoreDbContext())
                {
                    return dbContext.Categories.ToList();
                }
            }
            set => Categories = value;
        }

        public void AddCategory(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                dbContext.Categories.Add(new Category() { Name = name, ParrentCategory = null });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет подкатегорию категории.
        /// </summary>
        /// <param name="name">Имя категории</param>
        /// <param name="parrentCategory">Родительская категория</param>
        public void AddSubCategory(string name, Category parrentCategory)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");
            if (parrentCategory == null)
                throw new ArgumentNullException("parrentCategory");
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                dbContext.Categories.Add(new Category() { Name = name, ParrentCategory = parrentCategory });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет подкатегорию категории.
        /// </summary>
        /// <param name="name">Имя категории</param>
        /// <param name="parrentCategoryId">Id родительской категории</param>
        public void AddSubCategory(string name, int parrentCategoryId)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");

            using (StoreDbContext dbContext = new StoreDbContext())
            {
                var parrentCategory = dbContext.Categories.Find(parrentCategoryId);
                if (parrentCategory == null)
                    throw new ArgumentException($"There is no category with id = {parrentCategoryId}");
                dbContext.Categories.Add(new Category() { Name = name, ParrentCategory = parrentCategory });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="categoryId"></param>
        public void RemoveCategory(int categoryId)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                var category = dbContext.Categories.Find(categoryId);
                if (category != null)
                    dbContext.Categories.Remove(category);
                else
                    throw new ArgumentException($"There is no category with Id ={categoryId}");
            }
        }

        //TODO
        public void EditCategory(int categoryId, string name = null, Category parrentCategory = null, bool resetParrentCategory = false)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                var category = dbContext.Categories.Find(categoryId);
                if (category != null)
                {
                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(name))
                        if (category.Name != name)
                            category.Name = name;

                    if (parrentCategory != null && resetParrentCategory)
                        category.ParrentCategory = parrentCategory;

                    //Проверка наличия родительской категории.

                    dbContext.SaveChanges();
                }
                else
                    throw new ArgumentException($"There is no category with Id ={categoryId}");
            }
        }



    }
}
