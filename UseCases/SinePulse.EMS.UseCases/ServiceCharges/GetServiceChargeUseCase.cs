using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Repositories;
using System.Linq;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class GetServiceChargeUseCase : IGetServiceChargeUseCase
  {
    private readonly IRepository _repository;

    public GetServiceChargeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetServiceChargeResponseMessage.ServiceCharge GetServiceCharge(
      GetServiceChargeRequestMessage message)
    {
      if (message.BranchMediumId > 0)
      {
        var serviceCharge = _repository.Table<ServiceCharge>()
          .Include(nameof(BranchMedium))
          .Where(brm => brm.BranchMedium.Id == message.BranchMediumId)
          .FirstOrDefault();

        if (serviceCharge != null)
        {
          return new GetServiceChargeResponseMessage.ServiceCharge
          {
            ServiceChargeId = serviceCharge.Id,
            PerStudentCharge = serviceCharge.PerStudentCharge,
            PaymentBufferPeriod = serviceCharge.PaymentBufferPeriod,
            EffectiveDate = serviceCharge.EffectiveDate,
            EndDate = serviceCharge.EndDate
          };
        }
      }

      return new GetServiceChargeResponseMessage.ServiceCharge();
    }
    
  }
}
