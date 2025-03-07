using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

[Route("api/transactions")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
    }

    [HttpPost("pay")]
    public async Task<IActionResult> PayWithCard([FromBody] TransactionRequest request)
    {
        var success = await _transactionService.ProcessTransactionAsync(request.CardNumber, request.Amount);

        if (!success)
        {
            return BadRequest(new { Message = "Transaction failed: insufficient funds or invalid card." });
        }

        return Ok(new { Message = "Payment successful." });
    }
}

public class TransactionRequest
{
    [Required]
    public string CardNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}
