namespace Ecommerce_Web_Application.Models
{
    public class JobPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        //Foreign key
        public string UserId { get; set; }

        //Navigation prop
        public UserViewModel User { get; set; }
    }
}
