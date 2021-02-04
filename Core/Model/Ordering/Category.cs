using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Managing;

namespace Core.Model.Ordering
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category ParrentCategory { get; set; }

        public List<Category> SubCategories { get;}
        
        //TODO в метод гет поместить сериализацию подкатегорий в JSON.
        public string SubCategoriesJson { get; set; }

        public Category()
        {
            SubCategories = new List<Category>();
        }

    }
}
