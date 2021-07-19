using FunApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.GeneratorService
{
    public interface IGeneratorService
    {
        Task<ServiceResponse<GeneratedName>> GetGeneratedName(string name);
    }
}
