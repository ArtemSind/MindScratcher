using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using Microsoft.Extensions.Options;
using MindScratcher.Models;
using MindScratcher.Models.Options;
using MongoDB.Driver;

namespace MindScratcher.Repositories;

public class FolderRepository : MongoRepositoryBase
{
    private readonly IMongoCollection<Folder> _folders;
    
    public FolderRepository(IOptions<MongoOptions> options) : base(options.Value)
    {
        _folders = _db.GetCollection<Folder>(options.Value.FoldersCollectionName);
    }
    
    public async Task<OperationResult<Folder>> CreateFolderAsync(Folder folder)
    {
        await _folders.InsertOneAsync(folder);

        return new OperationResult<Folder>(folder);
    }

    public async Task<OperationResult> DeleteFolderAsync(string folderId)
    {
        var filter = Builders<Folder>.Filter.Eq(cardFolder => cardFolder.Id, folderId);
        await _folders.DeleteOneAsync(filter);
        
        return OperationResult.Ok;
    }

    public async Task<OperationResult<List<Folder>>> GetFoldersAsync(string folderId = null)
    {
        var filter = folderId is null 
            ? Builders<Folder>.Filter.Empty
            : Builders<Folder>.Filter.Eq(folder => folder.Id, folderId);
        var folders = await (await _folders.FindAsync(filter)).ToListAsync();

        return new OperationResult<List<Folder>>(folders);
    }
}