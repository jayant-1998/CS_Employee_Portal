
namespace Employee_Portal.Models.ResponseViewModels
{
    public class OtherServerViewModel
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

}
