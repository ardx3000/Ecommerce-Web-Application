using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Web_Application.Models
{
    public class UserViewModel : IdentityUser
    {
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public UserViewModel()
        {
            
        }
    }
}
