using System;
using System.Collections.Generic;
using System.Data;
using Core.Model.Managing;
using Core.Model.Ordering;
using Core.Model.Users;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new UserManager();
            OrderManager orderManager = new OrderManager();
            userManager.AddClient("Pidor", "Pidorovich");
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine() {Product = new Product() {Category = new Category() {Name = "Hui"}, Description = "TEstProduct", Filter = null, Name = "TestProd"}});
            orderManager.CreateOrder(1, orderLines, DeliveryType.Pickup);
            Console.WriteLine("Orders:");
            foreach (var order in orderManager.Orders)
            {
                Console.WriteLine($"\t Id = {order.Id}, Client - {order.Client.Id}" +
                                  $" {order.Client.Name} {order.Client.SecondName}," +
                                  $" CreateDate - {order.CreateDate}, Complete Date - {order.CompleteDate}," +
                                  $" Total Price - {order.TotalPrice}, Status {order.OrderStatus}");
                foreach (OrderLine orderLine in order.OrderLines)
                {
                    Console.WriteLine($"\t\t Id - {orderLine.Id}, Order - {orderLine.Order.Id}," +
                                      $" Product - {orderLine.ProductName}, Count - {orderLine.Count}");
                }
            }

        }
    }
}
