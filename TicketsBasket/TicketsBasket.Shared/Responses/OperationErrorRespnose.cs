namespace TicketsBasket.Shared.Responses
{
    public class OperationErrorRespnose : BaseResponse
    {
        public string[] Errors { get; set; }

        public OperationErrorRespnose()
        {
            IsSuccess = false; 
        }

        public OperationErrorRespnose(string message) : this()
        {
            Message = message;
        }

        public OperationErrorRespnose(string message, string[] errors) : this()
        {
            Message = message;
            Errors = errors;
        }
    }
}
