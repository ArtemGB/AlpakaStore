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
    //TODO расчёт цен и запись цены в заказ
    public class OrderManager
    {

        public event EventHandler OrderCreatedHandler;
        public event EventHandler OrderStatusChangedHandler;
        public event EventHandler OrderCompletedHandler;

        public OrderManager()
        {
        }

        public List<Order> Orders
        {
            get
            {
                using (StoreDbContext dbContext = new StoreDbContext())
                {
                    return dbContext.Orders.ToList();
                }
            }
        }

        /// <summary>
        /// Создание заказа.
        /// </summary>
        /// <param name="clientId">Клиент для которого создаётся заказ</param>
        /// <param name="orderLines">Список товаров в заказе</param>
        /// <param name="deliveryType">Способ доставки.</param>
        public void CreateOrder(int clientId, List<OrderLine> orderLines, DeliveryType deliveryType)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            try
            {
                //Проверка, что существует клиент с таким Id.
                if (!dbContext.Clients.Any(c => c.Id == clientId))
                    throw new ArgumentException($"There is no client with id = {clientId}.", nameof(clientId));
                Order newOrder = new Order { Client = dbContext.Clients.Find(clientId), OrderLines = orderLines, CreateDate = DateTime.Now };
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
                CompletedOrder completedOrder = new CompletedOrder()
                {
                    Client = ordToComplete.Client,
                    CreateDate = ordToComplete.CreateDate,
                    CompleteDate = DateTime.Now
                };
                dbContext.CompletedOrders.Add(completedOrder);
                dbContext.Orders.Remove(ordToComplete);

                dbContext.SaveChanges();
                OrderCompletedHandler.Invoke(this,new CompletedOrderEventArgs(completedOrder));
            }
            else
            {
                throw new ArgumentNullException(nameof(order), $"There is no order with id = {order.Id}");
            }
        }


        /// <summary>
        /// Завершение заказа.
        /// </summary>
        /// <param name="orderId">Id заказа для завершения.</param>
        public void CompleteOrder(int orderId)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            var ordToComplete = dbContext.Orders.Find(orderId);
            if (ordToComplete != null)
            {
                ChangeOrderStatus(orderId, OrderStatus.Completed);

                // Перенос заказа в таблицу завершённых заказов.
                CompletedOrder completedOrder = new CompletedOrder()
                {
                    Client = ordToComplete.Client,
                    CreateDate = ordToComplete.CreateDate,
                    CompleteDate = DateTime.Now
                };
                dbContext.CompletedOrders.Add(completedOrder);
                dbContext.Orders.Remove(ordToComplete);

                dbContext.SaveChanges();
                OrderCompletedHandler.Invoke(this, new CompletedOrderEventArgs(completedOrder));
            }
            else
            {
                throw new ArgumentNullException($"There is no order with id = {orderId}");
            }
        }
    }
}
