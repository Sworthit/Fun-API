using FunApi.Constants;
using FunApi.Context;
using FunApi.Controllers;
using FunApi.Model;
using FunApi.Services.GeneratorService;
using FunApi.Services.NameService;
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
    public class ControllerTest
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
    }
}
