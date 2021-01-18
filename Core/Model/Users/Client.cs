using Core.Model.Ordering;
using System.Collections.Generic;

namespace Core.Model.Users
{
    public class Client : User
    {
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }

        public bool ConfirmeOrder()
        {
            return false;
        }

        public bool CancelOrder(Order order)
        {
            return false;
        }

        public bool CancelOrder(int orderID)
        {
            return false;
        }

        private string OrdersToJSON()
        {
            return "";
        }
    }
}
