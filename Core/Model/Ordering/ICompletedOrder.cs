using System;

namespace Core.Model.Ordering
{
    public interface ICompletedOrder
    {
        public DateTime CompleteDateTime { get; }
    }
}