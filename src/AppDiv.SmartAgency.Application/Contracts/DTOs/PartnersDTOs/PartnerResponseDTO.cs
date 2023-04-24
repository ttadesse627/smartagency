using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs
{
    public class PartnerResponseDTO
    {
      
      public string  Id {get; set;}   
      public string  PartnerType {get; set;}
      public string  PartnerName {get; set;}
      public string  PartnerNameAmharic {get; set;}
      public string  PartnerNameArabic {get; set;}
      public string  ContactPerson {get; set;}
      public string  IdNumber {get; set;}   
      public string  ManagerNameAmharic {get; set;}
      public string  LicenseNumber {get; set;}
      public string  BankName {get; set;} 
      public string  BankAccount {get; set;} 
      public string  HeaderLogo {get; set;}
      public string  ReferenceNumber {get; set;} 
      public string  Address {get; set;}
    }
}