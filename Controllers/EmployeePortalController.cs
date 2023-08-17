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
        [HttpPost("create")]
        public async Task<IActionResult> Create(RegistrationViewModel reg, string? jwt, int id)
        {
            try
            {
                var result = await _service.CreateAsync(reg,jwt, id);

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
        [HttpGet("read")]
        public async Task<IActionResult> Read(string? jwt, int id)
        {
            try
            {
                var result = await _service.ReadAsync(jwt , id);

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
        [HttpPut("update")]
        public async Task<IActionResult> Update(string? jwt, int id,UpdateViewModel update)
        {
            try
            {
                var result = await _service.UpdateAsync(update , jwt , id);

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
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string? jwt, int id)
        {
            try
            {
                var result = await _service.DeleteAsync(jwt , id);

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
    }
}