using DataLayer.MongoDB;
using DomainClass.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Service.Mongo
{
    public class MongoService : IMongoService
    {

        private readonly MongoDbContext _context;
        public MongoService(MongoDbContext context) =>
            _context = context;

        public async Task<List<LoggerEntity>> GetAllAsync()
        {
            var logs = await _context.LoggerEntities.Find(_ => true).ToListAsync();
            foreach (var log in logs)
                log.CreateDateTime = log.CreateDateTime.ToLocalTime();
            return logs;
        }
        public async Task<LoggerEntity?> GetByIdAsync(string id)
        {
            var log = await _context.LoggerEntities.Find(entity => entity.Id == id).FirstOrDefaultAsync();
            log.CreateDateTime = log.CreateDateTime.ToLocalTime();
            return log;
        }

        public async Task CreateAsync(LoggerEntity newLog) =>
            await _context.LoggerEntities.InsertOneAsync(newLog);

        public async Task UpdateAsync(string id, LoggerEntity loggerEntity) =>
            await _context.LoggerEntities.ReplaceOneAsync(entity => entity.Id == id, loggerEntity);

        public async Task RemoveAsync(string id) =>
            await _context.LoggerEntities.DeleteOneAsync(entity => entity.Id == id);


        ///---------------------------------
        ///  روش قبلی بدون کانتکس
        //  private readonly IMongoCollection<LoggerEntity> _loggerCollection;
        //public MongoService(IOptions<LoggerDatabaseSettings> logDatabaseSettings, MongoDbContext context)
        //{
        //    var mongoClient = new MongoClient(
        //   logDatabaseSettings.Value.ConnectionString);

        //    var mongoDatabase = mongoClient.GetDatabase(
        //        logDatabaseSettings.Value.DatabaseName);

        //    _loggerCollection = mongoDatabase.GetCollection<LoggerEntity>(
        //        logDatabaseSettings.Value.LoggerCollectionName);

        //    _context = context;
        //}
        // public async Task<List<LoggerEntity>> GetAsync() =>
        //await _loggerCollection.Find(_ => true).ToListAsync();

        // public async Task<LoggerEntity?> GetAsync(string id) =>
        //     await _loggerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // public async Task CreateAsync(LoggerEntity newLog) =>
        //     await _loggerCollection.InsertOneAsync(newLog);

        // public async Task UpdateAsync(string id, LoggerEntity updatedLog) =>
        //     await _loggerCollection.ReplaceOneAsync(x => x.Id == id, updatedLog);

        // public async Task RemoveAsync(string id) =>
        //     await _loggerCollection.DeleteOneAsync(x => x.Id == id);


    }
}
