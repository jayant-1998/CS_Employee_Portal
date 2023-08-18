using Employee_Portal.Models.ResponseViewModels;
using Employee_Portal.Services.Interfaces;
using Newtonsoft.Json;

namespace Employee_Portal.Services.Implementations
{
    public class FetchDataFromLogin : IFetchDataFromLogin
    {
        public async Task<OtherServerViewModel> FetchData(string? jwt, int id)
        {
            using (HttpClient client = new HttpClient())
            {

                string requestUrl = null;
                string apiUrl = "https://localhost:7299/api/JWTEmployee/get";
                if (jwt == null)
                {
                    requestUrl = $"{apiUrl}?id={id}";
                }
                else
                {
                    requestUrl = $"{apiUrl}?jwt={jwt}&id=0";
                }

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                OtherServerViewModel res = JsonConvert.DeserializeObject<OtherServerViewModel>(responseBody);
                return res;
            }
        }
    }
}
