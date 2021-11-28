using AutoMapper;
using employee_management.Models;

namespace employee_management
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Skill, GetSkillDto>();
            CreateMap<AddSkillDto, Skill>();
        }
    }
}