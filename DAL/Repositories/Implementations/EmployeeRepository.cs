using Employee_Portal.DAL.DBContexts;
using Employee_Portal.DAL.Entities;
using Employee_Portal.DAL.Repositories.Interfaces;
using Employee_Portal.Models.RequestViewModels;
using Microsoft.EntityFrameworkCore;

namespace Employee_Portal.DAL.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;

        public EmployeeRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDBContext>();
        }

        public async Task<Token?> GetSecretKey()
        {
            Token response = await _context.Tokens
                                           .Where(r => r.Id == 1)
                                           .FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            return null;
        }

        public async Task<Employee?> LoginAsync(LoginViewModel reg)
        {
            Employee response = await _context.Employees
                                .Where(r => r.Email == reg.Email)
                                .FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            return null;
        }

        public async Task<Employee> RegistrationAsync(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            await _context.SaveChangesAsync();
            return emp;
        }
    }
}
