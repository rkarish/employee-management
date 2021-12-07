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
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> Add(AddEmployeeDto employee)
        {
            return Ok(await employeeService.Add(employee));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> GetById(int id)
        {
            return Ok(await employeeService.GetById(id));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> GetAll()
        {
            return Ok(await employeeService.GetAll());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> Update(UpdateEmployeeDto updateEmployee)
        {
            return Ok(await employeeService.Update(updateEmployee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> DeleteById(int id)
        {
            return Ok(await employeeService.DeleteById(id));
        }
    }
}