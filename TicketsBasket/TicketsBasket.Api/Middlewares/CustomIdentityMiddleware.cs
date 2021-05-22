using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsBasket.Services;

namespace TicketsBasket.Api.Middlewares
{
    public class CustomIdentityMiddleware
    {
        private readonly RequestDelegate _next; 
        public CustomIdentityMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context, IUserProfilesService userProfilesService)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                var userProfile = await userProfilesService.GetProfileByUserIdAsync();
                if(userProfile != null)
                {
                    string roleName = userProfile.IsOrganizer ? "Organizer" : "User"; 
                    context.User.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, roleName) }));
                }
            }

            await _next(context); 
        }

    }
}
