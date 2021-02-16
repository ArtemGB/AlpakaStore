using System.ComponentModel.DataAnnotations.Schema;
using Core.DbControl;
using Core.Model.PriceCalculation;

namespace Core.Model.Ordering
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public  int FilterId { get; set; }

        public Filter Filter


        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Price { get; set; }
    }
}
