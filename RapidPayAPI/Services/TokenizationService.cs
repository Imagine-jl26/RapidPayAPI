using CreditCardValidator;
using RapidPayAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RapidPayAPI.Services
{
    public class TokenizationService : ITokenizationService
    {
        private const string SecretKey = "YourSuperSecretKey123";

        public string GenerateCardToken(string cardNumber)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(cardNumber));
            return Convert.ToBase64String(hash);
        }

        public string GenerateCardNumber()
        {
            return CreditCardFactory.RandomCardNumber(CardIssuer.Unknown, 15);
        }
    }
}
