namespace RapidPayAPI.Models.Card
{
    public class UpdateCardRequest
    {
        public decimal? Balance { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool? IsActive { get; set; }
    }
}
