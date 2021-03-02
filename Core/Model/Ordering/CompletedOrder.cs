using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Users;

namespace Core.Model.Ordering
{
    [Table("Completed Orders")]
    public class CompletedOrder : ICompletedOrder
    {
        public DateTime CompleteDateTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Clients.Find(ClientId);
            }
        }

        [NotMapped]
        public List<CompletedOrderLine> OrderLines
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.CompletedOrderLines.Where(ord => ord.OrderId == Id).ToList();
            }
        }

        //TODO в метод гет парсинг продуктов в JSON
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double TotalPrice { get; set; }


    }
}
