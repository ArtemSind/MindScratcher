using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using Microsoft.Extensions.Options;
using MindScratcher.Models;
using MindScratcher.Models.Options;
using MongoDB.Driver;

namespace MindScratcher.Repositories;

public class CardRepository : MongoRepositoryBase
{
    private readonly IMongoCollection<Card> _cards;

    public CardRepository(IOptions<MongoOptions> options) : base(options.Value)
    {
        _cards = _db.GetCollection<Card>(options.Value.CardsCollectionName);
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
}