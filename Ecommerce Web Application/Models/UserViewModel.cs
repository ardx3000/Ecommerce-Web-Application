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

        public ICollection<JobPostViewModel> JobAdverts { get; set; } = new List<JobPostViewModel>();

        public UserViewModel()
        {
            
        }
    }
}
