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
            orderLines.Add(new OrderLine() {Product = new Product() {Category = new Category(), Description = "TEstProduct", Filter = null, Name = "TestProd"}});
            orderManager.CreateOrder(1, orderLines, DeliveryType.Pickup);

        }
    }
}
