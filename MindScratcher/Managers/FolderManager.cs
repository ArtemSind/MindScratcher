using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using MindScratcher.Models;
using MindScratcher.Repositories;

namespace MindScratcher.Managers;

public class FolderManager
{
    private readonly FolderRepository _folderRepository;
    private readonly CardRepository _cardRepository;

    public FolderManager(FolderRepository folderRepository, 
        CardRepository cardRepository)
    {
        _folderRepository = folderRepository;
        _cardRepository = cardRepository;
    }

    public async Task<OperationResult<List<Folder>>> GetAllFoldersAsync()
    {
        var foldersOperation = await _folderRepository.GetFoldersAsync();
        if (!foldersOperation.Success)
            return foldersOperation;

        return new OperationResult<List<Folder>>(foldersOperation.Value);
    }

    public async Task<OperationResult<Folder>> GetFolderAsync(string folderId)
    {
        var folderOperation = await _folderRepository.GetFoldersAsync(folderId);
        if (!folderOperation.Success)
            return new(folderOperation);

        var folder = folderOperation.Value.FirstOrDefault();

        return new(folder);
    }

    public async Task<OperationResult<Folder>> CreateFolderAsync(Folder folder)
        => await _folderRepository.CreateFolderAsync(folder);
    
    public async Task<OperationResult> DeleteFolderWithCardsAsync(string folderId)
    {
        var operations = await Task.WhenAll(_folderRepository.DeleteFolderAsync(folderId),
            _cardRepository.DeleteCardsAsync(folderId));

        foreach (var operation in operations)
        {
            if (!operation.Success)
                return operation;
        }
        
        return OperationResult.Ok;
    }
}