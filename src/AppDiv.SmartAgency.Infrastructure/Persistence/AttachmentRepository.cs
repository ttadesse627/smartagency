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
    private readonly IMapper _mapper;

    public AttaachmentRepository(SmartAgencyDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public override async Task InsertAsync(Attachment attachment, CancellationToken cancellationToken)
    {
        await base.InsertAsync(attachment, cancellationToken);
    }
    public async Task<Attachment> GetByIdAsync(Guid Id)
    {
        return await base.GetAsync(Id);
    }
    public async Task<ServiceResponse<List<AttachmentResponseDTO>>> DeleteAttachment(Guid id)
    {
        var serviceResponse = new ServiceResponse<List<AttachmentResponseDTO>>();
        try
        {
            var attachment = await _context.Attachments.FirstOrDefaultAsync(att => att.Id == id);

            if (attachment is null)
            {
                throw new Exception($"There is no character with id {id} to delete.");
            }
            _context.Attachments.Remove(attachment);
            int resp = await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Attachments
                .Select(attch => CustomMapper.Mapper.Map<AttachmentResponseDTO>(attch)).ToListAsync();

        }
        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }
        return serviceResponse;
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
            attachment.Code = updatedAttachment.Code;
            attachment.Description = updatedAttachment.Description;
            attachment.Category = updatedAttachment.Category;
            attachment.IsRequired = updatedAttachment.IsRequired;
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