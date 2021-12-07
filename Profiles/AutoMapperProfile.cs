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

            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<AddEmployeeDto, Employee>();
        }
    }
}