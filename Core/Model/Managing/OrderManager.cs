using Core.DbControl;
using Core.Model.Ordering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Users;


namespace Core.Model.Managing
{
    public class OrderManager
    {
        private DbSet<Order> orders;

        public event EventHandler OrderCreatedHandler;
        public event EventHandler OrderStatusChangedHandler;
        public event EventHandler OrderCompletedHandler;

        public OrderManager(DbSet<Order> orders)
        {
            this.orders = orders;
        }

        //public List<Order> ReadOrders

        //TODO блок catch
        /// <summary>
        /// Создание заказа.
        /// </summary>
        public void CreateOrder(Client client, List<OrderLine> orderLines, DeliveryType deliveryType)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                try
                {
                    Order newOrder = new Order {Client = client, OrderLines = orderLines, CreateDate = DateTime.Now};
                    dbContext.Orders.Add(newOrder);
                    dbContext.SaveChanges();
                    foreach (var ordLine in orderLines)
                        ordLine.Order = newOrder;
                    // Добавление строк заказов.
                    dbContext.OrderLines.AddRange(orderLines);
                    dbContext.SaveChanges();
                    OrderCreatedHandler?.Invoke(this, new OrderEventArgs(newOrder));
                }
                catch (Exception e)
                {

                }
            }
        }

        //TODO
        /// <summary>
        /// Меняет статус заказа.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="newStatus"></param>
        public void ChangeOrderStatus(Order order, OrderStatus newStatus)
        {
            if (order != null)
            {
                using (StoreDbContext dbContext = new StoreDbContext())
                {
                    var Orders = dbContext.Orders.ToList();
                    var ji = Orders.Where(ord => ord.Id == order.Id);
                    if (Orders.Any(ord => ord.Id == order.Id))
                    {

                    }
                    else throw new ArgumentException($"No order with Id = {order.Id}");
                }
            }
            else throw new ArgumentNullException(nameof(order),"Parameter order is null.");
        }
    }
}
