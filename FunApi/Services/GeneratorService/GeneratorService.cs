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
            var generatedDate = DateTime.UtcNow;
            generatedName.GeneratedDate = generatedDate;

            serviceResponse.Data = generatedName;

            return serviceResponse;

        }

        private string GenerateName(string name)
        {
            switch(methodChooser)
            {
                case 0:
                    return GetPermutatedName(name);
                case 1:
                    return GetMirroredName(name);
                case 2:
                    return GetReversedName(name);
                default:
                    methodChooser = 0;
                    return GetPermutatedName(name);
            }
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

            if (nameList.Count >= 0)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                int randomInt = random.Next(0, nameList.Count);
                methodChooser++;
                return textInfo.ToTitleCase(nameListWithoutDuplicates[randomInt]);
            }
            else
                methodChooser++;
                return "NO NAME FOUND";

        }

        private string GetMirroredName(string name)
        {
            char[] nameArray = name.ToCharArray();
            int j = 0;
            for (int i = nameArray.Length - 1; i > nameArray.Length/2; i-- )
            {
                nameArray[i] = char.ToLower(nameArray[j]);
                j++;
            }
            methodChooser++;
            return new string(nameArray); 
        }

        private string GetReversedName(string name)
        {
            char[] nameArray = name.ToCharArray();
            Array.Reverse(nameArray);
            for (int i = 0; i < nameArray.Length; i++)
            {
                if (i == 0)
                    nameArray[i] = char.ToUpper(nameArray[i]);
                else
                    nameArray[i] = char.ToLower(nameArray[i]);
            }
            methodChooser++;
            return new string(nameArray);
        }
    }
}
