using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsBasket.Shared.Models;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Infrastructure
{
    public class AzureADB2CUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AzureADB2CUserFactory(IAccessTokenProviderAccessor tokenProviderAccessor,
                                     IHttpClientFactory httpClientFactory) : base(tokenProviderAccessor)
        {
            _httpClientFactory = httpClientFactory; 
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if(initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                using (var httpClient = _httpClientFactory.CreateClient("TicketsBasket.Api"))
                {
                    var response = await httpClient.GetFromJsonAsync<OperationResponse<UserProfileDetail>>("api/userprofiles");
                    if(response.IsSuccess)
                    {
                        string role = response.Record.IsOrganizer ? "Organizer" : "User";
                        userIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            return initialUser; 
        }

    }
}
