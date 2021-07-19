using FunApi.Context;
using FunApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Services.GeneratorService
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ApiDBContext _context;

        private static int methodChooser = 0;

        public GeneratorService(ApiDBContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<GeneratedName>> GetGeneratedName(string name)
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();

            var nameObject = await _context.Names.FirstOrDefaultAsync(n => n.name == name);

            if (nameObject == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Could not find name in the database";
                return serviceResponse;
            }
            var generatedName = new GeneratedName();
            generatedName.Name = GenerateName(nameObject.name);

            serviceResponse.Data = generatedName;

            return serviceResponse;

        }

        private string GenerateName(string name)
        {
            switch(methodChooser)
            {
                case 0:
                    return GetPermutatedName(name);
                default:
                    methodChooser = 0;
                    break;
            }
            return "NONAME";     
        }

        private void FindPermutations(string word, out List<string> result)
        {
            result = new List<string>();

            if (word.Length == 1)
            {
                result.Add(word);
                return;
            }

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                string temp = word.Remove(i, 1);

                List<string> tempResult;
                FindPermutations(temp, out tempResult);

                foreach (var tempRes in tempResult)
                    result.Add(c + tempRes);
            }
        }

        private string GetPermutatedName(string name)
        {
            List<string> nameList = new List<string>();
            FindPermutations(name, out nameList);
            Random random = new Random();

            var nameListWithoutDuplicates = nameList.Distinct().ToArray();
            List<string> nameListWithProperLetters = new List<string>();
            foreach (var names in nameListWithoutDuplicates)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                nameListWithProperLetters.Add(textInfo.ToTitleCase(names));
            }
            if (nameList.Count >= 0)
            {
                int randomInt = random.Next(0, nameList.Count);
                return nameListWithProperLetters[randomInt];
            }
            else
                return "NO NAME FOUND";
        }
    }
}
