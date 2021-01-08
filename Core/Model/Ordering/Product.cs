using Core.Model.PriceCalculation;

namespace Core.Model.Ordering
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Filter Filter { get; set; }
        public Category Category { get; set; }
        public Price Price { get; set; }
    }
}
