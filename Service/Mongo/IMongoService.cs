using DomainClass.Mongo;

namespace Service.Mongo
{
    public interface IMongoService
    {

        Task<List<LoggerEntity>> GetAllAsync();
        Task<LoggerEntity?> GetByIdAsync(string id);
        Task CreateAsync(LoggerEntity newLog);
        Task UpdateAsync(string id, LoggerEntity updatedLog);

    }
}
