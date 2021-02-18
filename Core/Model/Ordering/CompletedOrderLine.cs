using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Ordering
{
    [Table("Completed Order Lines")]
    public class CompletedOrderLine : OrderLine
    {
        public CompletedOrderLine() { }
        public CompletedOrderLine(Product product, int count)
        {
            ProductId = product.Id;
            Count = count;
        }
    }
}
