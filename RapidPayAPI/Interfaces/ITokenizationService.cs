namespace RapidPayAPI.Interfaces
{
    public interface ITokenizationService
    {
        string GenerateCardToken(string cardNumber);
        string GenerateCardNumber();
    }
}
