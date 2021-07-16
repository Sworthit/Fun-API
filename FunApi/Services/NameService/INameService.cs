
using FunApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.NameService
{
    public interface INameService
    {
        Task<ServiceResponse<List<Name>>> GetAllNames();

        Task<ServiceResponse<Name>> GetName(string name);

        Task<ServiceResponse<Name>> AddName(Name newName);
    }
}
