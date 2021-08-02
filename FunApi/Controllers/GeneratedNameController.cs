using FunApi.Context;
using FunApi.Model;
using FunApi.Services.GeneratorService;
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
    public class GeneratorController : Controller
    {
        private readonly IGeneratorService _generatorService;
        public GeneratorController(IGeneratorService generatorService)
        {
            _generatorService = generatorService;
        }
        [HttpGet("{name}")]
        public async Task<ServiceResponse<GeneratedName>> Get(string name)
        {
            return await _generatorService.CheckIfGeneratedNameExist(name);
        }

        [HttpPost]
        public async Task<ServiceResponse<GeneratedName>> SaveGeneratedName(GeneratedName generatedName)
        {
            return await _generatorService.AddGeneratedName(generatedName);
        }

        [HttpGet("luckyshot")]
        public async Task<ServiceResponse<GeneratedName>> GetLuckShotName()
        {
            return await _generatorService.GetLuckyShotName();
        }
    }
}
