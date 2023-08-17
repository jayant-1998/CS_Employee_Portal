using System.ComponentModel.DataAnnotations;

namespace Employee_Portal.Models.RequestViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "password is Required")]
        public string Password { get; set; }
    }
}
