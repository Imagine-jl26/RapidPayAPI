namespace RapidPayAPI.Models.Card
{
    public class CreateCardRequest
    {
        public decimal? CreditLimit { get; set; }

        public bool IsValid(out string errorMessage)
        {
            if (CreditLimit.HasValue && CreditLimit <= 0)
            {
                errorMessage = "CreditLimit must be greater than zero.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
