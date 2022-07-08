using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kidsffw.Api.Controllers
{
    using Application.Interfaces.Service;
    using Common.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public UserRegistrationController(IUserRegistrationService userRegistrationService) => _userRegistrationService = userRegistrationService;

        [HttpPost("RegisterUser")]
        [ProducesResponseType(typeof(GetUserRequestDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRegistrationRequestDto userRegistration)
        {
            var result = await _userRegistrationService.AddUserRegistration(userRegistration);
            return Ok(result);
        }

        [HttpGet("ListUsersByMobileNumber/{mobileNumber}")]
        [ProducesResponseType(typeof(IEnumerable<GetUserRequestDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListUsersByMobileNumber(string mobileNumber)
        {
            var result = await _userRegistrationService.GetUsersByMobileNumber(mobileNumber);
            return Ok(result);
        }

    }
}
