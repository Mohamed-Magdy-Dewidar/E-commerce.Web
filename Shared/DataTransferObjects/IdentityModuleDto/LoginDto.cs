﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityModuleDto
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;


        [Required]
  
        public string Password { get; set; } = default!;
    }
}
