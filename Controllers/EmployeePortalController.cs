using Employee_Portal.Models.RequestViewModels;
using Employee_Portal.Models.ResponseViewModels;
using Employee_Portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeePortalController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeePortalController(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetRequiredService<IEmployeeService>();
        }
        // POST: insert data
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(RegistrationViewModel reg)
        {
            try
            {
                RegistrationViewModel result = await _service.RegistrationAsync(reg);
                var response = new ApiViewModel
                {
                    Code = 200,
                    Message = "Success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                var result = await _service.LoginAsync(login);
                if (result == "unauthorized")
                {
                    throw new Exception("unauthorized");
                }
                var response = new ApiViewModel
                {
                    Code = 200,
                    Message = "Success",
                    Body = result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiViewModel
                {
                    Code = 500,
                    Message = ex.Message,
                    Body = null
                };
                return Ok(response);
            }
        }
        //[HttpPost("ENCREPTION")]
        //public IActionResult Encreption()
        //{
        //    try
        //    {
        //        var result = _service.Encryption();
        //        var response = new ApiViewModel
        //        {
        //            Code = 200,
        //            Message = "Success",
        //            Body = result
        //        };
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = new ApiViewModel
        //        {
        //            Code = 500,
        //            Message = ex.Message,
        //            Body = null
        //        };
        //        return Ok(response);
        //    }
        //}
    }
}