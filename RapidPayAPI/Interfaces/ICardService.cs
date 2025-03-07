using RapidPayAPI.Data;

namespace RapidPayAPI.Interfaces
{
    public interface ICardService
    {
        Task<(string CardNumber, Card Card)> CreateCardAsync(decimal? creditLimit);
        Task<Card?> GetCardByIdAsync(string id);
        Task UpdateCardAsync(Card card);
    }
}
