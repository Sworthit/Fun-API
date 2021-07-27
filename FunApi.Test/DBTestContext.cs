using FunApi.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunApi.Test
{
    public abstract class DBTestContext : IDisposable
    {
        private const string InMemoryConnectionString = "Data Source=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ApiDBContext DbContext;

        protected DBTestContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApiDBContext>()
                .UseSqlite(_connection)
                .Options;
            DbContext = new ApiDBContext(options);
            DbContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _connection.Close();
        }
    }
}
