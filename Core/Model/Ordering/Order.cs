using Core.Model.Users;
using System;
using System.Collections.Generic;

namespace Core.Model.Ordering
{
    class Order
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public List<Product> Products { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public double TotalPrice { get; set; }

    }
}
