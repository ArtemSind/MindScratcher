using System;
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

    public async Task<OperationResult<List<Folder>>> GetAllFoldersAsync()
    {
        var foldersOperation = await _cardRepository.GetFoldersAsync();
        if (!foldersOperation.Success)
            return foldersOperation;

        return new OperationResult<List<Folder>>(foldersOperation.Value);
    }

    public async Task<OperationResult<Folder>> CreateFolder(Folder folder)
        => await _cardRepository.CreateFolderAsync(folder);
    
    public async Task<OperationResult> DeleteFolderWithCardsAsync(string folderName)
    {
        var operations = await Task.WhenAll(_cardRepository.DeleteFolderAsync(folderName),
            _cardRepository.DeleteCardsAsync(folderName));

        foreach (var operation in operations)
        {
            if (!operation.Success)
                return operation;
        }
        
        return OperationResult.Ok;
    }

    public async Task<OperationResult<Card>> InsertCardAsync(Card card)
        => await _cardRepository.InsertCardAsync(card);

    public async Task<OperationResult<List<Card>>> GetCardsAsync(string folderId = null)
        => await _cardRepository.GetCardsAsync(folderId);

    public async Task<OperationResult> DeleteCardAsync(string cardId)
        => await _cardRepository.DeleteCardAsync(cardId);
}