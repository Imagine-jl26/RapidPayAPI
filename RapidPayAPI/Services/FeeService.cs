using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data;
using RapidPayAPI.Interfaces;
using RapidPayAPI.Models;

namespace RapidPayAPI.Services
{
    public class FeeService(IServiceScopeFactory scopeFactory) : IFeeService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));

        private static decimal _currentFeeRate;
        private static readonly object _lock = new();

        public decimal GetCurrentFee()
        {
            lock (_lock)
            {
                return _currentFeeRate;
            }
        }

        public void UpdateFee()
        {
            lock (_lock)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var lastFee = dbContext.Fees.OrderByDescending(f => f.EffectiveFrom).FirstOrDefault();
                _currentFeeRate = lastFee?.Multiplier ?? 1.00m;

                var random = new Random();
                decimal multiplier = (decimal)(random.NextDouble() * 2);

                _currentFeeRate *= multiplier;

                if (_currentFeeRate < 0.01m)
                {
                    _currentFeeRate = 0.01m;
                }

                var newFee = new Fee
                {
                    Multiplier = _currentFeeRate,
                    EffectiveFrom = DateTime.UtcNow
                };

                dbContext.Fees.Add(newFee);
                dbContext.SaveChanges();

                Console.WriteLine($"[UFE] Fee updated: {_currentFeeRate:F2}");
            }
        }
    }
}
