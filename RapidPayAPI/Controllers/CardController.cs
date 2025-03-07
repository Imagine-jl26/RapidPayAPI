using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.DTOs;
using RapidPayAPI.Extensions;
using RapidPayAPI.Interfaces;
using RapidPayAPI.Models.Card;

namespace RapidPayAPI.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardController(ICardService cardService, IUserService userService) : ControllerBase
    {
        private readonly ICardService _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        [HttpPost]
        public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardRequest request)
        {
            if (!request.IsValid(out var errorMessage))
            {
                return BadRequest(new { Message = errorMessage });
            }

            var createdResult = await _cardService.CreateCardAsync(request.CreditLimit);
            var card = createdResult.Card;

            return Ok(new CreatedCardDto
            {
                CardNumber = createdResult.CardNumber,
                Id = card.Id,
                Balance = card.Balance,
                CreditLimit = card.CreditLimit,
                IsActive = card.IsActive
            });
        }

        [HttpPost("{id}/authorize")]
        public async Task<IActionResult> IsAuthorizedCardAsync(string id)
        {
            var userId = User.GetId();
            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound(new { Message = "Card not found" });
            }

            if (!card.IsActive)
            {
                return BadRequest(new { Message = "Card is inactive" });
            }

            return Ok(new { Message = "Card is authorized for transactions." });
        }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetCardBalance(string id)
        {
            var userId = User.GetId();
            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound(new { Message = "Card not found" });
            }

            return Ok(new CardBalanceDto
            {
                Balance = card.Balance,
                CreditLimit = card.CreditLimit
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(string id, [FromBody] UpdateCardRequest request)
        {
            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound(new { Message = "Card not found" });
            }

            card.Balance = request.Balance ?? card.Balance;
            card.CreditLimit = request.CreditLimit ?? card.CreditLimit;
            card.IsActive = request.IsActive ?? card.IsActive;

            await _cardService.UpdateCardAsync(card);

            return Ok(new { Message = "Card updated successfully." });
        }
    }
}
