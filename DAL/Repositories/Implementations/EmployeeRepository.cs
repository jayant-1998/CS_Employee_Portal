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
        public async Task<Registration> CreateAsync(Registration reg)
        {
            await _context.AddAsync(reg);
            await _context.SaveChangesAsync();
            return reg;

        }

        public async Task<Registration?> DeleteAsync(int id)
        {
            Registration registration = await _context.registrations
                .Where(t => t.UserId == id)
                .FirstOrDefaultAsync();
            if (registration != null)
            {
                registration.IsDeleted = true;
                registration.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return registration;
            }
            return null;
        }

        public async Task<IEnumerable<Registration>> ReadAsync(int id)
        {
            if (id != 0)
            {
                var registration = await _context.registrations
                            .Where(t => t.UserId == id)
                            .ToListAsync();
                return registration;
            }
            else
            {
                var registration =await _context.registrations
                            .Where(t => !t.IsDeleted )
                            .ToListAsync();
                return registration;
            }
        }

        public async Task<Registration?> UpdateAsync(int id,UpdateViewModel update)
        {
            Registration registration = await _context.registrations
                            .Where(t => t.UserId == id && t.IsDeleted == false)
                            .FirstOrDefaultAsync();
            if (registration != null)
            {
                registration.Name= update.Name;
                registration.Age = update.Age;
                await _context.SaveChangesAsync();
                return registration;
            }
            return null;
        }
    }
}
