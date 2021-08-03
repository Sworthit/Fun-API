using FunApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public abstract class TestBase : IAsyncLifetime
    {
        private readonly DbContextOptions<ApiDbContext> _options;
        protected ApiDbContext InMemoryDatabase { get; private set; }


        public TestBase()
        {
            _options = new DbContextOptionsBuilder<ApiDbContext>()
                 .UseInMemoryDatabase(databaseName: "Names")
                 .Options;
        }

        public virtual async Task DisposeAsync()
        {
            await InMemoryDatabase.DisposeAsync();
        }

        public virtual Task InitializeAsync()
        {
            InMemoryDatabase = new ApiDbContext(_options);
            return Task.CompletedTask;
        }
    }
}
