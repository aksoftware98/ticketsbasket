﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync();
    }

    public class UserProfilesService : IUserProfilesService
    {

        private readonly IdentityOptions _options;

        public UserProfilesService(IdentityOptions options)
        {
            _options = options; 
        }

        public async Task<OperationResponse<UserProfileDetail>> GetProfileByUserIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
