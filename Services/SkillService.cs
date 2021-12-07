using AutoMapper;
using employee_management.Data;
using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Services
{
    public class SkillService : ISkillService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public SkillService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> Add(AddSkillDto newSkill)
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();

            if (context != null)
            {
                Skill skill = mapper.Map<Skill>(newSkill);
                skill.DateCreated = DateTime.Now;
                context.Skills.Add(skill);
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Skills.Select(s => mapper.Map<GetSkillDto>(s)).ToListAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No data context.";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> Update(UpdateSkillDto updatedSkill)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();

            try
            {
                if (context != null)
                {
                    Skill? skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == updatedSkill.Id);
                    if (skill != null)
                    {
                        skill.Name = updatedSkill.Name;
                        skill.Description = updatedSkill.Description;
                        skill.DateUpdated = DateTime.Now;
                        await context.SaveChangesAsync();
                        serviceResponse.Data = mapper.Map<GetSkillDto>(skill);
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Skill not found.";
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No data context.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> DeleteById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();

            try
            {
                if (context != null)
                {
                    Skill? skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == id);
                    if (skill != null)
                    {
                        context.Skills.Remove(skill);
                        await context.SaveChangesAsync();
                        serviceResponse.Data = await context.Skills.Select(s => mapper.Map<GetSkillDto>(s)).ToListAsync();
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Skill not found.";
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No data context.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();

            try
            {
                if (context != null)
                {
                    serviceResponse.Data = await context.Skills.Select(s => mapper.Map<GetSkillDto>(s)).ToListAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No data context.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            try
            {
                if (context != null)
                {
                    Skill? skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == id);
                    if (skill != null)
                    {
                        serviceResponse.Data = mapper.Map<GetSkillDto>(skill);
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Skill not found.";
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No data context.";
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