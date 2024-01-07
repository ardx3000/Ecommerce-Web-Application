using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web_Application.Models
{
    public class UserModel
    {

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        

        public UserModel()
        {
            // You can initialize properties or perform other actions here if needed
        }

    }


}
