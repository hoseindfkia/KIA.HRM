using DomainClass.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly string _collecttion;

        public MongoDbContext(string connectionString, string databaseName, string collecttion)
        {
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(DateTimeKind.Utc));

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            _collecttion = collecttion;
        }

        public IMongoCollection<LoggerEntity> LoggerEntities => _database.GetCollection<LoggerEntity>(_collecttion);


        //private readonly LoggerDatabaseSettings _mySettings;
        //public MongoDbContext(IOptions<LoggerDatabaseSettings> mySettings)
        //{
        //    _mySettings = mySettings.Value;
        //    var client = new MongoClient(_mySettings.ConnectionString);
        //    _database = client.GetDatabase(_mySettings.DatabaseName);
        //}
    }

}
