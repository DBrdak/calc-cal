using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using CalcCal.Domain.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace CalcCal.Infrastructure
{
    public sealed class DbContext
    {
        private IMongoCollection<Food> Food { get; }
        private IMongoCollection<User> Users { get; }

        public DbContext(IConfiguration configuration, IWebHostEnvironment env)
        {
            var client = env.IsDevelopment() ?
                LocalConnection(configuration) :
                ProductionConnection(configuration);

            var database = client.GetDatabase(
                configuration["DatabaseSettings:DatabaseName"]);

            Food = database.GetCollection<Food>(
                configuration["DatabaseSettings:Collections:Food"]);
            Users = database.GetCollection<User>(
                configuration["DatabaseSettings:Collections:Users"]);
        }

        private MongoClient LocalConnection(IConfiguration configuration) => new MongoClient(
            configuration["DatabaseSettings:ConnectionString"]);

        private MongoClient ProductionConnection(IConfiguration configuration)
        {
            var settings = MongoClientSettings.FromConnectionString(configuration["MONGO_URL"]);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            return new MongoClient(settings);
        }

        public IMongoCollection<T> Set<T>() where T : Entity
        {
            return typeof(T) switch
            {
                var type when type == typeof(Food) => (IMongoCollection<T>)Food,
                var type when type == typeof(User) => (IMongoCollection<T>)Users,
                _ => throw new InvalidOperationException($"No such a collection: {typeof(T)}")
            };
        }
    }
}
