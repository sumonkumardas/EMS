using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class AddServiceChargeUseCase : IAddServiceChargeUseCase
  {
    private readonly IRepository _repository;

    public AddServiceChargeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ServiceCharge AddServiceCharge(AddServiceChargeRequestMessage message)
    {
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);

      var previousServiceCharges = _repository.Table<ServiceCharge>()
                                    .Where(brm => brm.BranchMedium.Id == message.BranchMediumId)
                                    .ToList().OrderByDescending(x => x.EndDate);
      if((previousServiceCharges != null) && previousServiceCharges.Any())
      {
        var previousServiceCharge = previousServiceCharges.First();
        previousServiceCharge.EndDate = message.EffectiveDate.AddDays(-1);
        previousServiceCharge.AuditFields.LastUpdatedDateTime = DateTime.Now;
      }
      
      var serviceCharge = new ServiceCharge
      {
        PerStudentCharge = message.PerStudentCharge,
        PaymentBufferPeriod = message.PaymentBufferPeriod,
        EffectiveDate = message.EffectiveDate,
        EndDate = null,
        BranchMedium = branchMedium,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(serviceCharge);
      return serviceCharge;
    }
  }
}
