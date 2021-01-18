using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TicketsBasket.Infrastructure.Options
{
    public class IdentityOptions
    {

        public string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        public ClaimsPrincipal User { get; set; }

        // TODO: Other identity properties 

    }
}
