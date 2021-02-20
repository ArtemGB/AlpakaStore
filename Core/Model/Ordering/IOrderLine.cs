namespace Core.Model.Ordering
{
    public interface IOrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; }
        public int ProductId { get; init; }
        public Product Product { get; }
        public int Count { get; set; }
        public double Price { get; init; }
    }
}