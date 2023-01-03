using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using ATI.Services.Common.Swagger;
using Microsoft.AspNetCore.Mvc;
using MindScratcher.Managers;
using MindScratcher.Models;

namespace MindScratcher.Controllers;

public class FolderController : ControllerWithOpenApi
{
    private readonly FolderManager _folderManager;

    public FolderController(FolderManager folderManager)
    {
        _folderManager = folderManager;
    }
    
    [HttpPost("v1/folders")]
    public Task<IActionResult> CreateFolderAsync([FromBody] Folder folder)
        => 
            _folderManager.CreateFolderAsync(folder).AsActionResultAsync();

    [HttpGet("v1/folders")]
    public Task<IActionResult> GetAllFoldersAsync()
        => 
            _folderManager.GetAllFoldersAsync().AsActionResultAsync();
    
    [HttpGet("v1/folders/{folderId}")]
    public Task<IActionResult> GetFolderAsync([FromRoute] string folderId)
        => 
            _folderManager.GetFolderAsync(folderId).AsActionResultAsync();
    
    [HttpDelete("v1/folders")]
    public Task<IActionResult> DeleteFolderAsync([FromRoute] string folderId)
        =>
            _folderManager.DeleteFolderWithCardsAsync(folderId).AsActionResultAsync();
}