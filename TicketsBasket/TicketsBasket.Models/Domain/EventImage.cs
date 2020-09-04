using System.ComponentModel.DataAnnotations;

namespace TicketsBasket.Models.Domain
{
    public class EventImage : Record
    {
        [Required]
        [StringLength(256)]
        public string ImageUrl { get; set; }

        [StringLength(256)]
        public string ThumpUrl { get; set; }

        public virtual Event Event { get; set; }

        public string EventId { get; set; }
    }

}
