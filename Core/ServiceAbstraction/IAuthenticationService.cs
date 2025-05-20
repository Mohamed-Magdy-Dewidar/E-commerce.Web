using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //This EndPoint Will Handle User Login Take Email and Password 
        //Then Return Token ,  Email and DisplayName To Client

        Task<UserDto> LoginAsync(LoginDto loginDto);



        //This EndPoint Will Handle User Registration Will Take Email , Password  , UserName , Display Name And Phone Number
        //Then
        //Return Token, Email and Display Name To Client
        Task<UserDto> RegisterAsync(RegisterDto registerDto);


        Task<bool> CheckEmailAddressAsync(string Email);
        
        Task<AddressDto> GetCurrentUserAddressAsync(string Email);

        Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto);


        Task<UserDto> GetCurrentUserAsync(string Email);


    }
}
