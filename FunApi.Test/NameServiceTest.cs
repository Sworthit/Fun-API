using FunApi.Model;
using FunApi.Services.NameService;
using Shouldly;
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
            var nameObj = new NameModel { Name = "Maciek" };

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
            var nameObj = new NameModel { Name = badName };

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
            var firstName = new NameModel { Name = "Firstname" };
            var secondName = new NameModel { Name = "Secondname" };
            var lastName = new NameModel { Name = "Lastname" };
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
            var name = new NameModel { Name = "Maciek" };
            InMemoryDatabase.Names.Add(name);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetName(name.Name);

            // Assert
            result.Data.ShouldBe(name);
        }
    }
}
