using Core.DbControl;
using Core.Model.Ordering;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Core.Managing
{
    //TODO отдельные таблицы для завершённых заказов.
    public class OrderManager
    {

        public event EventHandler OrderCreatedHandler;
        public event EventHandler OrderStatusChangedHandler;
        public event EventHandler OrderCompletedHandler;

        private readonly StoreDbContext _dbContext;

        public OrderManager()
        {
            _dbContext = new StoreDbContext();
        }

        public List<Order> Orders => _dbContext.Orders.ToList();

        public List<CompletedOrder> CompletedOrders => _dbContext.CompletedOrders.ToList();

        /// <summary>
        /// Создание заказа.
        /// </summary>
        /// <param name="clientId">Клиент для которого создаётся заказ</param>
        /// <param name="orderLines">Список товаров в заказе</param>
        /// <param name="deliveryType">Способ доставки.</param>
        public Order AddOrder(int clientId, List<OrderLine> orderLines, DeliveryType deliveryType)
        {
            try
            {
                //Проверка, что существует клиент с таким Id.
                if (!_dbContext.Clients.Any(c => c.Id == clientId))
                    throw new ArgumentException($"There is no client with id = {clientId}.", nameof(clientId));

                //Расчёт общей стоимости заказа.
                double totalPrice = default;
                foreach (var orderLine in orderLines)
                    totalPrice += orderLine.Price;

                Order newOrder = new Order
                {
                    ClientId = clientId,
                    CreateDateTime = DateTime.Now,
                    TotalPrice = totalPrice,
                    DeliveryType = deliveryType
                };
                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();
                foreach (var ordLine in orderLines)
                    ordLine.OrderId = newOrder.Id;

                // Добавление строк заказов.
                _dbContext.OrderLines.AddRange(orderLines);
                _dbContext.SaveChanges();
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

            var ordToChange = _dbContext.Orders.Find(order.Id);

            if (ordToChange == null)
                throw new ArgumentNullException(nameof(order), "Parameter order is null.");

            ordToChange.OrderStatus = newStatus;
            _dbContext.SaveChanges();
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

            var ordToChange = _dbContext.Orders.Find(orderId);

            if (ordToChange == null)
                throw new ArgumentException($"No order with Id = {orderId}");

            ordToChange.OrderStatus = newStatus;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Завершение заказа
        /// </summary>
        /// <param name="order">Заказа для завершения.</param>
        //TODO Переливка строк заказов.
        public void CompleteOrder(Order order)
        {
            var ordToComplete = _dbContext.Orders.Find(order.Id);
            if (ordToComplete == null)
                throw new ArgumentNullException(nameof(order), $"There is no order with id = {order.Id}");
            CompletedOrder completedOrder = new CompletedOrder()
            {
                ClientId = ordToComplete.Client.Id,
                CreateDateTime = ordToComplete.CreateDateTime,
                TotalPrice = ordToComplete.TotalPrice,
                CompleteDateTime = DateTime.Now,
                OrderStatus = OrderStatus.Completed
            };
            _dbContext.CompletedOrders.Add(completedOrder);
            _dbContext.SaveChanges();
            // Перенос строк заказов завершённых заказов.
            List<CompletedOrderLine> completedOrderLines = new List<CompletedOrderLine>();
            foreach (var orderLine in ordToComplete.OrderLines)
            {
                completedOrderLines.Add(new CompletedOrderLine(orderLine.Product, orderLine.Count) {OrderId = completedOrder.Id});
            }
            _dbContext.CompletedOrderLines.AddRange(completedOrderLines);

            //_dbContext.AttachRange(ordToComplete.OrderLines);
            _dbContext.OrderLines.RemoveRange(ordToComplete.OrderLines);
            _dbContext.Orders.Remove(ordToComplete);
            _dbContext.SaveChanges();
            OrderCompletedHandler?.Invoke(this, new CompletedOrderEventArgs(completedOrder));
        }


        /// <summary>
        /// Завершение заказа.
        /// </summary>
        /// <param name="orderId">Id заказа для завершения.</param>
        public void CompleteOrder(int orderId)
        {
            var ordToComplete = _dbContext.Orders.Find(orderId);
            if (ordToComplete != null)
            {
                // Перенос заказа в таблицу завершённых заказов.
                CompletedOrder completedOrder = new CompletedOrder()
                {
                    ClientId = ordToComplete.Client.Id,
                    CreateDateTime = ordToComplete.CreateDateTime,
                    TotalPrice = ordToComplete.TotalPrice,
                    CompleteDateTime = DateTime.Now,
                    OrderStatus = OrderStatus.Completed
                };
                _dbContext.CompletedOrders.Add(completedOrder);
                _dbContext.SaveChanges();
                // Перенос строк заказов завершённых заказов.
                List<CompletedOrderLine> completedOrderLines = new List<CompletedOrderLine>();
                foreach (var orderLine in ordToComplete.OrderLines)
                {
                    completedOrderLines.Add(new CompletedOrderLine(orderLine.Product, orderLine.Count) { OrderId = completedOrder.Id });
                }
                _dbContext.CompletedOrderLines.AddRange(completedOrderLines);

                _dbContext.OrderLines.RemoveRange(ordToComplete.OrderLines);
                _dbContext.Orders.Remove(ordToComplete);
                _dbContext.SaveChanges();
                OrderCompletedHandler?.Invoke(this, new CompletedOrderEventArgs(completedOrder));
            }
            else
            {
                throw new ArgumentNullException($"There is no order with id = {orderId}");
            }
        }
    }
}
