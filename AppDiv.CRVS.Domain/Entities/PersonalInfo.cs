
using AppDiv.CRVS.Domain.Base;

namespace AppDiv.CRVS.Domain.Entities{
    public class PersonalInfo : BaseAuditableEntity{
        public string FirstNameStr { get; set; }
        public string MiddleNameStr { get; set; }
        public string LastNameStr { get; set;}
        public DateTime BirthDate { get; set; }
        public string GenderId { get; set; }



        
    }
}