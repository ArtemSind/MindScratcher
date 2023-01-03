using MindScratcher.Models.Options;
using MongoDB.Driver;

namespace MindScratcher.Repositories;

public abstract class MongoRepositoryBase
{
    protected readonly IMongoDatabase _db;
    protected MongoRepositoryBase(MongoOptions options)
    {
        _db = new MongoClient(options.ConnectionString).GetDatabase(options.MindScratcherDbName);
    }
    
}