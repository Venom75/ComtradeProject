using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using ComtradeProject.DTOs;
using ComtradeProject.Model;
using ComtradeProject.Repository;
using ServiceReference;
using System.Formats.Asn1;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComtradeProject.Services
{
    public class ReportService : IReportService
    {
        public async Task<List<RewardedPersonDTO>> ReadFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<RewardedPersonDTO>().ToList();
            }
        }
    }
}
