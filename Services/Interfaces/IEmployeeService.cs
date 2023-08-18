using Employee_Portal.DAL.Entities;
using Employee_Portal.Models.RequestViewModels;
using Employee_Portal.Models.ResponseViewModels;

namespace Employee_Portal.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Registration> CreateAsync(RegistrationViewModel reg, string? jwt, int id);
        public Task<IEnumerable<OtherServerViewModel>> ReadAsync(string? jwt, int id);
        public Task<Registration> UpdateAsync(UpdateViewModel update, string? jwt, int id);
        public Task<Registration> DeleteAsync(string? jwt, int id);

    }
}
