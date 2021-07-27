using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunApi.Test
{
    public class ApiDBContextTest : DBTestContext
    {
        [Fact]
        public async Task Should_DatabaseBeAvailable_AndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }
    }
}
