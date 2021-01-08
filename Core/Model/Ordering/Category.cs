using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Ordering
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category ParrentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        
        //TODO в метод гет поместить сериализацию Подкатегорий в JSON.
        public string SubCategoriesJSON { get; set; }

    }
}
