using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Data
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string CardToken { get; set; } = null!;

        public decimal Balance { get; set; }

        public decimal? CreditLimit { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
