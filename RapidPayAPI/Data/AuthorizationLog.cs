using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Data
{
    public class AuthorizationLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CardId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsAuthorized { get; set; }

        public string? Reason { get; set; }
    }
}
