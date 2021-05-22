using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsBasket.Shared.Responses
{

    public class OperationResponse<T> : BaseResponse
    {

        public T Record { get; set; }

        public OperationResponse()
        {
            IsSuccess = true; 
        }

        public OperationResponse(string message) : this()
        {
            Message = message;
        }

        public OperationResponse(string message, T record) : this()
        {
            Message = message;
            Record = record;
        }
    }
}
