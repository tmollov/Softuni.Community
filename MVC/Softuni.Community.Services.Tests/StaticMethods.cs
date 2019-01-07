using System;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Services.Tests
{
    public static class StaticMethods
    {
        public static SuCDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SuCDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new SuCDbContext(dbOptions);
            return dbContext;
        }

        public static CustomUser GetTestUser()
        {
            var testUser = new CustomUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mail@mail.com",
                UserName = "TestUser",
                PasswordHash = "MySecretPass1",
            };
            return testUser;
        }
    }
}