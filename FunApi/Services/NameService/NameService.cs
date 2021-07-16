using FunApi.Context;
using FunApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunApi.Services.NameService
{
    
    public class NameService : INameService
    {
        private ApiDBContext _context;

        public NameService(ApiDBContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Name>> AddName(Name newName)
        {
            ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
            var names = await _context.Names.ToListAsync();
            foreach (var savedName in names)
            {
                if (newName.name == savedName.name)
                {
                    serviceResponse.Message = "Name already exists in database";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
            }
            serviceResponse.Data = newName;

            _context.Names.Add(newName);
            await _context.SaveChangesAsync();

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Name>>> GetAllNames()
        {
            ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
            var names = await _context.Names.ToListAsync();

            serviceResponse.Data = names;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Name>> GetName(string name)
        {
            ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
            var nameObject = await _context.Names.FirstOrDefaultAsync(c => c.name == name);
            if (nameObject == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Name does not exists in the database";
                return serviceResponse;
            }

            serviceResponse.Data = nameObject;

            return serviceResponse;
        }
    }
}
