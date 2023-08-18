namespace Employee_Portal.Models.RequestViewModels
{
    public class RegistrationWithRoleViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }
}
