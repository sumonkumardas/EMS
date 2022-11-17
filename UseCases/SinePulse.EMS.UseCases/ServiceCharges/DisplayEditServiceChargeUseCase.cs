using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class DisplayEditServiceChargeUseCase : IDisplayEditServiceChargeUseCase
  {
    private readonly IRepository _repository;

    public DisplayEditServiceChargeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayEditServiceChargeResponseMessage ShowServiceCharge(DisplayEditServiceChargeRequestMessage message)
    {
        var serviceCharge = _repository.Table<ServiceCharge>()
          .Include(nameof(BranchMedium))
          .Where(i => i.Id == message.ServiceChargeId)
          .FirstOrDefault();
        
          var Charge =  new DisplayEditServiceChargeResponseMessage.ServiceCharge
          {
            Id = serviceCharge.Id,
            PerStudentCharge = serviceCharge.PerStudentCharge,
            PaymentBufferPeriod = serviceCharge.PaymentBufferPeriod,
            EffectiveDate = serviceCharge.EffectiveDate,
            EndDate = serviceCharge.EndDate,
            BranchMedium = serviceCharge.BranchMedium
          };
        return new DisplayEditServiceChargeResponseMessage(Charge);
      
    }
  }
}
