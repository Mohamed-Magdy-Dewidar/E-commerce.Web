using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [HttpGet(template: "emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([EmailAddress] string Email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailAddressAsync(Email);
            return Ok(result);
        }

        //Get Current User
        [Authorize]
        //[HttpGet("CurrentUser")]

        // after user gets token every time he refreshes the page or makes a request,
        // this method will be called to get the current user
        // as http is stateless, we always need to get the user from the token
        //commented the 'CurrentUser' for frontend Integration
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken());
            return Ok(AppUser);
        }


        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var AppUserAddress = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken());
            return Ok(AppUserAddress);
        }


        
        
        [Authorize]
        [HttpPut("Address")]        
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(GetEmailFromToken(), addressDto);
            return Ok(UpdatedAddress);
        }










    }
}
