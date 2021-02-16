using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.DbControl;

namespace Core.Model.Ordering
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category ParentCategory { get; set; }

        [NotMapped]
        public List<Category> SubCategories
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Categories.Where(c => c.ParentCategory.Id == Id).ToList();
            }
        }

        //TODO в метод гет поместить сериализацию подкатегорий в JSON.
        public string SubCategoriesJson { get; set; }

        public Category()
        {
        }

    }
}
