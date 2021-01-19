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

        //TODO
        /// <summary>
        /// Создание заказа.
        /// </summary>
        /// <param name="newOrder"></param>
        public void CreateOrder(Client client, List<Product> products, DeliveryType deliveryType)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                try
                {
                    
                    dbContext.Orders.AddAsync(new Order{Client = client, Products = products});
                    OrderCreatedHandler?.Invoke(this, new OrderEventArgs(newOrder));
                }
                catch (Exception e)
                {

                }
            }
        }

        //TODO
        public void ChangeOrderStatus(Order order, OrderStatus newStatus)
        {

        }
    }
}
