using FunApi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services
{
    
    public class NameGeneratingService
    {
        private ApiDBContext _context;

        public NameGeneratingService(ApiDBContext context)
        {
            _context = context;
        }


    }
}
