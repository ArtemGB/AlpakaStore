using Core.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Ordering
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public List<Product> Products { get; set; }

        //TODO в метод гет парсинг продуктов в JSON
        public string ProductsJSON { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusName 
        { 
            get
            {
                switch (OrderStatus)
                {
                    case OrderStatus.Confirmed: return "Подтверждён";
                    case OrderStatus.Assembling: return "Сборка";
                    case OrderStatus.Delivering: return "Доставка";
                    case OrderStatus.Completed: return "Завершён";
                    default: return "";
                }
            }
            set { }
        }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public double TotalPrice { get; set; }

    }
}
