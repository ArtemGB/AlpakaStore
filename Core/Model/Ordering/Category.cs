using System.Collections.Generic;

namespace Core.Model.Ordering
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category ParentCategory { get; set; }

        public List<Category> SubCategories { get; }

        //TODO в метод гет поместить сериализацию подкатегорий в JSON.
        public string SubCategoriesJson { get; set; }

        public Category()
        {
            SubCategories = new List<Category>();
        }

    }
}
