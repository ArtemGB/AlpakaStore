using Core.DbControl;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Ordering
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int FilterId { get; set; }

        public Filter Filter
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Filters.Find(FilterId);
            }
        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Price { get; set; }
    }
}
