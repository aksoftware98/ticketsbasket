using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Exceptions;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Domain;
using TicketsBasket.Models.Mappers;
using TicketsBasket.Repositories;
using TicketsBasket.Services.Storage;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Requests;

namespace TicketsBasket.Services
{
    public class UserProfilesService : BaseService, IUserProfilesService
    {

        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public UserProfilesService(IdentityOptions identity,
                                   IUnitOfWork unitOfWork,
                                   IStorageService storageService)
        {
            _identity = identity;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
        }

        public async Task<UserProfileDetail> CreateProfileAsync(CreateProfileRequest model)
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

            return newUser.ToUserProfileDetail();
        }

        public async Task<UserProfileDetail> GetProfileByUserIdAsync()
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);

            if (userProfile == null)
                throw new NotFoundException("User profile not found");

            return userProfile.ToUserProfileDetail();
        }

        public async Task<UserProfileDetail> UpdateProfilePictureAsync(IFormFile image)
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);

            if (userProfile == null)
                throw new NotFoundException("User profile not found");

            // Save the new image 
            string imageUrl = userProfile.ProfilePicture;

            imageUrl = await _storageService.SaveBlobAsync("users", image, BlobType.Image);

            // remove the old blob
            if (userProfile.ProfilePicture != "Unknown")
            {
                await _storageService.RemoveBlobAsync("users", userProfile.ProfilePicture);
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ValidationException("Image is required"); 


            userProfile.ProfilePicture = imageUrl;

            await _unitOfWork.CommitChangesAsync();

            return userProfile.ToUserProfileDetail();
        }
    }
}
