using ComtradeProject.DTOs;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ComtradeProject.Services;

namespace ComtradeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        private readonly IReportService _reportService;
        string filePath = "Reports/report.csv";
        public ReportServiceController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("read-from-csv")]
        public IActionResult GetCsvData()
        {
            try
            {
                return Ok(_reportService.ReadFromCsv(filePath));
            }
            catch (Exception e)
            {
                return BadRequest("Failed to read and return CSV data: " + e.Message);
            }
        }
    }
}
