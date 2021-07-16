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
    public class NameController : Controller
    {
        private readonly ApiDBContext _context;

        public NameController(ApiDBContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<List<Name>> GetAll()
        {
            return await _context.Names.ToListAsync();
        }

        [HttpPost("new")]
        public async Task<IActionResult> Post(Name name)
        {
            var names = await _context.Names.ToListAsync();
            foreach (var savedName in names)
            {
                if (name.name == savedName.name)
                {
                    return BadRequest("Name already exists"); 
                }
            }
            _context.Names.Add(name);
            var generatedName = new GeneratedName();
            generatedName.Name = name.name + "huehue";
            _context.GeneratedNames.Add(generatedName);
            await _context.SaveChangesAsync();
            return Ok(name);
        }

    }
}
