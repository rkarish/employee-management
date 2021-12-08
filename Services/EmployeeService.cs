using AutoMapper;
using employee_management.Data;
using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public EmployeeService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> Add(AddEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            try
            {
                Employee employee = mapper.Map<Employee>(newEmployee);
                employee.DateCreated = DateTime.Now;
                await context.Employees!.AddAsync(employee);
                foreach (var skillId in newEmployee.SkillIds!)
                {
                    Skill? skill = await context.Skills!.FirstOrDefaultAsync(s => s.Id == skillId);

                    if (skill == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Skill id {skillId} not found.";
                        return serviceResponse;
                    }

                    await context.EmployeeSkills!.AddAsync(new EmployeeSkill
                    {
                        EmployeeId = employee.Id,
                        SkillId = skillId,
                        Employee = employee,
                        Skill = skill
                    });
                }

                await context.SaveChangesAsync();

                serviceResponse.Data = await context.Employees!
                    .Include(e => e.EmployeeSkills!)
                    .ThenInclude(es => es.Skill)
                    .Select(e => mapper.Map<GetEmployeeDto>(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            try
            {
                serviceResponse.Data = await context.Employees!
                    .Include(e => e.EmployeeSkills!)
                    .ThenInclude(es => es.Skill)
                    .Select(e => mapper.Map<GetEmployeeDto>(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();

            try
            {
                Employee? employee = await context.Employees!
                    .Include(e => e.EmployeeSkills!)
                    .ThenInclude(es => es.Skill)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                }

                serviceResponse.Data = mapper.Map<GetEmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();

            try
            {
                Employee? employee = await context.Employees!.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                }

                HashSet<int> updatedSkillIds = new HashSet<int>(updatedEmployee.SkillIds!);

                // Check if each Skill exists.
                foreach (var skillId in updatedSkillIds)
                {
                    Skill? skill = await context.Skills!.FirstOrDefaultAsync(s => s.Id == skillId);

                    if (skill == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Skill id {skillId} not found.";
                        return serviceResponse;
                    }
                }

                // Get the current SkillId's for the Employee.
                List<int>? skillIds = await context.EmployeeSkills!
                    .Where(es => es.EmployeeId == updatedEmployee.Id)
                    .Select(es => es.SkillId)
                    .ToListAsync();

                // Add EmployeeSkill's that do not exist for the Employee.
                foreach (var skillId in updatedSkillIds)
                {
                    if (!skillIds.Contains(skillId))
                    {
                        Skill? skill = await context.Skills!.FirstOrDefaultAsync(s => s.Id == skillId);

                        await context.EmployeeSkills!.AddAsync(new EmployeeSkill
                        {
                            EmployeeId = employee!.Id,
                            SkillId = skillId,
                            Employee = employee,
                            Skill = skill
                        });
                    }
                }

                foreach (var skillId in skillIds)
                {
                    if (!updatedSkillIds.Contains(skillId))
                    {
                        EmployeeSkill? employeeSkill = await context.EmployeeSkills!.FirstOrDefaultAsync(es => es.EmployeeId == employee!.Id && es.SkillId == skillId);
                        context.EmployeeSkills!.Remove(employeeSkill!);
                    }
                }

                employee!.FirstName = updatedEmployee.FirstName;
                employee!.LastName = updatedEmployee.LastName;
                employee!.Details = updatedEmployee.Details;
                employee!.HiringDate = updatedEmployee.HiringDate;
                employee!.DateUpdated = DateTime.Now;

                await context.SaveChangesAsync();

                serviceResponse.Data = mapper.Map<GetEmployeeDto>(
                    await context.Employees!
                        .Include(e => e.EmployeeSkills!)
                        .ThenInclude(es => es.Skill)
                        .FirstOrDefaultAsync(e => e.Id == employee.Id)
                );
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> DeleteById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            if (context == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
                return serviceResponse;
            }

            try
            {
                Employee? employee = await context.Employees!.FirstOrDefaultAsync(e => e.Id == id);
                if (employee != null)
                {
                    context.Employees!.Remove(employee);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = await context.Employees.Select(e => mapper.Map<GetEmployeeDto>(e)).ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}