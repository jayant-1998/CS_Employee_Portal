using Employee_Portal.Models.RequestViewModels;

namespace Employee_Portal.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<RegistrationViewModel> RegistrationAsync(RegistrationViewModel reg);
        public Task<string> LoginAsync(LoginViewModel reg);
        //public string Encryption();
    }
}
