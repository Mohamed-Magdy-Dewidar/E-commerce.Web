using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityModuleDto
{
    public class RegisterDto
    {

        /**
         * 
         * username 
         * PhoneNumber were set with default "" to fit angular project as it does not take username and phone number
         * **/

        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(20, MinimumLength = 3,
        //    ErrorMessage = "Username must be between 3 and 20 characters")]
        //[RegularExpression(@"^[a-zA-Z0-9_]+$",
        //    ErrorMessage = "Username can only contain letters, numbers, and underscores")]

        public string? UserName { get; set; } = "Mohamed_Dewidar";



        [Required(ErrorMessage = "Display name is required")]
        [StringLength(50, MinimumLength = 2,  ErrorMessage = "Display name must be between 2 and 50 characters")]
        public string DisplayName { get; set; } = default!;



        //[Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; } = "";



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100 , ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = default!;

       
        
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; } = default!;

    }
}
