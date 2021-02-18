using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Ordering;

namespace Core.Managing
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
        }


        /// <summary>
        /// Добавление новой категории.
        /// </summary>
        /// <param name="name">Название категории</param>
        public void AddCategory(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                dbContext.Categories.Add(new Category() { Name = name, ParentCategory = null });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет подкатегорию категории.
        /// </summary>
        /// <param name="name">Название категории</param>
        /// <param name="parentCategory">Родительская категория</param>
        public void AddSubCategory(string name, Category parentCategory)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");
            if (parentCategory == null)
                throw new ArgumentNullException(nameof(parentCategory));
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                dbContext.Categories.Add(new Category() { Name = name, ParentCategory = parentCategory });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет подкатегорию категории.
        /// </summary>
        /// <param name="name">Имя категории</param>
        /// <param name="parentCategoryId">Id родительской категории</param>
        public void AddSubCategory(string name, int parentCategoryId)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter name is null or white space.");

            using (StoreDbContext dbContext = new StoreDbContext())
            {
                var parentCategory = dbContext.Categories.Find(parentCategoryId);
                if (parentCategory == null)
                    throw new ArgumentException($"There is no category with id = {parentCategoryId}");
                dbContext.Categories.Add(new Category() { Name = name, ParentCategory = parentCategory });
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

        /// <summary>
        /// Редактирование категории
        /// </summary>
        /// <param name="categoryId">Id категории для редактирования</param>
        /// <param name="name">Новое имя</param>
        /// <param name="parentCategory">Новая родительская категория. default = null</param>
        public void EditCategory(int categoryId, string name = null, Category parentCategory = null)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                var category = dbContext.Categories.Find(categoryId);
                if (category != null)
                {
                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(name))
                        if (category.Name != name)
                            category.Name = name;

                    if (parentCategory != null)
                    {
                        var parCategory = dbContext.Categories.Find(parentCategory.Id);
                        if (parCategory != null)
                            category.ParentCategory = parCategory;
                        else
                            throw new ArgumentException("Не существует такой родительской категории.",
                                nameof(parentCategory));
                    }
                    dbContext.SaveChanges();
                }
                else
                    throw new ArgumentException($"There is no category with Id = {categoryId}");
            }
        }



    }
}
