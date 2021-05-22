namespace TicketsBasket.Infrastructure.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}
