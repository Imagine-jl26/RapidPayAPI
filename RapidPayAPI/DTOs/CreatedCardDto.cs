namespace RapidPayAPI.DTOs
{
    public class CreatedCardDto
    {
        public string CardNumber { get; set; } = null!;
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
