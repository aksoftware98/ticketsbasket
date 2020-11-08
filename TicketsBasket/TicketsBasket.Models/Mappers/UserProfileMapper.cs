using System;
using System.Collections.Generic;
using System.Text;
using TicketsBasket.Models.Domain;
using TicketsBasket.Shared.Models;

namespace TicketsBasket.Models.Mappers
{
    public static class UserProfileMapper
    {

        public static UserProfileDetail ToUserProfileDetail(this UserProfile userProfile)
        {
            return new UserProfileDetail
            {
                Id = userProfile.Id,
                Country = userProfile.Country,
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                IsOrganizer = userProfile.IsOrganizer,
                LastName = userProfile.LastName,
                UserId = userProfile.UserId,
                ProfilePicture = userProfile.ProfilePicture,
                CreatedSince = "1m"
            };
        }

    }
}
