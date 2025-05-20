using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace Presentation.Controller
{
    
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost(template: "Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }


        [HttpPost(template: "Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user  = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        //check email
        [HttpGet(template:"CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAddressAsync(Email);
            return Ok(result);
        }

        //Get Current User
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(claimType: ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(Email);
            return Ok(AppUser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(claimType: ClaimTypes.Email);
            var AppUserAddress = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(Email);
            return Ok(AppUserAddress);
        }
        
        
        [Authorize]
        [HttpPut("Address")]        
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(claimType: ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(Email, addressDto);
            return Ok(UpdatedAddress);
        }










    }
}
