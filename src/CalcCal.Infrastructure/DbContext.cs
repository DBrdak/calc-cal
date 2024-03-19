using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CalcCal.Infrastructure
{
    public sealed class DbContext
    {
        public IMongoCollection<Food> Food { get; }
        public IMongoCollection<User> Users { get; }

        public DbContext(IConfiguration configuration)
        {
            var client = new MongoClient(
                configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(
                configuration["DatabaseSettings:DatabaseName"]);

            Food = database.GetCollection<Food>(
                configuration["DatabaseSettings:Collections:Food"]);
            Users = database.GetCollection<User>(
                configuration["DatabaseSettings:Collections:Users"]);
        }
    }
}
