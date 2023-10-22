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
        private readonly SOAPDemoSoapClient _soapClient;
        string filePath = "Reports/report.csv";

        public ReportService(SOAPDemoSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        public async Task<List<CombinedPersonDTO>> CombinedPerson()
        {
            List<RewardedPersonDTO> list = ReadFromCsv();
            List<CombinedPersonDTO> combinedList = new List<CombinedPersonDTO>();
            foreach (var person in list) {
                combinedList.Add(new CombinedPersonDTO() { 
                    CombinedPerson = await _soapClient.FindPersonAsync(person.PersonId.ToString()), 
                    Reward = person.Reward
                });
            }
            return combinedList;
        }
  
        public List<RewardedPersonDTO> ReadFromCsv()
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<RewardedPersonDTO>().ToList();
            }
        }
    }
}
