using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModuleDto;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager , IConfiguration configuration , IMapper _mapper) : IAuthenticationService
    {


        public async Task<bool> CheckEmailAddressAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            return user is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);            
            return new UserDto()
            {
                Email = Email,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user),
            };

        }




        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(x => x.Email == Email) ?? throw new UserNotFoundException(Email);
            if (user.Address is null)
            {
                throw new AddressNotFoundException(user?.UserName ?? "UserName:Unknown");
            }
            
            return _mapper.Map<Address , AddressDto>(user.Address);

        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(x => x.Email == Email) ?? throw new UserNotFoundException(Email);
            if (user.Address is not null) // update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else //Create Address
            {
                user.Address =  _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address ,  AddressDto>(user.Address);
        }
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user  = await _userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordValid)
                return new UserDto()
                {
                    Email = loginDto.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user),
                };
            else
                throw new UnauthorizedException();  

        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Clamis = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName !)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            
            foreach(var role in Roles)
                Clamis.Add(new Claim(ClaimTypes.Role, role));



            //var SecretKey = configuration["JWTOptions:SecretKey"];
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credintials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: Clamis,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credintials
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);






        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user  = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };

            var result =await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = await CreateTokenAsync(user),
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }

    
    }
}
