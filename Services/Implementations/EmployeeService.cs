using Employee_Portal.DAL.Entities;
using Employee_Portal.DAL.Repositories.Interfaces;
using Employee_Portal.Extensions;
using Employee_Portal.Models.RequestViewModels;
using Employee_Portal.Models.ResponseViewModels;
using Employee_Portal.Services.Interfaces;

namespace Employee_Portal.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IEmployeeRepository>();
        }
        public async Task<Registration?> CreateAsync(RegistrationViewModel reg, string? jwt, int id)
        {
            var obj = new FetchDataFromLogin();
            OtherServerViewModel temp = await obj.FetchData(jwt,id);
            if (temp.Role == "admin")
            {
                return await _repository.CreateAsync(reg.ToViewModel<RegistrationViewModel, Registration>());
            }
            throw new Exception("only admin can add user");
        }

        public async Task<Registration?> DeleteAsync(string? jwt, int id)
        {
            var obj = new FetchDataFromLogin();
            OtherServerViewModel temp = await obj.FetchData(jwt, id);
            if (temp.Role != "admin") 
            {
                return await _repository.DeleteAsync(temp.UserId);
            }
            throw new Exception("Admin can not be deleted");
        }

        public async Task<IEnumerable<OtherServerViewModel>> ReadAsync(string? jwt, int id)
        {
            var obj = new FetchDataFromLogin();
            OtherServerViewModel temp = await obj.FetchData(jwt, id);
            if (temp.Role.Equals("admin"))
            {
                List<OtherServerViewModel> responses = new List<OtherServerViewModel>();
                var items = await _repository.ReadAsync(0);
                foreach (var item in items)
                {
                    var response = item.ToViewModel<Registration, OtherServerViewModel>();
                    responses.Add(response);
                }
                return responses;
            }
            else if (temp.Role.Equals("user"))
            {
                List<OtherServerViewModel> responses = new List<OtherServerViewModel>();
                var items = await _repository.ReadAsync(temp.UserId);
                foreach (var item in items)
                {
                    var response = item.ToViewModel<Registration, OtherServerViewModel>();
                    responses.Add(response);
                }
                return responses;
            }
            throw new Exception("NO other role is allow then user,admin");
        }

        public async Task<Registration> UpdateAsync(UpdateViewModel update, string? jwt, int id)
        {
            var obj = new FetchDataFromLogin();
            OtherServerViewModel temp = await obj.FetchData(jwt, id);
            if (temp.Role.Equals("user"))
            {
                return await _repository.UpdateAsync(temp.UserId,update);
            }
            throw new Exception("admin can not be update");
        }
    }
}
