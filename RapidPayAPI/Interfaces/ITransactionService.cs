using RapidPayAPI.Models;

namespace RapidPayAPI.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> ProcessTransactionAsync(string cardNumber, decimal amount);
    }
}
