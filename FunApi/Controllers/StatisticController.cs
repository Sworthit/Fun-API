using FunApi.Context;
using FunApi.Model;
using FunApi.Services.StatisticService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _service;

        public StatisticController(IStatisticService service)
        {
            _service = service;
        }
        
        [HttpGet("avg")]
        public async Task<ServiceResponse<double>> GetAvgNameLength()
        {
            return await _service.GetAvgNameLength();
        }

        [HttpGet("shortest")]
        public async Task<ServiceResponse<GeneratedName>> GetShortestName()
        {
            return await _service.GetShortestName();
        }

        [HttpGet("longest")]
        public async Task<ServiceResponse<GeneratedName>> GetLongestName()
        {
            return await _service.GetLongestName();
        }

        [HttpGet("today")]
        public async Task<ServiceResponse<List<GeneratedName>>> GetNamesGeneratedToday()
        {
            return await _service.GetNamesGeneratedToday();
        }
    }
}
