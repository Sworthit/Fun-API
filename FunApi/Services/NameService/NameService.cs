using FunApi.Constants;
using FunApi.Context;
using FunApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FunApi.Services.NameService
{

    public class NameService : INameService
    {
        private ApiDbContext _context;

        public NameService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<NameModel>> AddName(NameModel newName)
        {
            ServiceResponse<NameModel> serviceResponse = new ServiceResponse<NameModel>();
            ServiceResponse<bool> validationResponse = new ServiceResponse<bool>();
            validationResponse = ValidateName(newName.Name);
            if (!validationResponse.Data)
            {
                serviceResponse.Message = validationResponse.Message;
                serviceResponse.Success = validationResponse.Success;
                return serviceResponse;
            }
            var nameInDb = await _context.Names.FirstOrDefaultAsync(n => n.Name == newName.Name);

            if (nameInDb != null)
            {
                serviceResponse.Message = Messages.PostFailedUserExist;
                serviceResponse.Success = false;
                return serviceResponse;
            }
            serviceResponse.Data = newName;
            serviceResponse.Message = Messages.PostSuccess;

            _context.Names.Add(newName);
            await _context.SaveChangesAsync();

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<NameModel>>> GetAllNames()
        {
            ServiceResponse<List<NameModel>> serviceResponse = new ServiceResponse<List<NameModel>>();
            var names = await _context.Names.ToListAsync();

            if (names == null)
            {
                serviceResponse.Message = Messages.GetNamesFailed;
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = names;
            serviceResponse.Message = Messages.GetNamesSuccess;

            return serviceResponse;
        }

        public async Task<ServiceResponse<NameModel>> GetName(string name)
        {
            ServiceResponse<NameModel> serviceResponse = new ServiceResponse<NameModel>();
            var nameObject = await _context.Names.FirstOrDefaultAsync(c => c.Name == name);
            if (nameObject == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetFailed;
                return serviceResponse;
            }

            serviceResponse.Data = nameObject;
            serviceResponse.Message = Messages.GetSuccess;

            return serviceResponse;
        }

        private ServiceResponse<bool> ValidateName(string name)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = false;
                    serviceResponse.Message = Messages.ElonMuskPunExceptionMessage;
                    return serviceResponse;
                }
                serviceResponse.Success = false;
                serviceResponse.Data = false;
                serviceResponse.Message = Messages.NameIsNotValid;
                return serviceResponse;
            }
            serviceResponse.Data = true;
            serviceResponse.Message = Messages.NameIsValid;

            return serviceResponse;
        }
    }
}
