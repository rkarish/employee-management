using employee_management.Models;

namespace employee_management.Services
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<GetSkillDto>> GetSkillById(int id);
        Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill);
        Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill(int id);
        Task<ServiceResponse<GetSkillDto>> UpdateSkill(AddSkillDto updatedSkill);
    }
}