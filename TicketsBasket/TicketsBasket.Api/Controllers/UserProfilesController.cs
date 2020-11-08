using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsBasket.Services;

namespace TicketsBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilesController : ControllerBase
    {

        private readonly IUserProfilesService _userProfilesService;

        public UserProfilesController(IUserProfilesService userProfilesService)
        {
            _userProfilesService = userProfilesService; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userProfilesService.GetProfileByUserIdAsync();
            if (result.IsSuccess)
                return Ok(result);

            return NotFound(); 
        }

    }
}
