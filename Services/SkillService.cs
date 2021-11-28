using AutoMapper;
using employee_management.Models;

namespace employee_management.Services
{
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
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            Skill skill = mapper.Map<Skill>(newSkill);
            skill.Id = skills.Max(s => s.Id) + 1;
            skills.Add(skill);
            serviceResponse.Data = skills.Select(s => mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }

        public Task<ServiceResponse<GetSkillDto>> UpdateSkill(AddSkillDto updatedSkill)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<List<GetSkillDto>>> ISkillService.DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<List<GetSkillDto>>> ISkillService.GetAllSkills()
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<GetSkillDto>> ISkillService.GetSkillById(int id)
        {
            throw new NotImplementedException();
        }
    }
}