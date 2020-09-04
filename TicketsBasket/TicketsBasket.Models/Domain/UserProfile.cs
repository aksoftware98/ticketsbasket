using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketsBasket.Models.Domain
{
    public class UserProfile : Record
    {

        public UserProfile()
        {
            CreatedOn = DateTime.UtcNow; 
        }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Country { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        public bool IsOrganizer { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual List<Event> Events { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public virtual List<WishlistEvent> WishlistEvents { get; set; }
        public virtual List<Like> Likes { get; set; }


        public virtual List<JobApplication> SentApplications { get; set; } // Sent by normal user
        public virtual List<JobApplication> RecievedApplications { get; set; } // Sent by normal user

    }

  

}
