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

            if (context == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
                return serviceResponse;
            }

            try
            {
                Employee employee = mapper.Map<Employee>(newEmployee);
                employee.DateCreated = DateTime.Now;
                await context.Employees.AddAsync(employee);
                foreach (var skillId in newEmployee.SkillIds)
                {
                    Skill? skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
                    if (skill == null)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Skill id {skillId} not found.";
                        return serviceResponse;
                    }
                    await context.EmployeeSkills.AddAsync(new EmployeeSkill
                    {
                        EmployeeId = employee.Id,
                        SkillId = skillId,
                        Employee = employee,
                        Skill = skill
                    });
                }
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Employees
                    .Include(e => e.EmployeeSkills)
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

            if (context == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
                return serviceResponse;
            }

            try
            {
                serviceResponse.Data = await context.Employees.Select(e => mapper.Map<GetEmployeeDto>(e)).ToListAsync();
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

            if (context == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
                return serviceResponse;
            }

            try
            {
                // Employee? employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                Employee? employee = await context.Employees.Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill).FirstOrDefaultAsync(e => e.Id == id);

                if (employee != null)
                {
                    serviceResponse.Data = mapper.Map<GetEmployeeDto>(employee);
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

        public async Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();

            if (context == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
                return serviceResponse;
            }

            try
            {
                Employee? employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                if (employee != null)
                {
                    employee.FirstName = updatedEmployee.FirstName;
                    employee.LastName = updatedEmployee.LastName;
                    employee.Details = updatedEmployee.Details;
                    employee.HiringDate = updatedEmployee.HiringDate;
                    employee.DateUpdated = DateTime.Now;
                    await context.SaveChangesAsync();
                    serviceResponse.Data = mapper.Map<GetEmployeeDto>(employee);
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
                Employee? employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
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