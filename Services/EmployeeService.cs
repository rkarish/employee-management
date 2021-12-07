using employee_management.Models;

namespace employee_management.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<ServiceResponse<List<GetEmployeeDto>>> Add(AddEmployeeDto newEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetEmployeeDto>>> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetEmployeeDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetEmployeeDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}