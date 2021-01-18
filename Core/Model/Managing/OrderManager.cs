using Core.DbControl;
using Core.Model.Ordering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    dbContext.Orders.Add(newOrder);
                }
                catch (Exception e)
                {
                    OrderCreatingErrorHandler?.Invoke(newOrder, new EventArgs());
                }

            }
        }

        public async Task CreateOrderAsync(Order newOrder)
        {
            Task.Run(() =>
            {
                using (StoreDbContext dbContext = new StoreDbContext())
                {
                    try
                    {
                        dbContext.Orders.AddAsync(newOrder);
                    }
                    catch (Exception e)
                    {
                        OrderCreatingErrorHandler?.Invoke(newOrder, new EventArgs());
                    }

                }
            });
        }

        //TODO
        public void ChangeOrderStatus(Order order, OrderStatus newStatus)
        {

        }
    }
}
