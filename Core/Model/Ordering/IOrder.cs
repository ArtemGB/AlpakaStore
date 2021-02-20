using System;
using System.Collections.Generic;
using Core.Model.Users;

namespace Core.Model.Ordering
{
    public interface IOrder
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; }
        public List<IOrderLine> OrderLines { get; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double TotalPrice { get; set; }

    }
}