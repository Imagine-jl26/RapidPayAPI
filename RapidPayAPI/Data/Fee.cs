using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Data
{
    public class Fee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public decimal Multiplier { get; set; }

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
    }
}
