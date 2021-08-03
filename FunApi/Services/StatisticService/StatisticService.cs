using FunApi.Constants;
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
        private readonly ApiDbContext _context;
        public StatisticService(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<double>> GetAvgNameLength()
        {
            ServiceResponse<double> serviceResponse = new ServiceResponse<double>();
            var nameListAvg = await _context.GeneratedNames.AverageAsync(n => (int?)n.Name.Length);
            var nameListCount = await _context.GeneratedNames.CountAsync();

            if (!nameListAvg.HasValue || nameListCount == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetNamesFailed;
                return serviceResponse;
            }
            serviceResponse.Message = Messages.GetNamesAmount(nameListCount);
            serviceResponse.Data = nameListAvg.Value;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GeneratedName>> GetLongestName()
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();
            var longestName = await _context.GeneratedNames.OrderByDescending(n => n.Name.Length).FirstOrDefaultAsync();
            var nameListCount = await _context.GeneratedNames.CountAsync();

            if (longestName == null || nameListCount == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetNamesFailed;
                return serviceResponse;
            }
            serviceResponse.Data = longestName;
            serviceResponse.Message = Messages.GetSuccess;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GeneratedName>> GetShortestName()
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();
            var shortestName = await _context.GeneratedNames.OrderByDescending(n => n.Name.Length).LastOrDefaultAsync();
            var nameListCount = await _context.GeneratedNames.CountAsync();

            if (shortestName == null || nameListCount == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetNamesFailed;
                return serviceResponse;
            }
            serviceResponse.Data = shortestName;
            serviceResponse.Message = Messages.GetSuccess;

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GeneratedName>>> GetNamesGeneratedToday()
        {
            ServiceResponse<List<GeneratedName>> serviceResponse = new ServiceResponse<List<GeneratedName>>();
            var todayDate = DateTime.Now.ToUniversalTime();
            var nameList = await _context.GeneratedNames.Where(n => n.GeneratedDate.Date == todayDate.Date).ToListAsync();

            if (nameList.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetNamesFailed;
                return serviceResponse;
            }

            serviceResponse.Data = nameList;
            serviceResponse.Message = Messages.GetNamesSuccess;

            return serviceResponse;
        }
    }
}
