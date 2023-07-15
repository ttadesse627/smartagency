using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using Newtonsoft.Json.Linq;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.ApplicantsCvDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.EnjazDTOs;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
{
    private readonly SmartAgencyDbContext _context;
    private readonly IFileService _fileService;
    public ApplicantRepository(SmartAgencyDbContext dbContext, IFileService fileService) : base(dbContext)
    {
        _context = dbContext;
        _fileService = fileService;
    }
    public override async Task InsertAsync(Applicant applicant, CancellationToken cancellationToken)
    {
        await base.InsertAsync(applicant, cancellationToken);
    }

    public async Task<Applicant> GetApplicantByIdAsync(Guid id)
    {
        var applicant = await _context.Applicants.Where(appl => appl.Id.Equals(id)).FirstOrDefaultAsync(appl => appl.Id == id);
        if (applicant is null)
        {
            throw new Exception($"An applicant with id {id} could not be found!");
        }
        return applicant;
    }

    public async Task<List<Applicant>> GetAll()
    {
        var response = new List<Applicant>();
        var applicants = await _context.Applicants.ToListAsync();

        if (applicants.Count >= 0)
        {
            response = applicants;
        }
        return response;
    }
    public async Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber)
    {
        var serviceResponse = new ServiceResponse<Applicant>();
        serviceResponse.Data = await _context.Applicants.FirstOrDefaultAsync(a => a.PassportNumber == passportNumber);

        return serviceResponse;
    }
    public async Task<int> AddApplicantAsync(Applicant applicant)
    {
        var response = 0;
        try
        {
            await _context.Applicants.AddAsync(applicant);
            response = 1;
        }
        catch (Exception ex)
        {
            throw new Exception("Couldn't add the applicant");
        }

        return response;
    }

    public async Task<int> EditApplicantAsync(Applicant applicant)
    {
        var response = 0;
        try
        {
            _context.Set<Applicant>().Attach(applicant);
            _context.Entry(applicant).State = EntityState.Modified;
            // _context.Applicants.Update(applicant);
            response = await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot update the applicant: {ex.Message}");
        }
        return response;
    }


    public virtual async Task<SearchModel<Applicant>> GetAllApplWithPredicateAsync(int pageNumber, int pageSize, string orderBy, SortingDirection sortingDirection, List<Expression<Func<Applicant, bool>>> predicates, params string[] eagerLoadedProperties)
    {
        long maxPage = 1, totalItems = 0;

        var query = _context.Set<Applicant>().AsQueryable();
        if (predicates.Count > 0)
        {
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
        }

        foreach (var nav_property in eagerLoadedProperties)
        {
            query = query.Include(nav_property);
        }

        totalItems = query.LongCount();
        if (totalItems > 0)
        {
            maxPage = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(totalItems) / pageSize));
            if (pageNumber >= maxPage)
            {
                pageNumber = Convert.ToInt32(maxPage);
            }
        }

        // Sorting
        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderExpression = $"{orderBy} {(sortingDirection == SortingDirection.Ascending ? "ascending" : "descending")}";
            query = query.OrderBy(orderExpression);
        }

        // Pagination
        var skipAmount = (pageNumber - 1) * pageSize;
        query = query.Skip(skipAmount).Take(pageSize);

        var result = await query.ToListAsync();
        return new SearchModel<Applicant>
        {
            CurrentPage = pageNumber,
            MaxPage = maxPage,
            PagingSize = pageSize,
            Items = result,
            TotalCount = totalItems,
            SortingColumn = orderBy,
            SortingDirection = sortingDirection
        };
    }

    public async Task<SearchModel<Applicant>> GetAllApplWithPredicateSrchAsync(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection, Expression<Func<Applicant, bool>>? predicate = null, params string[] eagerLoadedProperties)
    {
        long maxPage = 1, totalItems = 0;

        var query = _context.Set<Applicant>().AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        foreach (var nav_property in eagerLoadedProperties)
        {
            query = query.Include(nav_property);
        }

        totalItems = query.LongCount();
        if (totalItems > 0)
        {
            maxPage = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(totalItems) / pageSize));
            if (pageNumber >= maxPage)
            {
                pageNumber = Convert.ToInt32(maxPage);
            }
        }

        // Sorting
        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderExpression = $"{orderBy} {(sortingDirection == SortingDirection.Ascending ? "ascending" : "descending")}";
            query = query.OrderBy(orderExpression);
        }

        // Pagination
        var skipAmount = (pageNumber - 1) * pageSize;
        query = query.Skip(skipAmount).Take(pageSize);
        var result = await query.ToListAsync();
        return new SearchModel<Applicant>
        {
            CurrentPage = pageNumber,
            MaxPage = maxPage,
            PagingSize = pageSize,
            SearchKeyWord = searchTerm,
            Items = result,
            TotalCount = totalItems,
            SortingColumn = orderBy,
            SortingDirection = sortingDirection
        };
    }


    public async Task<ApplicantCvResponseDTO> GetApplicantCvDetail(Guid id)
    {

        var response = await _context.Applicants
            .Where(app => app.Id == id)
            .Include("Salary")
            .Include("Religion")
            .Include("Jobtitle")
            .Include("MaritalStatus")
            .Include("Skills.LookUp")
            .Include("Education.LevelOfQualifications.LookUp")
            .Include("Address")
            .Include("EmergencyContact")
            .Include("EmergencyContact.Address")
            .Include("Experiences.Country")
            .Include("PassportIssuedPlace")
            .Include("Attachments")
            .Include("LanguageSkills.Language")
    .Select(app => new ApplicantCvResponseDTO
    {
        Overview = new OverviewResponseDTO
        {
            RefNumber = "001",
            FullName = $"{app.FirstName} {app.MiddleName} {app.LastName}",
            Religion = app.Religion.Value,
            DesiredPosition = app.Jobtitle.Value,
            Salary = app.Salary.Value,
            Age = DateTime.Now.Year - app.BirthDate.Year,
            Sex = app.Gender.ToString()
        },
        PersonalInfo = new PersonalInfoResponseDTO
        {
            Id = app.Id,
            Nationality = app.CurrentNationality,
            DateOfBirth = app.BirthDate.ToString("yyyy-MM-dd"),
            PlaceOfBirth = app.PlaceOfBirth,
            MaritalStatus = app.MaritalStatus.Value,
            NumberOfChildren = app.NumberOfChildren,
            Height = app.Height,
            Weight = app.Weight,
            EducationQualification = app.Education.LevelOfQualifications.FirstOrDefault().LookUp.Value,
            PhoneNumber = app.Address.PhoneNumber,
        },
        OverseasExperiences = app.Experiences.Select(e => new OverseasExperienceResponseDTO
        {
            Country = e.Country.Value,
            Period = e.PeriodLength,
            Position = e.Position
        }).ToList(),
        PassportInfo = new PassportInfoResponseDTO
        {
            PassportNumber = app.PassportNumber,
            IssuedDate = app.IssuedDate.ToString("yy-MM-dd"),
            ExpiryDate = app.PassportExpiryDate.ToString("yy-MM-dd"),
            PassportIssuedPlace = app.PassportIssuedPlace.Value,
            NextOfKinName = app.EmergencyContact.NameOfContactPerson,
            NextOfKinNumber = app.EmergencyContact.Address.PhoneNumber
        },
        Skills = app.Skills.Select(s => s.LookUp.Value).ToList(),
        Languages = app.LanguageSkills.Select(l => new LanguagesResponseDTO
        {
            LanguageName = l.Language.Value,
            Proficiency = l.Proficiency.ToString()
        }
        ).ToList(),
        // Attachments= new AttachmentsResponseDTO
        //  {
        //     Photo= Convert.ToBase64String(_fileService.getFile(id.ToString(), "3x4",null).Item1),
        //     FullSizePhoto= Convert.ToBase64String(_fileService.getFile(id.ToString(), "Full Size", null).Item1)
        //  },
        AttachmentTypes = app.Attachments.Where(att => att.ShowOnCv == true).Select(att => att.Title).ToList()
    }).FirstOrDefaultAsync();
        return response;


    }

    public async Task<List<NotProcessedApplicantResponseDTO>> GetNotProcessedApplicants()
    {


        var response = await _context.Applicants
                       .Where(ap => ap.ApplicantProcesses == null || ap.ApplicantProcesses.Count() == 0)
                       .Select(g => new NotProcessedApplicantResponseDTO
                       {
                           PassportNumber = g.PassportNumber,
                           Duration = DateTime.Now.Subtract(g.CreatedAt).Days,
                           FullName = g.FirstName + " " + g.MiddleName + " " + g.LastName,
                           Sex = g.Gender.ToString(),
                           MaritalStatus = g.MaritalStatus.Value,
                           Religion = g.Religion.Value,
                           Profession = g.Jobtitle.Value,
                           ArabicName = g.ArabicFullName,
                           Age = DateTime.Now.Year - g.BirthDate.Year,
                       }).ToListAsync();

        return response;

    }

    public async Task<List<NewAssignedVisaResponseDTO>> GetNewAssignedVisa()
    {
        var daysAgo = DateTime.Now.AddDays(-10);
        var response = await _context.Applicants
                            .Where(ap => ap.OrderId != null && ap.CreatedAt >= daysAgo)
                            .Select(ap => new NewAssignedVisaResponseDTO
                            {
                                VisaNumber = ap.Order.VisaNumber,
                                Date = ap.Order.OrderDate,
                                JobTitle = ap.Jobtitle!.Value,
                                Salary = ap.Salary.Value,
                                ApplicantId = ap.Id,
                                Employee = ap.FirstName + " " + ap.MiddleName + " " + ap.LastName,
                                Country = ap.DesiredCountry.Value
                            })

                            .ToListAsync();

        return response;

    }


    public async Task<ShowOrderStatusResponseDTO> GetShowOrderStatus(Guid id)
    {

        var showStatus = new ShowOrderStatusResponseDTO();

        var appOrderInfo = await _context.Applicants
        .Where(ap => (ap.Id == id) && (ap.IsDeleted == false) && (ap.OrderId != null))
            .Include(app => app.Order)
            .Include(app => app.Order.Partner)
            .Include(app => app.Order.Sponsor)
            .Include(app => app.Order.Sponsor.Address)
            .Include(app => app.Order.Sponsor.Address.City)
            .Include(app => app.Order.Priority)
            .Include(app => app.MaritalStatus)
            //.Include (app=> app.Gender)
            .Include(app => app.Religion)
            .Select(app => new
            {
                orderInfo = new OrderInfoResponseDTO
                {
                    OrderNumber = app.Order.OrderNumber,
                    ClientName = app.Order.Partner.PartnerName,
                    Priority = app.Order.Priority.Value,
                    VisaNumber = app.Order.VisaNumber,
                    Sponsor = app.Order.Sponsor.FullName,
                    City = app.Order.Sponsor.Address.City.Value
                },
                //applicants info
                applicantInfo = new ApplicantInfoResponseDTO
                {
                    PassportNumber = app.PassportNumber,
                    FullName = $"{app.FirstName} {app.MiddleName} {app.LastName}",
                    Sex = app.Gender.ToString(),
                    MaritalSatus = app.MaritalStatus.Value,
                    Religion = app.Religion.Value,
                    DateOfBirth = app.BirthDate.ToString("yyyy-MM-dd"),
                    CurrentNationality = app.CurrentNationality
                }
            })
        .FirstOrDefaultAsync();

        showStatus.OrderInformation = appOrderInfo?.orderInfo;
        showStatus.ApplicantInformation = appOrderInfo?.applicantInfo;



        var tickReg = await _context.TicketRegistrations.FirstOrDefaultAsync(tr => tr.ApplicantId == id);

        var ticketReady = await _context.TicketReadies
                     .Include(tr => tr.TicketOffice)
                     .Include(tr => tr.Applicant)
                     .Where(tr => tr.ApplicantId == id)
                     .Select(tr => new { tr.TicketOffice })
                     .FirstOrDefaultAsync();
        var traveledRes = await _context.TraveledApplicants
                     .Where(trA => trA.ApplicantId == id)
                     .Select(trA => new { trA.Remark })
                     .FirstOrDefaultAsync();
        var traveledTime = await _context.ApplicantProcesses.Where(aPro => aPro.ApplicantId == id && aPro.ProcessDefinition.Name == "Travel")
                            .Select(aPro => new { aPro.Date }).FirstOrDefaultAsync();
        var travelInfo = new TravelInfoResponseDTO
        {
            RegisteredDate = tickReg?.RegisteredDate,
            TicketNumber = tickReg?.TicketNumber,
            FlightDate = tickReg?.FlightDate,
            DepatrureFromAddis = tickReg?.DepartureTime,
            ArrivalTime = tickReg?.ArrivalTime,
            TicketOffice = ticketReady?.TicketOffice?.Value,
            TicketPrice = tickReg?.TicketPrice,
            Remark = traveledRes?.Remark,
            Traveled = traveledTime?.Date,
            UploadTicket = ""
        };

        showStatus.TravelInformation = travelInfo;

        var enjazRes = await _context.Enjazs.Where(en => en.ApplicantId == id)
                     .Select(en => new ShowStatusEnjazResponseDTO
                     {
                         Id = en.Id,
                         ApplicationNumber = en.ApplicationNumber,
                         TransactionCode = en.TransactionCode
                     }).FirstOrDefaultAsync();

        showStatus.EnjazResponse = enjazRes;

        //      var statuses= new List<StatusInfoResponseDTO>();

        //      var statusRes= await _context.ApplicantProcesses
        //                      .Where(appPro=> appPro.ApplicantId==id)
        //                      .Select(appPro=> new{
        //                       ProcessStatus= statuses.Select(st=> new StatusInfoResponseDTO{
        //                         Id= appPro.ProcessDefinitionId,
        //                         StatusName= appPro.ProcessDefinition.Name,
        //                         Date= appPro.Date

        //                       }).ToList()}).FirstOrDefaultAsync();   



        //   showStatus.StatusInformation=statusRes.ProcessStatus;


        var statusRes = await _context.ProcessDefinitions
        .GroupJoin(
            _context.ApplicantProcesses.Where(appPro => appPro.ApplicantId == id), // join with ApplicantProcesses table
            pd => pd.Id, // join on ProcessDefinition Id
            ap => ap.ProcessDefinitionId, // join on ApplicantProcess ProcessDefinitionId
            (pd, appProGroup) => new { ProcessDef = pd, ApplicantProcesses = appProGroup }) // select result
        .SelectMany(
            x => x.ApplicantProcesses.DefaultIfEmpty(), // flatten result
            (pd, appPro) => new StatusInfoResponseDTO
            {
                Id = pd.ProcessDef.Id,
                StatusName = pd.ProcessDef.Name,
                Date = appPro != null ? appPro.Date : null // set Date to null if no matching record in ApplicantProcesses table
            })
        .ToListAsync();

        showStatus.StatusInformation = statusRes;
        var res = showStatus;

        return showStatus;
    }


}