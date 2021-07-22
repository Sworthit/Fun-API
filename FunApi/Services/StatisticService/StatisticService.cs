using FunApi.Context;
using FunApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.StatisticService
{
    public class StatisticService : IStatisticService
    {
        private readonly ApiDBContext _context;
        public StatisticService(ApiDBContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<double>> GetAvgNameLength()
        {
            ServiceResponse<double> serviceResponse = new ServiceResponse<double>();
            var nameList = await _context.GeneratedNames.ToListAsync();

            if (nameList == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Could not fetch data";
                return serviceResponse;
            }
            serviceResponse.Message = "Avarage name length calculated, names found : " + nameList.Count.ToString();
            serviceResponse.Data = Statistic.CalculateAvgNameLength(nameList);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GeneratedName>> GetLongestName()
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();
            var nameList = await _context.GeneratedNames.ToListAsync();

            if (nameList == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Could not fetch data";
                return serviceResponse;
            }
            serviceResponse.Data = Statistic.GetLongestName(nameList);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GeneratedName>> GetShortestName()
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();
            var nameList = await _context.GeneratedNames.ToListAsync();

            if (nameList == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Could not fetch data";
                return serviceResponse;
            }
            serviceResponse.Data = Statistic.GetShortestName(nameList);

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GeneratedName>>> GetNamesGeneratedToday()
        {
            ServiceResponse<List<GeneratedName>> serviceResponse = new ServiceResponse<List<GeneratedName>>();
            var nameList = await _context.GeneratedNames.ToListAsync();

            if (nameList == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Could not fetch data";
                return serviceResponse;
            }
            serviceResponse.Data = Statistic.GetNamesGeneratedToday(nameList);

            return serviceResponse;
        }
    }
}
