using FunApi.Context;
using FunApi.Model;
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
    public class GeneratedNameController : Controller
    {
        private ApiDBContext _context;
        public GeneratedNameController(ApiDBContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<List<GeneratedName>> Get()
        {
            var allNames = await _context.GeneratedNames.ToListAsync();
            return allNames;
        }
    }
}
