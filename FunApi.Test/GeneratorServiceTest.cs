using FunApi.Model;
using FunApi.Services.GeneratorService;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public class GeneratorServiceTest : TestBase
    {
        private GeneratorService _serviceUnderTest;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _serviceUnderTest = new GeneratorService(InMemoryDatabase);
        }

        [Theory]
        [InlineData("Genname")]
        public async Task AddGeneratedName_IfGivenName_IsNotAlreadyBooked_ShouldReturn_NewName(string genName)
        {
            // Arrange
            var genNewName = new GeneratedName { Name = genName };

            // Act
            var result = await _serviceUnderTest.AddGeneratedName(genNewName);

            // Assert
            result.Data.ShouldBe(genNewName);
        }

        [Theory]
        [InlineData("Existingbookedname")]
        public async Task AddGeneratedName_IfGivenName_IsAlreadyBooked_ShouldReturn_Null(string existingBookedName)
        {
            // Arrange
            var genExistingName = new GeneratedName { Name = existingBookedName };
            InMemoryDatabase.GeneratedNames.Add(genExistingName);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.AddGeneratedName(genExistingName);

            // Assert
            result.Data.ShouldBeNull();
        }

        [Theory]
        [InlineData("Existinginnamesdb")]
        public async Task AddGeneratedName_IfGivenName_ExistsInNamesDb_ShouldReturn_Null(string existingInNamesDbName)
        {
            // Arrange
            var existingName = new NameModel { Name = existingInNamesDbName };
            var genName = new GeneratedName { Name = existingInNamesDbName };
            InMemoryDatabase.Names.Add(existingName);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.AddGeneratedName(genName);

            // Assert
            result.Data.ShouldBeNull();
        }

        [Theory]
        [InlineData("Maciekkeicam")]
        public async Task GetGeneratedName_IfGivenName_ExistsInDb_ShouldReturn_GeneratedName(string genNameString)
        {
            // Arrange
            var genName = new GeneratedName { Name = genNameString };
            InMemoryDatabase.GeneratedNames.Add(genName);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.CheckIfGeneratedNameExist(genNameString);

            // Assert
            result.Data.ShouldBe(genName);
        }
        
        [Fact]
        public async Task GetGeneratedName_IfGivenName_DoesNotExistsInDb_ShouldReturn_Null()
        {
            // Arrange
            var nonExistingName = "none";

            // Act
            var result = await _serviceUnderTest.CheckIfGeneratedNameExist(nonExistingName);

            // Assert
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetLuckyShotName_IfDatabase_IsEmpty_ShouldReturn_Null()
        {
            // Arrange
            InMemoryDatabase.GeneratedNames.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetLuckyShotName();

            // Assert
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetLuckyShotName_IfNameDatabase_IsNotEmpty_ShouldReturn_RandomName()
        {
            // Arrange
            var name = new NameModel { Name = "Maciek" };
            InMemoryDatabase.Names.Add(name);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetLuckyShotName();

            // Assert
            result.Data.Name.Length.ShouldBe(name.Name.Length);
        }
    }
}
