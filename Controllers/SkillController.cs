using employee_management.Models;
using employee_management.Services;
using Microsoft.AspNetCore.Mvc;

namespace employee_management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Skill>>>> Add(AddSkillDto skill)
        {
            return Ok(await skillService.Add(skill));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> GetById(int id)
        {
            return Ok(await skillService.GetById(id));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> GetAll()
        {
            return Ok(await skillService.GetAll());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> Update(UpdateSkillDto updatedSkill)
        {
            return Ok(await skillService.Update(updatedSkill));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> DeleteById(int id)
        {
            return Ok(await skillService.DeleteById(id));
        }
    }
}