using Employee_Portal.Models.ResponseViewModels;

namespace Employee_Portal.Services.Interfaces
{
    public interface IFetchDataFromLogin
    {
        public Task<OtherServerViewModel> FetchData(string? jwt, int id);

    }
}
