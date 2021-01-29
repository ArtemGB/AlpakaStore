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
            using StoreDbContext dbContext = new StoreDbContext();
            try
            {
                Order newOrder = new Order { Client = client, OrderLines = orderLines, CreateDate = DateTime.Now };
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
                throw new Exception("Order creation error.", e);
            }
        }

        //TODO
        /// <summary>
        /// Меняет статус заказа.
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <param name="newStatus">Новое значение статуса</param>
        public void ChangeOrderStatus(Order order, OrderStatus newStatus)
        {
            if (order != null)
            {
                using StoreDbContext dbContext = new StoreDbContext();
                var ordToChange = dbContext.Orders.Find(order.Id);
                if (ordToChange != null)
                {
                    ordToChange.OrderStatus = newStatus;
                    dbContext.SaveChanges();
                }
                else throw new ArgumentException($"No order with Id = {order.Id}");
            }
            else throw new ArgumentNullException(nameof(order), "Parameter order is null.");
        }

        /// <summary>
        /// Меняет статус заказа
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="newStatus">Новое значение статуса</param>
        public void ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            var ordToChange = dbContext.Orders.Find(orderId);
            if (ordToChange != null)
            {
                ordToChange.OrderStatus = newStatus;
                dbContext.SaveChanges();
            }
            else throw new ArgumentException($"No order with Id = {orderId}");
        }

        /// <summary>
        /// Завершение заказа
        /// </summary>
        /// <param name="order">Заказа для завершения.</param>
        public void CompleteOrder(Order order)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            var ordToComplete = dbContext.Orders.Find(order.Id);
            if (ordToComplete != null)
            {
                ChangeOrderStatus(order, OrderStatus.Completed);

                // Перенос заказа в таблицу завершённых заказов.
                dbContext.CompletedOrders.Add(new CompletedOrder()
                {
                    Client = ordToComplete.Client,
                    CreateDate = ordToComplete.CreateDate,
                    CompleteDate = DateTime.Now
                });
                dbContext.Orders.Remove(ordToComplete);

                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(order), $"There is no order with id = {order.Id}");
            }
        }
    }
}
