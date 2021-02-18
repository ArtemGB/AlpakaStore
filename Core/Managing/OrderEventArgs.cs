using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Ordering;

namespace Core.Managing
{
    public class OrderEventArgs : EventArgs
    {
        public readonly Order Order;

        public OrderEventArgs(Order order)
        {
            Order = order;
        }
    }

    public class CompletedOrderEventArgs : EventArgs
    {
        public readonly CompletedOrder Order;

        public CompletedOrderEventArgs(CompletedOrder completedOrder)
        {
            Order = completedOrder;
        }
    }
}
