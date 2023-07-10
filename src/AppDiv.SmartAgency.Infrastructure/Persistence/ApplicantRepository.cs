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
        var overseasExperiences = new List<OverseasExperienceResponseDTO>();
        var skills = new List<string>();
        var languages = new List<LanguagesResponseDTO>();
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
        //     Photo= Convert.ToBase64String(_fileService.getFile(id.ToString(), "Photo",null).Item1),
        //     FullSizePhoto= Convert.ToBase64String(_fileService.getFile(id.ToString(), "Full Size", null).Item1)
        //  },
        AttachmentNames = app.Attachments.Where(att=>att.ShowOnCv==true).Select(att => att.Title).ToList()
    }).FirstOrDefaultAsync();
        return response;


    }

    public async Task<List<NotProcessedApplicantResponseDTO>> GetNotProcessedApplicants()
    {
     

        var response= await _context.Applicants
                       .Where(ap=> ap.ApplicantProcesses==null || ap.ApplicantProcesses.Count()==0)
                       .Select(g => new NotProcessedApplicantResponseDTO{ PassportNumber= g.PassportNumber, 
                                          Duration = DateTime.Now.Subtract(g.CreatedAt).Days,
                                          FullName = g.FirstName+ " " +g.MiddleName+" " +g.LastName,
                                          Sex= g.Gender.ToString(),
                                          MaritalStatus= g.MaritalStatus.Value,
                                          Religion = g.Religion.Value,
                                          Profession =  g.Jobtitle.Value,
                                          ArabicName= g.ArabicFullName,
                                          Age = DateTime.Now.Year - g.BirthDate.Year,
                            }).ToListAsync();

          return response;
                       
    }

   public async Task<List<NewAssignedVisaResponseDTO>>  GetNewAssignedVisa()
   {
        var daysAgo = DateTime.Now.AddDays(-10);
        var response= await _context.Applicants
                            .Where(ap => ap.OrderId !=null && ap.CreatedAt >= daysAgo)
                            .Select( ap => new NewAssignedVisaResponseDTO {
                                         VisaNumber = ap.Order.VisaNumber,
                                         Date = ap.Order.OrderDate,
                                         JobTitle = ap.Jobtitle!.Value,
                                         Salary = ap.Salary.Value,
                                         ApplicantId = ap.Id,
                                         Employee = ap.FirstName +" " + ap.MiddleName + " " + ap.LastName,
                                         Country = ap.DesiredCountry.Value
                            }) 
                            
                            .ToListAsync();

       return response;

   }

   
}