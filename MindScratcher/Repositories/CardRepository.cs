using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using Microsoft.Extensions.Options;
using MindScratcher.Models;
using MindScratcher.Models.Options;
using MongoDB.Driver;

namespace MindScratcher.Repositories;

public class CardRepository
{
    private readonly IMongoCollection<Card> _cards;
    private readonly IMongoCollection<Folder> _folders;

    public CardRepository(IOptions<MongoOptions> options)
    {
        var mongoOptions = options.Value;
        var db = new MongoClient(mongoOptions.ConnectionString).GetDatabase(mongoOptions.MindScratcherDbName);
        _cards = db.GetCollection<Card>(mongoOptions.CardsCollectionName);
        _folders = db.GetCollection<Folder>(mongoOptions.FoldersCollectionName);
    }

    public async Task<OperationResult<Card>> InsertCardAsync(Card card)
    {
        await _cards.InsertOneAsync(card);
        
        return new OperationResult<Card>(card);
    }

    public async Task<OperationResult<List<Card>>> GetCardsAsync(string folderId = null)
    {
        var filter = folderId is null
            ? Builders<Card>.Filter.Empty
            : Builders<Card>.Filter.Eq(card => card.Id, folderId);
        var cards = await (await _cards.FindAsync(filter)).ToListAsync();
        
        return new OperationResult<List<Card>>(cards);
    }

    public async Task<OperationResult> DeleteCardAsync(string cardId)
    {
        var filter = Builders<Card>.Filter.Eq(card => card.Id, cardId);
        await _cards.DeleteOneAsync(filter);

        return OperationResult.Ok;
    }
    
    public async Task<OperationResult> DeleteCardsAsync(string folderId)
    {
        var filter = Builders<Card>.Filter.Eq(card => card.FolderId, folderId);
        await _cards.DeleteManyAsync(filter);

        return OperationResult.Ok;
    }

    public async Task<OperationResult<Folder>> CreateFolderAsync(Folder folder)
    {
        await _folders.InsertOneAsync(folder);

        return new OperationResult<Folder>(folder);
    }

    public async Task<OperationResult> DeleteFolderAsync(string folderName)
    {
        var filter = Builders<Folder>.Filter.Eq(cardFolder => cardFolder.FolderName, folderName);
        await _folders.DeleteOneAsync(filter);
        
        return OperationResult.Ok;
    }

    public async Task<OperationResult<List<Folder>>> GetFoldersAsync()
    {
        var filter = Builders<Folder>.Filter.Empty;
        var folders = await (await _folders.FindAsync(filter)).ToListAsync();

        return new OperationResult<List<Folder>>(folders);
    }
}