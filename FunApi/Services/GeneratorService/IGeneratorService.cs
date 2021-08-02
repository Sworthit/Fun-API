using FunApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.GeneratorService
{
    public interface IGeneratorService
    {
        Task<ServiceResponse<GeneratedName>> CheckIfGeneratedNameExist(string name);
        Task<ServiceResponse<GeneratedName>> AddGeneratedName(GeneratedName name);
        Task<ServiceResponse<GeneratedName>> GetLuckyShotName();
    }
}
