using Core.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.DbControl;

namespace Core.Model.Ordering
{
    [Table("Orders")]
    public class Order : IOrder<OrderLine>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public  int ClientId { get; set; }
        public Client Client
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.Clients.Find(ClientId);
            }
        }

        [NotMapped]
        public List<OrderLine> OrderLines
        {
            get
            {
                using StoreDbContext dbContext = new StoreDbContext();
                return dbContext.OrderLines.Where(ord => ord.OrderId == Id).ToList();
            }
        }

        //TODO в метод гет парсинг продуктов в JSON
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double TotalPrice { get; set; }

    }
}
