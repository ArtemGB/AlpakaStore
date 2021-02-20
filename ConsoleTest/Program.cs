using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Managing;
using Core.Model.Ordering;
using Core.Model.Users;

namespace ConsoleTest
{
    class Program
    {
        static UserManager userManager = new UserManager();
        static OrderManager orderManager = new OrderManager();
        static FilterManager filterManager = new FilterManager();
        static ProductManager productManager = new ProductManager();
        static CategoryManager categoryManager = new CategoryManager();

        static void Main(string[] args)
        {
            //userManager.AddClient("Pidor", "Pidorovich");
            //Filter filter = filterManager.AddFilter();
            //Filter filter1 = filterManager.AddFilter();
            //filterManager.AddTagToFilter(filter.Id, "Сочные Сосиски");
            //categoryManager.AddCategory("Сосиски");
            //filterManager.AddTagToFilter(filter1.Id, "Салями");
            //categoryManager.AddCategory("Колбасные");
            //productManager.AddProduct("Сосиска", "Обычная сосисочная сосиска", 160, filter,
            //    categoryManager.Categories.ToList()[0]);
            //productManager.AddProduct("Колбаса", "Обычная колбасная колбаса", 300, filter1,
            //    categoryManager.Categories.ToList()[0]);
            //List<OrderLine> orderLines = new List<OrderLine>();
            //orderLines.Add(new OrderLine(productManager.Products.Find(product => product.Id == 3018), 10));
            //orderLines.Add(new OrderLine(productManager.Products.Find(product => product.Id == 3018), 7));
            //orderManager.AddOrder(1, orderLines, DeliveryType.Pickup);
            PrintOrders();
            //orderManager.CompleteOrder(5005);
            Console.ReadLine();

        }

        static void PrintOrders()
        {
            foreach (var order in orderManager.Orders.ToList())
            {
                Console.WriteLine($" Id = {order.Id}, Client - {order.Client?.Id}" +
                                  $" {order.Client?.Name} {order.Client?.SecondName}," +
                                  $" CreateDateTime - {order.CreateDateTime}," +
                                  $" Total Price - {order.TotalPrice}, Status {order.OrderStatus}");
                foreach (OrderLine orderLine in order.OrderLines)
                {
                    Console.WriteLine($"\t Id - {orderLine.Id}, Order - {orderLine.Order.Id}," +
                                      $" Product - {orderLine.ProductName}, Count - {orderLine.Count}");
                }
                Console.WriteLine();
            }
        }
    }
}
