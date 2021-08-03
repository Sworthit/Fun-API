using FunApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunApi.Services.NameService
{
    public interface INameService
    {
        Task<ServiceResponse<List<NameModel>>> GetAllNames();

        Task<ServiceResponse<NameModel>> GetName(string name);

        Task<ServiceResponse<NameModel>> AddName(NameModel newName);
    }
}
