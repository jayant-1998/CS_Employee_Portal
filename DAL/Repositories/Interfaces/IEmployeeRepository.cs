using Employee_Portal.DAL.Entities;
using Employee_Portal.Models.RequestViewModels;

namespace Employee_Portal.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Registration> CreateAsync(Registration reg);
        public Task<IEnumerable<Registration>> ReadAsync(int id);
        public Task<Registration> UpdateAsync(int id,UpdateViewModel update);
        public Task<Registration> DeleteAsync(int id);
    }
}

