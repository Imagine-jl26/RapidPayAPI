using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayAPI.Data
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("Card")]
        public Guid CardId { get; set; }

        public virtual Card Card { get; set; } = null!;
    }
}
