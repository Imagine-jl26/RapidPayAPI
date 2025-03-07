using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data;
using RapidPayAPI.Interfaces;
using RapidPayAPI.Models;

namespace RapidPayAPI.Services
{
    public class TransactionService(ApplicationDbContext context, IFeeService feeService, ITokenizationService tokenizationService) : ITransactionService
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IFeeService _feeService = feeService ?? throw new ArgumentNullException(nameof(feeService));
        private readonly ITokenizationService _tokenizationService = tokenizationService ?? throw new ArgumentNullException(nameof(tokenizationService));

        public async Task<bool> ProcessTransactionAsync(string cardNumber, decimal amount)
        {
            string token = _tokenizationService.GenerateCardToken(cardNumber);

            var card = await _context.Cards.FirstOrDefaultAsync(c => c.CardToken == token);

            if (card == null || !card.IsActive)
            {
                return false;
            }

            decimal transactionFee = _feeService.GetCurrentFee();
            decimal totalAmount = amount + transactionFee;

            if (card.Balance + (card.CreditLimit ?? 0) < totalAmount)
            {
                return false;
            }

            card.Balance -= totalAmount;

            var transaction = new Transaction
            {
                CardId = card.Id,
                Amount = amount,
                Fee = transactionFee,
                Timestamp = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
