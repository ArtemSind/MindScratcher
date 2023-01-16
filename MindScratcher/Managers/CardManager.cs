using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using MindScratcher.Models;
using MindScratcher.Repositories;

namespace MindScratcher.Managers;

public class CardManager
{
    private readonly CardRepository _cardRepository;

    public CardManager(CardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<OperationResult<Card>> InsertCardAsync(Card card)
        => await _cardRepository.InsertCardAsync(card);

    public async Task<OperationResult<List<Card>>> GetCardsAsync(string folderId = null)
        => await _cardRepository.GetCardsAsync(folderId);

    public async Task<OperationResult> DeleteCardAsync(string cardId)
        => await _cardRepository.DeleteCardAsync(cardId);
}