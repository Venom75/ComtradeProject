using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using ComtradeProject.DTOs;
using ComtradeProject.Services;

namespace ComtradeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                return Ok(await _agentService.Register(registerDTO));
            }
            catch (Exception e)
            {

                return Conflict(e.Message);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            LoginResponseDTO token = new LoginResponseDTO();
            try
            {
                token = await _agentService.Login(loginDTO);

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
            return Ok(token);
        }
        [Authorize]
        [HttpPost("add-reward")]
        public async Task<IActionResult> AddReward(string personId, string reward) {

           var person = new PersonDTO();
            try
            {
                person = await _agentService.AddReward(personId, _agentService.GetUserId(User), reward);

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
            return Ok(person);
        }

        [Authorize]
        [HttpGet("get-report")]
        public async Task<IActionResult> GetReport()
        {

            try
            {
               return Ok(await _agentService.WriteToCsv());

            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
          
        }

    }
}
