using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using ATI.Services.Common.Swagger;
using Microsoft.AspNetCore.Mvc;
using MindScratcher.Managers;
using MindScratcher.Models;

namespace MindScratcher.Controllers;

public class CardController : ControllerWithOpenApi
{
    private readonly CardManager _cardManager;

    public CardController(CardManager cardManager)
    {
        _cardManager = cardManager;
    }

    [HttpPost("v1/cards")]
    public Task<IActionResult> InsertCardAsync([FromBody] Card card)
        => 
            _cardManager.InsertCardAsync(card).AsActionResultAsync();

    [HttpGet("v1/cards")]
    public Task<IActionResult> GetCardsAsync([FromRoute] string folderId = null)
        => 
            _cardManager.GetCardsAsync(folderId).AsActionResultAsync();

    [HttpDelete("v1/cards")]
    public Task<IActionResult> DeleteCardAsync([FromRoute] string cardId)
        => 
            _cardManager.DeleteCardAsync(cardId).AsActionResultAsync();
}