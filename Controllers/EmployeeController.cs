using employee_management.Models;
using employee_management.Services;
using Microsoft.AspNetCore.Mvc;

namespace employee_management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<EmployeeController>>>> Add(AddEmployeeDto employee)
        {
            return Ok();
        }
    }
}