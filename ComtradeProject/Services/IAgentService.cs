using ComtradeProject.DTOs;
using ComtradeProject.Model;
using System.Security.Claims;

namespace ComtradeProject.Services
{
    public interface IAgentService
    {
        int GetUserId(ClaimsPrincipal claim);
        Task<LoginResponseDTO> Login(LoginDTO agent);
        Task<RegisterResponseDTO> Register(RegisterDTO register);
        Task<PersonDTO> AddReward(string personId, int agentId, string reward);
        Task<string> WriteToCsv();

    }
}
