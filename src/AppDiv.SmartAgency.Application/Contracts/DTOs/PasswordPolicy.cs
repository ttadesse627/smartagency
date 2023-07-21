
namespace AppDiv.SmartAgency.Application.Contracts.DTOs
{
    public class PasswordPolicy
    {
        public bool Number { get; set; }
        public bool LowerCase { get; set; }
        public bool OtherChar { get; set; }
        public bool UpperCase { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}