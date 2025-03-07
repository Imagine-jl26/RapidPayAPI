using RapidPayAPI.Interfaces;

namespace RapidPayAPI.Services
{
    public class FeeUpdater(IFeeService feeService) : BackgroundService
    {
        private readonly IFeeService _feeService = feeService ?? throw new ArgumentNullException(nameof(feeService));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _feeService.UpdateFee();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
