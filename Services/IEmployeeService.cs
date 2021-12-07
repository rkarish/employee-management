using employee_management.Models;

namespace employee_management.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<GetEmployeeDto>>> GetAll();
        Task<ServiceResponse<GetEmployeeDto>> GetById(int id);
        Task<ServiceResponse<List<GetEmployeeDto>>> Add(AddEmployeeDto newEmployee);
        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteById(int id);
        Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedEmployee);
    }
}