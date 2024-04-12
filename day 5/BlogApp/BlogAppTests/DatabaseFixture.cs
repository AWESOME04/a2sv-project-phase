using System;
using BlogApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public DatabaseFixture()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        public void ResetDatabase()
        {
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
