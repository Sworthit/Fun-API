using FunApi.Model;
using FunApi.Services.StatisticService;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public class StatisticServiceTest : TestBase
    {
        private StatisticService _serviceUnderTest;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            _serviceUnderTest = new StatisticService(InMemoryDatabase);
        }

        [Fact]
        public async Task GetAvgNameLength_IfDatabase_IsEmpty_ShouldReturn_Zero()
        {
            // Arrange
            InMemoryDatabase.GeneratedNames.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetAvgNameLength();

            // Assert
            result.Data.ShouldBe(0);
        }

        [Fact]
        public async Task GetAvgNameLength_IfDatabase_IsNotEmpty_ShouldReturn_AvgNameLength()
        {
            // Arrange
            var nameList = new List<GeneratedName>()
            {
                new GeneratedName
                {
                    Name = "First",
                    GeneratedDate = new DateTime()
                },
                new GeneratedName
                {
                    Name = "Longer",
                    GeneratedDate = DateTime.UtcNow
                }
            };
            InMemoryDatabase.GeneratedNames.AddRange(nameList);
            int sum = nameList.Select(x => x.Name.Length).Sum();
            double expectedAvg = sum / nameList.Count();
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetAvgNameLength();

            // Assert
            result.Data.ShouldBe(expectedAvg);
        }

        [Fact]
        public async Task GetLongestName_IfDatabase_IsEmpty_ShouldReturn_Null()
        {
            // Arrange
            InMemoryDatabase.GeneratedNames.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetLongestName();

            // 
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetLongestName_IfDatabase_IsNotEmpty_ShouldReturn_LongestName()
        {
            // Arrange
            var nameList = new List<GeneratedName>()
            {
                new GeneratedName
                {
                    Name = "First",
                    GeneratedDate = new DateTime()
                },
                new GeneratedName
                {
                    Name = "Longer",
                    GeneratedDate = DateTime.UtcNow
                }
            };
            InMemoryDatabase.GeneratedNames.AddRange(nameList);
            var longestName = nameList.OrderByDescending(x => x.Name.Length).FirstOrDefault();
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetLongestName();

            // Assert
            result.Data.ShouldBe(longestName);
        }

        [Fact]
        public async Task GetShortestName_IfDatabase_IsEmpty_ShouldReturn_Null()
        {
            // Arrange
            InMemoryDatabase.GeneratedNames.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetShortestName();

            // 
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetShortestName_IfDatabase_IsNotEmpty_ShouldReturn_ShortestName()
        {
            // Arrange
            var nameList = new List<GeneratedName>()
            {
                new GeneratedName
                {
                    Name = "First",
                    GeneratedDate = new DateTime()
                },
                new GeneratedName
                {
                    Name = "Longer",
                    GeneratedDate = DateTime.UtcNow
                }
            };
            InMemoryDatabase.GeneratedNames.AddRange(nameList);
            var shortestName = nameList.OrderByDescending(x => x.Name.Length).Last();
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetShortestName();

            // Assert
            result.Data.ShouldBe(shortestName);
        }

        [Fact]
        public async Task GetNamesGeneratedToday_IfDatabase_IsEmpty_ShouldReturn_Null()
        {
            // Arrange
            InMemoryDatabase.GeneratedNames.ShouldBeEmpty();

            // Act
            var result = await _serviceUnderTest.GetNamesGeneratedToday();

            // Assert
            result.Data.ShouldBeNull();
        }

        [Fact]
        public async Task GetNamesGeneratedToday_IfDatabase_IsNotEmpty_ShouldReturn_NamesGenerated_Today()
        {
            // Arrange
            var nameList = new List<GeneratedName>()
            {
                new GeneratedName
                {
                    Name = "First",
                    GeneratedDate = new DateTime()
                },
                new GeneratedName
                {
                    Name = "Longer",
                    GeneratedDate = DateTime.UtcNow
                }
            };
            InMemoryDatabase.GeneratedNames.AddRange(nameList);
            await InMemoryDatabase.SaveChangesAsync();
            var namesGeneratedToday = await InMemoryDatabase.GeneratedNames.Where(x => x.GeneratedDate.Date == DateTime.Today).ToListAsync();

            // Act
            var result = await _serviceUnderTest.GetNamesGeneratedToday();

            // Assert
            result.Data.ShouldBeEquivalentTo(namesGeneratedToday);
        }

        [Fact]
        public async Task GetNamesGeneratedToday_IfNoNames_WereGeneratedToday_ShouldReturn_EmptyList()
        {
            // Arrange
            var nameList = new List<GeneratedName>()
            {
                new GeneratedName
                {
                    Name = "First",
                    GeneratedDate = new DateTime()
                },

            };
            InMemoryDatabase.GeneratedNames.AddRange(nameList);
            await InMemoryDatabase.SaveChangesAsync();

            // Act
            var result = await _serviceUnderTest.GetNamesGeneratedToday();

            // Assert
            result.Data.ShouldBeEmpty();
        }

    }
}
