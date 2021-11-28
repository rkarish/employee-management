using employee_management.Models;

namespace employee_management.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<ServiceResponse<List<Employee>>> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Employee>>> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Employee>>> GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Employee>> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Employee>> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}