using Employee_Portal.DAL.Entities;
using Employee_Portal.Models.RequestViewModels;

namespace Employee_Portal.Services.Interfaces
{
    public interface ICreateFromLogin
    {
        public Task<Registration> Create(RegistrationWithRoleViewModel model);
    }
}
