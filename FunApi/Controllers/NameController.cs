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
    public class NameController : ControllerBase
    {
        private readonly INameService _service;

        public NameController(INameService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ServiceResponse<List<NameModel>>> GetAllNames()
        {
            return await _service.GetAllNames();
        }

        [HttpPost]
        public async Task<ServiceResponse<NameModel>> SaveName(NameModel name)
        {
            return await _service.AddName(name);
        }

        [HttpGet]
        public async Task<ServiceResponse<NameModel>> GetByName([FromRoute]string name)
        {
            return await _service.GetName(name);
        }

    }
}
