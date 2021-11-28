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
        public async Task<ActionResult<ServiceResponse<List<Skill>>>> AddSkill(AddSkillDto skill)
        {
            return Ok(await skillService.AddSkill(skill));
        }
    }
}