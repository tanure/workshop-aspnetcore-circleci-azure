using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using aspnetcore;
using Xunit;

namespace test
{
    [Collection("Base Collection")]
    public abstract class BaseIntegrationTest
    {
        protected readonly TestServer Server;

        protected readonly HttpClient Client;

        protected readonly DataContext TestDataContext;

        protected BaseTestFixture Fixture { get; }

        protected BaseIntegrationTest(BaseTestFixture fixture)
        {
            Fixture = fixture;

            TestDataContext = Fixture.TestDataContext;
            Client = Fixture.Client;
            Server = Fixture.Server;

            ClearDb().Wait();
        }

        public async Task ClearDb()
        {
            var commands = new[]
            {
                "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                "EXEC sp_MSForEachTable 'DELETE FROM ?'",
                "EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'"
            };

            await TestDataContext.Database.OpenConnectionAsync();

            foreach(var command in commands)
            {
                await TestDataContext.Database.ExecuteSqlCommandAsync(command);
            }

            TestDataContext.Database.CloseConnection();
        }
    }
}