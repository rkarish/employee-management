using employee_management.Models;

namespace employee_management.Services
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAll();
        Task<ServiceResponse<GetSkillDto>> GetById(int id);
        Task<ServiceResponse<List<GetSkillDto>>> Add(AddSkillDto newSkill);
        Task<ServiceResponse<List<GetSkillDto>>> DeleteById(int id);
        Task<ServiceResponse<GetSkillDto>> Update(UpdateSkillDto updatedSkill);
    }
}