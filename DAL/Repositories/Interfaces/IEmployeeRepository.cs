using Employee_Portal.DAL.Entities;
using Employee_Portal.Models.RequestViewModels;

namespace Employee_Portal.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Employee> RegistrationAsync(Employee reg);
        public Task<Employee?> LoginAsync(LoginViewModel reg);
        public Task<Token?> GetSecretKey();
    }
}

