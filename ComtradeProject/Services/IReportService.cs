using ComtradeProject.DTOs;
using ComtradeProject.Model;
using System.Security.Claims;

namespace ComtradeProject.Services
{
    public interface IReportService
    {
        Task<List<RewardedPersonDTO>> ReadFromCsv(string filePath);
    }
}
