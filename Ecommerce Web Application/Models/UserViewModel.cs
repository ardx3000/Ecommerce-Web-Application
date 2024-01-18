using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web_Application.Models
{
    public class UserViewModel : IdentityUser
    {

        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public UserViewModel()
        {
            
        }
    }
}
