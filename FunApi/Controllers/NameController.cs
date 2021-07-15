using FunApi.Context;
using FunApi.Model;
using Microsoft.AspNetCore.Mvc;
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
        public List<Name> Get()
        {
            return _context.Names.ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
