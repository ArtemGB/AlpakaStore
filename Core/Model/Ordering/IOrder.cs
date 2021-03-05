using System;
using System.Collections.Generic;
using Core.Model.Users;

namespace Core.Model.Ordering
{
    public interface IOrder<T> where T : IOrderLine
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; }
        public List<T> OrderLines { get; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double TotalPrice { get; set; }
        public T AddOrderLine(T orderLine);
        public T AddOrderLine(Product product, int count);
        public void RemoveOrderLine(T orderLine);
        public void RemoveOrderLine(int id);

    }
}