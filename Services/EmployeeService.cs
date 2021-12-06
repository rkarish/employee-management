using employee_management.Models;

namespace employee_management.Services
{
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

        public Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}