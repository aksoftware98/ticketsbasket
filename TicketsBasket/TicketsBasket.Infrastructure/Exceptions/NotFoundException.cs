namespace TicketsBasket.Infrastructure.Exceptions
{
    public class NotFoundException : BadRequestException
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
