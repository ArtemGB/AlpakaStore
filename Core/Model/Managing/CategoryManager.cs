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

        public void AddCategory(string name, Category parrentCategory = null)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                dbContext.Categories.Add(new Category() {Name = name});
                dbContext.SaveChanges();
            }
        }
    }
}
