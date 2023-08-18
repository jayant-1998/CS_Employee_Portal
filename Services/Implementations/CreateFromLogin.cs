using Employee_Portal.DAL.Entities;
using Employee_Portal.Extensions;
using Employee_Portal.Models.RequestViewModels;
using Employee_Portal.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Employee_Portal.Services.Implementations
{
    public class CreateFromLogin : ICreateFromLogin
    {
        public async Task<Registration> Create(RegistrationWithRoleViewModel model)
        {
            Registration reg = model.ToViewModel<RegistrationWithRoleViewModel, Registration>();
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = "https://localhost:7299/api/JWTEmployee/get";

                string requestBody = JsonConvert.SerializeObject(reg);
                await Console.Out.WriteLineAsync(requestBody);
                HttpContent content = new StringContent(requestBody,Encoding.UTF8,"application/json");
               
                var response = await client.PostAsync(requestUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Registration res = JsonConvert.DeserializeObject<Registration>(responseBody);
                return res;
            }
        }
    }
}
