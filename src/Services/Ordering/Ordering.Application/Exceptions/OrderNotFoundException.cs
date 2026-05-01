using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message)
            : base(message)
        {
            
        }
        public OrderNotFoundException(OrderId orderId)
            : base($"Order with ID '{orderId}' was not found.")
        {
        }
    }
}
