using FunApi.Context;
using FunApi.Model;
using FunApi.Services.NameService;
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
        private readonly INameService _service;

        public NameController(INameService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ServiceResponse<List<Name>>> GetAll()
        {
            return await _service.GetAllNames();
        }

        [HttpPost]
        public async Task<ServiceResponse<Name>> Post(Name name)
        {
            return await _service.AddName(name);
        }

        [HttpGet("{name}")]
        public async Task<ServiceResponse<Name>> GetByName(string name)
        {
            return await _service.GetName(name);
        }

    }
}
