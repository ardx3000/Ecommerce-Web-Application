using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Web_Application.Models
{
    public class UserEditViewModel
    {
        [NotMapped]
        public string? UserName { get; set; }

        [NotMapped]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [NotMapped]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        
        [NotMapped]
        [DataType(DataType.Password)]
        public string PasswordValidation { get; set; }


        public void MapToUserViewModel(UserViewModel user, PasswordHasher<UserViewModel> passwordHasher)
        {
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;

            if (!string.IsNullOrEmpty(NewPassword))
            {
                // Hash the password before assigning
                user.PasswordHash = passwordHasher.HashPassword(user, NewPassword);
            }
        }
    }
}
