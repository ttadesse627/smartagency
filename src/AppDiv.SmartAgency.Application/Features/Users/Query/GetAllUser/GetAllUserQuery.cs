
// using AppDiv.CRVS.Application.Common;
// using AppDiv.CRVS.Application.Contracts.DTOs;
// using AppDiv.CRVS.Application.Interfaces;
// using AppDiv.CRVS.Application.Interfaces.Persistence;
// using AppDiv.CRVS.Application.Mapper;
// using AppDiv.CRVS.Domain;
// using AppDiv.CRVS.Domain.Entities;
// using AppDiv.CRVS.Domain.Repositories;
// using AutoMapper;
// using MediatR;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace AppDiv.CRVS.Application.Features.Lookups.Query.GetAllUser

// {
//     // Customer query with List<Customer> response
//     public record GetAllUserQuery : IRequest<PaginatedList<UserResponseDTO>>
//     {
//         public int? PageCount { set; get; } = 1!;
//         public int? PageSize { get; set; } = 10!;
//     }

//     public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PaginatedList<UserResponseDTO>>
//     {
//         private readonly IIdentityService _identityService;
//         // private readonly IMapper _mapper;

//         public GetAllUserQueryHandler(IIdentityService identityService)
//         {
//             _identityService = identityService;
//             // _mapper = mapper;
//         }
//         public async Task<PaginatedList<UserResponseDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
//         {
//             return await PaginatedList<UserResponseDTO>
//              .CreateAsync(
//                  _identityService
//                 .AllUsersDetail()
//                 .Select(user => new UserResponseDTO
//                 {
//                     Id = user.Id,
//                     UserName = user.UserName,
//                     Email = user.Email,
//                     AddressId = user.AddressId,
//                     PersonalInfo = new PersonalInfoDTO
//                     {
//                         Id = user.PersonalInfo.Id,
//                         FirstName = user.PersonalInfo.FirstNameLang,
//                         MiddleName = user.PersonalInfo.MiddleNameLang,

//                         // LastName = user.PersonalInfo.LastNameLang,
//                         BirthDate = user.PersonalInfo.BirthDate,
//                         NationalId = user.PersonalInfo.NationalId,
//                         // PlaceOfBirthLookup = user.PersonalInfo.PlaceOfBirthLookup.ValueLang,
//                         NationalityLookup = user.PersonalInfo.NationalityLookup.ValueLang,
//                         // TitleLookup = user.PersonalInfo.TitleLookup.ValueLang,
//                         // ReligionLookup = user.PersonalInfo.ReligionLookup.ValueLang,
//                         EducationalStatusLookup = user.PersonalInfo.EducationalStatusLookup.ValueLang,
//                         // TypeOfWorkLookup = user.PersonalInfo.TypeOfWorkLookup.ValueLang,
//                         MarraigeStatusLookup = user.PersonalInfo.MarraigeStatusLookup.ValueLang,
//                         NationLookup = user.PersonalInfo.NationLookup.ValueLang,
//                         CreatedDate = user.PersonalInfo.CreatedAt,
//                         // ContactInfo = _mapper.Map<ContactInfoDTO>(user.PersonalInfo.ContactInfo)

//                     }
//                 }).ToList()

//                 , request.PageCount ?? 1, request.PageSize ?? 10);





//             // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
//         }
//     }
// }