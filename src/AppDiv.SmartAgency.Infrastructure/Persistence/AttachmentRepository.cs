using System.Linq;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;

namespace AppDiv.SmartAgency.Infrastructure.Persistence;
public class AttaachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
{
    private readonly SmartAgencyDbContext _context;

    public AttaachmentRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Task<List<AttachmentDropDownDto>> GetApplicantAttachments()
    {
        var response = _context.Attachments
                           .Where(att => att.Type == Domain.Enums.AttachmentType.Applicant)
                           .Select(att => new AttachmentDropDownDto
                           {
                               Key = att.Id,
                               Value = att.Title,
                               IsRequired = att.Required

                           }).ToListAsync();

        return response;
    }

    public Task<List<AttachmentDropDownDto>> GetOrderAttachments()
    {
        var response = _context.Attachments
                           .Where(att => att.Type == Domain.Enums.AttachmentType.Order)
                           .Select(att => new AttachmentDropDownDto
                           {
                               Key = att.Id,
                               Value = att.Title,
                               IsRequired = att.Required

                           }).ToListAsync();

        return response;
    }

    public async Task<ServiceResponse<AttachmentResponseDTO>> UpdateAttachment(EditAttachmentCommand updatedAttachment)
    {
        var serviceResponse = new ServiceResponse<AttachmentResponseDTO>();
        try
        {
            var attachment = await _context.Attachments
                .FirstOrDefaultAsync(ch => ch.Id == updatedAttachment.Id);
            if (attachment is null)
            {
                throw new Exception($"Attachment with id {updatedAttachment.Id} is not found.");
            }
            attachment.Title = updatedAttachment.Title;
            attachment.Type = updatedAttachment.Type;
            attachment.Required = updatedAttachment.Required;
            attachment.ShowOnCv = updatedAttachment.ShowOnCv;
            await _context.SaveChangesAsync();
            serviceResponse.Data = CustomMapper.Mapper.Map<AttachmentResponseDTO>(attachment);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
            Console.WriteLine(ex);
        }
        return serviceResponse;
    }
}