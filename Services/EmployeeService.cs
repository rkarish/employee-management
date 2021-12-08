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
                // Add the new Employee.
                Employee employee = mapper.Map<Employee>(newEmployee);
                employee.DateCreated = DateTime.Now;
                await context.Employees!.AddAsync(employee);

                foreach (var skillId in newEmployee.SkillIds!)
                {
                    // Check if each Skill exists.
                    Skill? skill = await context.Skills!.FirstOrDefaultAsync(s => s.Id == skillId);
                    if (skill == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Skill id {skillId} not found.";
                        return serviceResponse;
                    }

                    // Add the new EmployeeSkill's.
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
                    return serviceResponse;
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
                // Get the Employee and make sure it exists.
                Employee? employee = await context.Employees!.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                    return serviceResponse;
                }

                // Check if each Skill exists.
                foreach (var skillId in updatedEmployee.SkillIds!)
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
                foreach (var skillId in updatedEmployee.SkillIds!)
                {
                    if (!skillIds.Contains(skillId))
                    {
                        await context.EmployeeSkills!.AddAsync(new EmployeeSkill
                        {
                            EmployeeId = employee!.Id,
                            SkillId = skillId,
                            Employee = employee,
                            Skill = await context.Skills!.FirstOrDefaultAsync(s => s.Id == skillId)
                        });
                    }
                }

                // Remove EmployeeSkill's that are not in the update.
                foreach (var skillId in skillIds)
                {
                    if (!updatedEmployee.SkillIds.Contains(skillId))
                    {
                        EmployeeSkill? employeeSkill = await context.EmployeeSkills!.
                            FirstOrDefaultAsync(es => es.EmployeeId == employee!.Id && es.SkillId == skillId);
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

            try
            {
                Employee? employee = await context.Employees!.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                    return serviceResponse;
                }

                foreach (var employeeSkill in await context.EmployeeSkills!.Where(es => es.EmployeeId == employee.Id).ToListAsync())
                {
                    context.EmployeeSkills!.Remove(employeeSkill);
                }

                context.Employees!.Remove(employee!);
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Employees!.Select(e => mapper.Map<GetEmployeeDto>(e)).ToListAsync();

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