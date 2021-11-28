using employee_management.Models;

namespace employee_management.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<Employee>>> GetAllEmployee();
        Task<ServiceResponse<Employee>> GetEmployeeById(int id);
        Task<ServiceResponse<List<Employee>>> AddEmployee(Employee employee);
        Task<ServiceResponse<List<Employee>>> DeleteEmployee(int id);
        Task<ServiceResponse<Employee>> UpdateEmployee(Employee employee);
    }
}