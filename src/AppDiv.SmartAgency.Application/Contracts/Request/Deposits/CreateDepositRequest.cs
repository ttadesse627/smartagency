namespace AppDiv.SmartAgency.Application.Contracts.Request.Deposits
{
    public class CreateDepositRequest
    {
        public string? PassportNumber { get; set; }
        public double DepositAmount { get; set; }
        public DateTime Month { get; set; }
        public string? DepositedBy { get; set; }

    }
}