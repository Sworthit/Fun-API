using FunApi.Context;
using FunApi.Model;
using FunApi.Services.NameService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public class NameServiceTest : TestBase
    {
        private NameService _serviceUnderTest;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _serviceUnderTest = new NameService(InMemoryDatabase);
        }

        [Fact]
        public async Task AddName_IfGivenName_ContainsOnlyLetters_ShouldReturn_NewName()
        {
            // Arrange
            var nameObj = new Name { name = "Maciek" };

            // Act
            var result = await _serviceUnderTest.AddName(nameObj);
            
            // Assert
            result.Data.ShouldBe(nameObj);          
        }

        [Theory]
        [InlineData("N4me")]
        [InlineData("Na!me")]
        [InlineData("34543")]
        [InlineData("#@#%%$#")]
        [InlineData("Na me")]
        public async Task AddName_IfGivenName_ContainsInvalidSigns_ShouldReturn_Null(string badName)
        {
            // Arrange
            var nameObj = new Name { name = badName };

            // Act
            var result = await _serviceUnderTest.AddName(nameObj);

            // Assert
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetAllNames_IfDatabase_IsEmpty_ShouldReturn_EmptyList()
        {
            // Arrange
            InMemoryDatabase.Names.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetAllNames();

            // Assert
            result.Data.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAllNames_IfDatabase_IsNotEmpty_ShouldReturn_ListOfNames()
        {
            // Arrange
            var firstName = new Name { name = "Firstname" };
            var secondName = new Name { name = "Secondname" };
            var lastName = new Name { name = "Lastname" };
            InMemoryDatabase.Names.Add(firstName);
            InMemoryDatabase.Names.Add(secondName);
            InMemoryDatabase.Names.Add(lastName);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetAllNames();

            // Assert
            result.Data.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData("Name")]
        public async Task GetName_IfDatabase_IsEmpty_ShouldReturn_Null(string name)
        {
            // Arrange
            InMemoryDatabase.Names.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetName(name);

            // Assert
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetName_IfDatabase_IsNotEmpty_ShouldReturn_Name()
        {
            // Arrange
            var name = new Name { name = "Maciek" };
            InMemoryDatabase.Names.Add(name);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetName(name.name);

            // Assert
            result.Data.ShouldBe(name);
        }
    }
}
