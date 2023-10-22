using ComtradeProject.DTOs;
using ComtradeProject.Model;
using System.Security.Claims;

namespace ComtradeProject.Services
{
    public interface IReportService
    {
        List<RewardedPersonDTO> ReadFromCsv();
        Task<List<CombinedPersonDTO>> CombinedPerson();
    }
}
