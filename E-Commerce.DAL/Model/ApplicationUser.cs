using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Model
{
    // Add this class in the situation we need to add a new things to the applicationUser that's not in the IdentityUser
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string? city { get; set; }
        public string? street { get; set; }
        public string? CodeResetPassword { get; set; }
        public DateTime? PasswordResetCodeExpiry { get; set; }
    }
}
