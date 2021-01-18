using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Domain;
using TicketsBasket.Models.Mappers;
using TicketsBasket.Repositories;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync();

        Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model); 
    }

    public class UserProfilesService : BaseService, IUserProfilesService
    {

        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork; 

        public UserProfilesService(IdentityOptions identity,
                                   IUnitOfWork unitOfWork)
        {
            _identity = identity;
            _unitOfWork = unitOfWork; 
        }

        public async Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model)
        {
            var user = _identity.User;

            var city = user.FindFirst("city").Value;
            var country = user.FindFirst("country").Value;
            var firstName = user.FindFirst(ClaimTypes.GivenName).Value;
            var lastName = user.FindFirst(ClaimTypes.Surname).Value;
            var fullName = user.FindFirst("name").Value; 
            var email = user.FindFirst("emails").Value;

            // TODO: Upload the picture to Azure Blob Storage
            string profilePictureUrl = "Unknown";

            var newUser = new UserProfile
            {
                Country = country,
                City = city,
                CreatedOn = DateTime.UtcNow,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid().ToString(),
                UserId = _identity.UserId,
                IsOrganizer = model.IsOrganizer,
                ProfilePicture = profilePictureUrl,
            };

            await _unitOfWork.UserProfiles.CreateAsync(newUser);
            await _unitOfWork.CommitChangesAsync();

            return Success("User profile created successfully!", newUser.ToUserProfileDetail());
        }

        public async Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync()
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);
            
            if (userProfile == null)
                return Error<UserProfileDetail>("Profile not found", null);

            return Success("Profile retrieved successfully", userProfile.ToUserProfileDetail());
        }
    }
}
