using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Model.Managing;
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
            userManager.AddClient("Pidor", "Pidorovich");
            Filter filter = filterManager.AddFilter();
            Filter filter1 = filterManager.AddFilter();
            filterManager.AddTagToFilter(filter.Id, "Сочные Сосиски");
            categoryManager.AddCategory("Сосиски");
            filterManager.AddTagToFilter(filter.Id, "Салями");
            categoryManager.AddCategory("Колбасные");
            productManager.AddProduct("Сосиска", "Обычная сосисочная сосиска", 160, filter,
                categoryManager.Categories.ToList()[0]);
            productManager.AddProduct("Колбаса", "Обычная колбасная колбаса", 300, filter1,
                categoryManager.Categories.ToList()[0]);
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine() { Count = 10, Product = productManager.Products.Find(product => product.Id == 1) });
            orderLines.Add(new OrderLine() { Count = 3, Product = productManager.Products.Find(product => product.Id == 2) });
            orderManager.AddOrder(1, orderLines, DeliveryType.Pickup);
            PrintOrders();
            Console.ReadLine();

        }

        static void PrintOrders()
        {
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
