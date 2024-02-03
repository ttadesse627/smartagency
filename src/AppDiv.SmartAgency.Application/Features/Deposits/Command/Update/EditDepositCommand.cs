
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Deposits.Command.Update
{
    public class EditDepositCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string PassportNumber { get; set; } = null!;
        public double DepositAmount { get; set; }
        public DateTime Month { get; set; }
        public string DepositedBy { get; set; } = null!;
        //public Guid ApplicantId {get; set;}


    }





    public class EditDepositCommandHandler : IRequestHandler<EditDepositCommand, string>
    {

        private readonly IDepositRepository _depositRepository;
        private readonly IApplicantRepository _applicantRepository;

        public EditDepositCommandHandler(IDepositRepository depositRepository, IApplicantRepository applicantRepository)
        {
            _depositRepository = depositRepository;
            _applicantRepository = applicantRepository;
        }
        public async Task<string> Handle(EditDepositCommand request, CancellationToken cancellationToken)
        {
            int response = 0;


            try
            {

                var serviceResponse = await _applicantRepository.GetApplicantByPassportNumber(request.PassportNumber);
                if (serviceResponse.Data != null)
                {
                    var deposit = new Deposit()
                    {
                        Id = request.Id,
                        PassportNumber = request.PassportNumber,
                        DepositAmount = request.DepositAmount,
                        DepositedBy = request.DepositedBy,
                        Month = request.Month,
                        ApplicantId = serviceResponse.Data.Id

                    };


                    var res = await _depositRepository.UpdateAsync(deposit);


                    if (res >= 1)
                    {
                        response = res;

                    }

                }
                else
                {
                    return "No data found with " + request.PassportNumber + " passport number";
                }


            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            return response + " record updated";
            //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
            // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

        }
    }
}