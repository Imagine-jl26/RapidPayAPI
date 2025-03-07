namespace RapidPayAPI.Interfaces
{
    public interface IFeeService
    {
        decimal GetCurrentFee();
        void UpdateFee();
    }
}
