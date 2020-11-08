using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsBasket.Shared.Responses
{
    public class OperationResponse<T>
    {

        public string Message { get; set; }
        public T Record { get; set; }
        public bool IsSuccess { get; set; }

    }
}
