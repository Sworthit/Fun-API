using FunApi.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public abstract class TestBase : IAsyncLifetime
    {
        private readonly DbContextOptions<ApiDBContext> _options;
        protected ApiDBContext InMemoryDatabase { get; private set; }


        public TestBase()
        {
            _options = new DbContextOptionsBuilder<ApiDBContext>()
                 .UseInMemoryDatabase(databaseName: "Names")
                 .Options;
        }

        public virtual async Task DisposeAsync()
        {
            await InMemoryDatabase.DisposeAsync();
        }

        public virtual Task InitializeAsync()
        {
            InMemoryDatabase = new ApiDBContext(_options);
            return Task.CompletedTask;
        }
    }
}
