using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetLookUpByCategory : IRequest<LookUp>
{
    public Category Category { get; private set; }
    public GetLookUpByCategory(Category Category)
    {
        this.Category = Category;
    }
}
}