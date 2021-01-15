using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Core.Model.Managing
{
    public class OrderManager
    {
        private DbSet<Order> orders;

        public List<Order> Orders
        {
            get => orders.ToList();
        }
        public event EventHandler OrderCreatedHandler;
        public event EventHandler OrderCreatingErrorHandler;
        public event EventHandler OrderStatusChangedHandler;
        public event EventHandler OrderCompletedHandler;

        public OrderManager(DbSet<Order> orders)
        {
            this.orders = orders;
        }

        //TODO
        public void CreateOrder(Order newOrder)
        {
            using (StoreDbContext dbContext = new StoreDbContext())
            {
                try
                {
                    dbContext.Orders.AddAsync(newOrder);
                }
                catch (Exception e)
                {
                   OrderCreatingErrorHandler(newOrder, new EventArgs());
                }
                
            }
        }

        //TODO
        public void ChangeOrderStatus(Order order, OrderStatus newStatus)
        {

        }
    }
}
