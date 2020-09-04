using System;

namespace TicketsBasket.Models.Domain
{
    public class Like : Record
    {

        public Like()
        {
            LikedOn = DateTime.UtcNow; 
        }

        public DateTime LikedOn { get; set; }

        public virtual Event Event { get; set; }
        public string EventId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public string UserProfileId { get; set; }
    }

}
