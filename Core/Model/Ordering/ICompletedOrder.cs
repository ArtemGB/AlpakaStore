using System;

namespace Core.Model.Ordering
{
    public interface ICompletedOrder : IOrder<CompletedOrderLine>
    {
        public DateTime CompleteDateTime { get; }
    }
}