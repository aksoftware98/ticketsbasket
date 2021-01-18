using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Mappers;
using TicketsBasket.Repositories;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync();
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

        public async Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync()
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);
            
            if (userProfile == null)
                return Error<UserProfileDetail>("Profile not found", null);

            return Success("Profile retrieved successfully", userProfile.ToUserProfileDetail());
        }
    }
}
