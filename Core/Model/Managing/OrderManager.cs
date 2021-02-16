using Core.DbControl;
using Core.Model.Ordering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Core.Model.Managing
{
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
        public Order AddOrder(int clientId, List<OrderLine> orderLines, DeliveryType deliveryType)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            try
            {
                //Проверка, что существует клиент с таким Id.
                if (!dbContext.Clients.Any(c => c.Id == clientId))
                    throw new ArgumentException($"There is no client with id = {clientId}.", nameof(clientId));

                //Расчёт общей стоимости заказа.
                double totalPrice = default;
                foreach (var orderLine in orderLines)
                    totalPrice += orderLine.Price;

                Order newOrder = new Order
                {
                    ClientId = clientId,
                    CreateDate = DateTime.Now,
                    TotalPrice = totalPrice,
                    DeliveryType = deliveryType
                };
                dbContext.Orders.Add(newOrder);
                dbContext.SaveChanges();
                foreach (var ordLine in orderLines)
                    ordLine.OrderId = newOrder.Id;

                // Добавление строк заказов.
                dbContext.OrderLines.AddRange(orderLines);
                dbContext.SaveChanges();
                OrderCreatedHandler?.Invoke(this, new OrderEventArgs(newOrder));
                return newOrder;
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
            if (newStatus == OrderStatus.Completed)
                throw new ArgumentException("Prohibited complete order in this method, use CompleteOrder method.");

            if (order == null)
                throw new ArgumentException($"No order with Id = {order.Id}");

            using StoreDbContext dbContext = new StoreDbContext();
            var ordToChange = dbContext.Orders.Find(order.Id);

            if (ordToChange == null)
                throw new ArgumentNullException(nameof(order), "Parameter order is null.");

            ordToChange.OrderStatus = newStatus;
            dbContext.SaveChanges();
            OrderStatusChangedHandler?.Invoke(this, new OrderEventArgs(order));
        }

        /// <summary>
        /// Меняет статус заказа
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <param name="newStatus">Новое значение статуса</param>
        public void ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Completed)
                throw new ArgumentException("Prohibited complete order in this method, use CompleteOrder method.");

            using StoreDbContext dbContext = new StoreDbContext();
            var ordToChange = dbContext.Orders.Find(orderId);

            if (ordToChange == null)
                throw new ArgumentException($"No order with Id = {orderId}");

            ordToChange.OrderStatus = newStatus;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Завершение заказа
        /// </summary>
        /// <param name="order">Заказа для завершения.</param>
        //TODO Переливка строк заказов.
        public void CompleteOrder(Order order)
        {
            using StoreDbContext dbContext = new StoreDbContext();
            var ordToComplete = dbContext.Orders.Find(order.Id);
            if (ordToComplete == null)
                throw new ArgumentNullException(nameof(order), $"There is no order with id = {order.Id}");
            ChangeOrderStatus(order, OrderStatus.Completed);

            // Перенос заказа в таблицу завершённых заказов.
            CompletedOrder completedOrder = new CompletedOrder()
            {
                ClientId = ordToComplete.Client.Id,
                CreateDate = ordToComplete.CreateDate,
                CompleteDate = DateTime.Now
            };
            dbContext.CompletedOrders.Add(completedOrder);

            // Перенос строк заказов завершённых заказов.
            dbContext.CompletedOrderLines.AddRange(order.OrderLines);

            dbContext.Orders.Remove(ordToComplete);

            dbContext.SaveChanges();
            OrderCompletedHandler?.Invoke(this, new CompletedOrderEventArgs(completedOrder));
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
