using FunApi.Constants;
using FunApi.Context;
using FunApi.Controllers;
using FunApi.Model;
using FunApi.Services.GeneratorService;
using FunApi.Services.NameService;
using FunApi.Services.StatisticService;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public class ControllerTest : DBTestContext
    {
        
        [Fact]
        public async Task Should_NameController_ReturnName()
        {
            // Arrange
            var nameObj = new ServiceResponse<Name>
            {
                Data = new Name
                {
                    name = "Mat"
                }
            };
            var nameServiceMock = new Mock<INameService>();
            nameServiceMock.Setup(n => n.GetName(nameObj.Data.name)).ReturnsAsync(nameObj);

            NameController nameController = new NameController(nameServiceMock.Object);

            // Act
            var result = await nameController.GetByName(nameObj.Data.name);
            // Assert
            Assert.Equal(nameObj.Data.name, result.Data.name);
        }

        [Fact]
        public async Task Should_NameController_ReturnNoNameFound()
        {
            // Arrange
            var nameObj = new ServiceResponse<Name>
            {
                Data = new Name
                {
                    name = "Nonexistingname"
                },
                Success = false
            };

            var nameServiceMock = new Mock<INameService>();
            nameServiceMock.Setup(n => n.GetName(nameObj.Data.name)).ReturnsAsync(nameObj);

            NameController nameController = new NameController(nameServiceMock.Object);

            // Act
            var result = await nameController.GetByName(nameObj.Data.name);
            Assert.Equal(nameObj.Success, result.Success);
        }

        [Fact]
        public async Task Should_GeneratorController_ReturnNameIsNotUnique()
        {
            // Arrange
            var nameObj = new ServiceResponse<GeneratedName>
            {
                Data = new GeneratedName
                {
                    Name = "Mat"
                },
                Success = false,
                Message = Messages.NameExistsInNameDatabase
            };

            var generatorServiceMock = new Mock<IGeneratorService>();
            generatorServiceMock.Setup(g => g.AddGeneratedName(nameObj.Data)).ReturnsAsync(nameObj);
            GeneratorController generatorController = new GeneratorController(generatorServiceMock.Object);

            // Act
            var result = await generatorController.SaveGeneratedName(nameObj.Data);
            // Assert
            Assert.Equal(nameObj.Message, result.Message);
        }

        [Fact]
        public async Task Should_GeneratorController_ReturnNameAlreadyExistsResponse()
        {
            // Arrange
            var alreadyBookedNameObj = new ServiceResponse<GeneratedName>
            {
                Data = new GeneratedName
                {
                    Name = "Januszex"
                },
                Success = false
            };

            var generatorServiceMock = new Mock<IGeneratorService>();
            generatorServiceMock.Setup(g => g.AddGeneratedName(alreadyBookedNameObj.Data)).ReturnsAsync(alreadyBookedNameObj);

            GeneratorController generatorController = new GeneratorController(generatorServiceMock.Object);

            // Act
            var result = await generatorController.SaveGeneratedName(alreadyBookedNameObj.Data);
            // Assert
            Assert.Equal(alreadyBookedNameObj.Success, result.Success);
        }

        [Fact]
        public async Task Should_ServiceController_ReturnAvgNameLength()
        {
            // Arrange
            var generatedName = new GeneratedName()
            {
                Name = "Test"
            };
            DbContext.GeneratedNames.Add(generatedName);
            DbContext.SaveChanges();
            var generatedNameResponse = new ServiceResponse<double>()
            {
                Data = generatedName.Name.Length
            };
            var statisticServiceMock = new Mock<IStatisticService>();
            statisticServiceMock.Setup(s => s.GetAvgNameLength()).ReturnsAsync(generatedNameResponse);

            StatisticController statisticController = new StatisticController(statisticServiceMock.Object);

            // Act
            var result = await statisticController.GetAvgNameLength();
            // Assert
            Assert.Equal(generatedName.Name.Length, result.Data);
        }

        [Fact]
        public async Task Should_ServiceController_ReturnLongestName()
        {
            // Arrange
            var shortName = new GeneratedName()
            {
                Name = "Short"
            };
            var longName = new GeneratedName()
            {
                Name = "Longer"
            };
            var longestName = new GeneratedName()
            {
                Name = "Longestest"
            };
            var generatedResponse = new ServiceResponse<GeneratedName>()
            {
                Data = longestName
            };
            DbContext.GeneratedNames.Add(shortName);
            DbContext.GeneratedNames.Add(longName);
            DbContext.GeneratedNames.Add(longestName);
            DbContext.SaveChanges();
            var statisticServiceMock = new Mock<IStatisticService>();
            statisticServiceMock.Setup(s => s.GetLongestName()).ReturnsAsync(generatedResponse);

            StatisticController statisticController = new StatisticController(statisticServiceMock.Object);

            // Act
            var result = await statisticController.GetLongestName();
            // Assert
            Assert.Equal(longestName.Name, result.Data.Name);
        }

        [Fact]
        public async Task Should_ServiceController_ReturnShortestName()
        {
            // Arrange
            var shortName = new GeneratedName()
            {
                Name = "Short"
            };
            var longName = new GeneratedName()
            {
                Name = "Longer"
            };
            var longestName = new GeneratedName()
            {
                Name = "Longestest"
            };
            var generatedResponse = new ServiceResponse<GeneratedName>()
            {
                Data = shortName
            };
            DbContext.GeneratedNames.Add(shortName);
            DbContext.GeneratedNames.Add(longName);
            DbContext.GeneratedNames.Add(longestName);
            DbContext.SaveChanges();
            var statisticServiceMock = new Mock<IStatisticService>();
            statisticServiceMock.Setup(s => s.GetShortestName()).ReturnsAsync(generatedResponse);

            StatisticController statisticController = new StatisticController(statisticServiceMock.Object);

            // Act
            var result = await statisticController.GetShortestName();
            // Assert
            Assert.Equal(shortName.Name, result.Data.Name);
        }

        [Fact]
        public async Task Should_ServiceController_ReturnNamesBookedToday()
        {
            // Arrange
            var shortName = new GeneratedName()
            {
                Name = "Short",
                GeneratedDate = new DateTime()
            };
            var longName = new GeneratedName()
            {
                Name = "Longer",
                GeneratedDate = DateTime.Now.ToUniversalTime()
        };
            var longestName = new GeneratedName()
            {
                Name = "Longestest",
                GeneratedDate = DateTime.Now.ToUniversalTime()
            };
            var todaysNameList = new List<GeneratedName>();
            todaysNameList.Add(longName);
            todaysNameList.Add(longestName);
            var generatedResponse = new ServiceResponse<List<GeneratedName>>()
            {
                Data = todaysNameList
            };
            DbContext.GeneratedNames.Add(shortName);
            DbContext.GeneratedNames.Add(longName);
            DbContext.GeneratedNames.Add(longestName);
            DbContext.SaveChanges();
            var statisticServiceMock = new Mock<IStatisticService>();
            statisticServiceMock.Setup(s => s.GetNamesGeneratedToday()).ReturnsAsync(generatedResponse);

            StatisticController statisticController = new StatisticController(statisticServiceMock.Object);

            // Act
            var result = await statisticController.GetNamesGeneratedToday();
            // Assert
            Assert.Equal(todaysNameList.Count, result.Data.Count);
        }
    }
}
