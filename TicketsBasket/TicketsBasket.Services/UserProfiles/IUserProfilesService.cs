using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<UserProfileDetail> GetProfileByUserIdAsync();
        Task<UserProfileDetail> CreateProfileAsync(CreateProfileRequest model);
        Task<UserProfileDetail> UpdateProfilePictureAsync(IFormFile image);
    }
}
