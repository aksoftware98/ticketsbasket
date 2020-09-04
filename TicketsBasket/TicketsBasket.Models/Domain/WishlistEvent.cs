using System;

namespace TicketsBasket.Models.Domain
{
    public class WishlistEvent : Record
    {

        public WishlistEvent()
        {
            CreatedOn = DateTime.UtcNow;    
        }
        public DateTime CreatedOn { get; set; }


        public virtual Event Event { get; set; }
        public string EventId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public string UserProfileId { get; set; }
    }

}
