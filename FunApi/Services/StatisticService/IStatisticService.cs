﻿using FunApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.StatisticService
{
    public interface IStatisticService
    {
        Task<ServiceResponse<double>> GetAvgNameLength();
        Task<ServiceResponse<GeneratedName>> GetLongestName();
        Task<ServiceResponse<GeneratedName>> GetShortestName();
        Task<ServiceResponse<List<GeneratedName>>> GetNamesGeneratedToday();
    }
}
