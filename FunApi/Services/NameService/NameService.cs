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
        private ApiDBContext _context;

        public NameService(ApiDBContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Name>> AddName(Name newName)
        {
            ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
            ServiceResponse<bool> validationResponse = new ServiceResponse<bool>();
            validationResponse = ValidateName(newName.name);
            if (!validationResponse.Data)
            {
                serviceResponse.Message = validationResponse.Message;
                serviceResponse.Success = validationResponse.Success;
                return serviceResponse;
            }
            var names = await _context.Names.ToListAsync();
            foreach (var savedName in names)
            {
                if (newName.name == savedName.name)
                {
                    serviceResponse.Message = Messages.PostFailedUserExist;
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
            }
            serviceResponse.Data = newName;
            serviceResponse.Message = Messages.PostSuccess;

            _context.Names.Add(newName);
            await _context.SaveChangesAsync();

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Name>>> GetAllNames()
        {
            ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
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

        public async Task<ServiceResponse<Name>> GetName(string name)
        {
            ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
            var nameObject = await _context.Names.FirstOrDefaultAsync(c => c.name == name);
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

        public ServiceResponse<bool> ValidateName(string name)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            if(!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                if(Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$"))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = false;
                    serviceResponse.Message = Messages.ElonMuskException;
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
