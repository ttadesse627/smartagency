using AppDiv.CRVS.Application.Contracts.DTOs;
using AppDiv.CRVS.Application.Features.Customers.Command.Create;
using AppDiv.CRVS.Application.Features.Customers.Command.Update;
using AppDiv.CRVS.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Mapper
{
    internal class CRVSMappingProfile:Profile
    {
        public CRVSMappingProfile() {

            CreateMap<Customer, CustomerResponseDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, EditCustomerCommand>().ReverseMap();
        }
    }
}
