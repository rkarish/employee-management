using employee_management.Models;

namespace employee_management.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployee();
        Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id);
        Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee);
        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id);
        Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdatedEmployeeDto updatedEmployee);
    }

    public class EmployeeService : IEmployeeService
    {
        public Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdatedEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}