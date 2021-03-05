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

        public OrderLine AddOrderLine(OrderLine orderLine)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                orderLine.OrderId = Id;
                dbContext.Add(orderLine);
                return orderLine;
            }
        }

        public OrderLine AddOrderLine(Product product, int count)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                OrderLine orderLine = new OrderLine(Id, product, count);
                dbContext.Add(orderLine);
                return orderLine;
            }
        }

        public void RemoveOrderLine(OrderLine orderLine)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                if (orderLine == null) throw new ArgumentNullException(nameof(orderLine));
                dbContext.Attach(orderLine);
                dbContext.OrderLines.Remove(orderLine);
            }
        }

        public void RemoveOrderLine(int id)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                OrderLine lineToDelete = dbContext.OrderLines.Find(id);
                if (lineToDelete != null)
                    dbContext.OrderLines.Remove(lineToDelete);
                else throw new ArgumentException($"There is no Order line with id = {id}.");
            }
        }
    }
}
