using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketsBasket.Models.Domain
{
    public class Record
    {

        public Record()
        {
            Id = Guid.NewGuid().ToString(); 
        }

        [Key]
        public string Id { get; set; }

    }
}
