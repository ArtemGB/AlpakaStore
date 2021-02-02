using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Ordering
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category ParrentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        
        //TODO в метод гет поместить сериализацию подкатегорий в JSON.
        public string SubCategoriesJson { get; set; }

        public Category()
        {
            SubCategories = new List<Category>();
        }

    }
}
