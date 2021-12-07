using AutoMapper;
using employee_management.Models;

namespace employee_management.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Skill, GetSkillDto>();
            CreateMap<AddSkillDto, Skill>();

            CreateMap<Employee, GetEmployeeDto>().ForMember(dto => dto.Skills, e => e.MapFrom(e => e.EmployeeSkills.Select(es => es.Skill)));
            CreateMap<GetEmployeeDto, Employee>();

            CreateMap<AddEmployeeDto, Employee>();
        }
    }
}