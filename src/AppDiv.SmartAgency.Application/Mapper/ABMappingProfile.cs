using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Create;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Update;
using AppDiv.SmartAgency.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Mapper
{
    internal class SmartAgencyMappingProfile:Profile
    {
        public SmartAgencyMappingProfile() {

            CreateMap<Customer, CustomerResponseDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, EditCustomerCommand>().ReverseMap();
        }
    }
}
