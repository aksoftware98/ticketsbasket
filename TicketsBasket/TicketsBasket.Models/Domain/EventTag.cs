using System.ComponentModel.DataAnnotations;

namespace TicketsBasket.Models.Domain
{
    public class EventTag : Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public virtual Event Event { get; set; }

        public string EventId { get; set; }
    }

}
