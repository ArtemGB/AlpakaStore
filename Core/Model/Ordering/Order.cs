using Core.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model.Ordering
{
    [Table("Orders")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Client Client { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        //TODO в метод гет парсинг продуктов в JSON
        public OrderStatus OrderStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public double TotalPrice { get; set; }

    }
}
