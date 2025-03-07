using RapidPayAPI.Data;
using RapidPayAPI.Interfaces;

namespace RapidPayAPI.Services
{
    public class CardService(ApplicationDbContext context, ITokenizationService tokenizationService) : ICardService
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ITokenizationService _tokenizationService = tokenizationService ?? throw new ArgumentNullException(nameof(tokenizationService));

        private readonly Random _random = new Random();

        public async Task<(string CardNumber, Card Card)> CreateCardAsync(decimal? creditLimit)
        {
            string cardNumber = _tokenizationService.GenerateCardNumber();
            string token = _tokenizationService.GenerateCardToken(cardNumber);

            var card = new Card
            {
                CardToken = token,
                Balance = _random.Next(100, 1000),
                CreditLimit = creditLimit,
                IsActive = true
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return (cardNumber, card);
        }

        public async Task<Card?> GetCardByIdAsync(string id)
        {
            return await _context.Cards.FindAsync(id);
        }

        public async Task UpdateCardAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }
    }
}
