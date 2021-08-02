using FunApi.Constants;
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

        public async Task<ServiceResponse<GeneratedName>> AddGeneratedName(GeneratedName name)
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();

            var namesList = await _context.GeneratedNames.ToListAsync();
            var existingNameList = await _context.Names.ToListAsync();

            foreach (var generatedName in namesList)
            {
                if (name.Name == generatedName.Name)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.GeneratedNameFailed(generatedName.GeneratedDate);
                    return serviceResponse;
                }
            }
            foreach (var existingName in existingNameList)
            {
                if (name.Name == existingName.name)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = Messages.NameExistsInNameDatabase;
                    return serviceResponse;
                }
            }
            name.GeneratedDate = DateTime.Now.ToUniversalTime();
            _context.GeneratedNames.Add(name);
            await _context.SaveChangesAsync();

            serviceResponse.Data = name;
            serviceResponse.Message = Messages.GeneratedNameSuccess;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GeneratedName>> CheckIfGeneratedNameExist(string name)
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();

            var nameObject = await _context.GeneratedNames.FirstOrDefaultAsync(n => n.Name == name);

            if (nameObject == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.NameDoesNotExist;
                return serviceResponse;
            }

            serviceResponse.Message = Messages.GetNamesSuccess;
            serviceResponse.Data = nameObject;

            return serviceResponse;

        }

        public async Task<ServiceResponse<GeneratedName>> GetLuckyShotName()
        {
            ServiceResponse<GeneratedName> serviceResponse = new ServiceResponse<GeneratedName>();
            Random random = new Random();
            var nameList = await _context.Names.ToListAsync();
            int randomIndex = random.Next(nameList.Count);
            if (nameList.Count == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Messages.GetNamesFailed;
                return serviceResponse;
            }

            var generatedName = new GeneratedName();
            generatedName.Name = GenerateName(nameList[randomIndex].name);
            generatedName.GeneratedDate = DateTime.Now.ToUniversalTime();
            serviceResponse.Message = Messages.GenerateName;
            serviceResponse.Data = generatedName;

            return serviceResponse;
        }

        private string GenerateName(string name)
        {
            switch (methodChooser)
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
            for (int i = nameArray.Length - 1; i > nameArray.Length / 2; i--)
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
