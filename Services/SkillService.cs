using AutoMapper;
using employee_management.Models;

namespace employee_management.Services
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<GetSkillDto>> GetSkillById(int id);
        Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill);
        Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill(int id);
        Task<ServiceResponse<GetSkillDto>> UpdateSkill(UpdateSkillDto updatedSkill);
    }

    public class SkillService : ISkillService
    {
        private static List<Skill> skills = new List<Skill>
        {
            new Skill(),
            new Skill{Id = 1, Name = "Python", Description = "Developer", DateCreated = DateTime.Now}
        };

        private readonly IMapper mapper;

        public SkillService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        {
            Skill skill = mapper.Map<Skill>(newSkill);
            skill.Id = skills.Max(s => s.Id) + 1;
            skill.DateCreated = DateTime.Now;
            skills.Add(skill);

            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            serviceResponse.Data = skills.Select(s => mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> UpdateSkill(UpdateSkillDto updatedSkill)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();

            try
            {
                Skill? skill = skills.FirstOrDefault(s => s.Id == updatedSkill.Id);

                if (skill != null)
                {
                    skill.Name = updatedSkill.Name;
                    skill.Description = updatedSkill.Description;
                    skill.DateUpdated = DateTime.Now;
                    serviceResponse.Data = mapper.Map<GetSkillDto>(skill);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            serviceResponse.Data = skills.Select(s => mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> GetSkillById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            serviceResponse.Data = mapper.Map<GetSkillDto>(skills.FirstOrDefault(s => s.Id == id));
            return serviceResponse;
        }
    }
}