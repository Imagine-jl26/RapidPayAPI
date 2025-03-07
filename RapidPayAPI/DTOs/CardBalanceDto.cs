namespace RapidPayAPI.DTOs
{
    public class CardBalanceDto
    {
        public decimal Balance { get; set; }

        public decimal? CreditLimit { get; set; }
    }
}
