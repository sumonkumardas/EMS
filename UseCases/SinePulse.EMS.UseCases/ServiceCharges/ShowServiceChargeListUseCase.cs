using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class ShowServiceChargeListUseCase : IShowServiceChargeListUseCase
  {

    private readonly IRepository _repository;

    public ShowServiceChargeListUseCase(IRepository repository)
    {
      _repository = repository;
    }
    public ShowServiceChargeListResponseMessage ShowServiceChargeList(ShowServiceChargeListRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ServiceCharge.BranchMedium),
        $"{nameof(ServiceCharge.BranchMedium)}.{nameof(BranchMedium.Branch)}",
        $"{nameof(ServiceCharge.BranchMedium)}.{nameof(BranchMedium.Medium)}",
        $"{nameof(ServiceCharge.BranchMedium)}.{nameof(BranchMedium.Branch)}.{nameof(BranchMedium.Branch.Institute)}"
      };

      var serviceCharge = _repository.Table<ServiceCharge>(includes)
                                    .Where(brm => brm.BranchMedium.Id == message.BranchMediumId)
                                    .ToList();
      var serviceChargeCollection = new List<ShowServiceChargeListResponseMessage.ServiceCharge>();
      foreach (var service in serviceCharge)
      {
        serviceChargeCollection.Add(ServiceChargeEntity(service));
      }
      return new ShowServiceChargeListResponseMessage(serviceChargeCollection);
    }

    private ShowServiceChargeListResponseMessage.ServiceCharge ServiceChargeEntity(ServiceCharge serviceChargeEntity)
    {
      return new ShowServiceChargeListResponseMessage.ServiceCharge
      {
        Id = serviceChargeEntity.Id,
        PerStudentCharge = serviceChargeEntity.PerStudentCharge,
        PaymentBufferPeriod = serviceChargeEntity.PaymentBufferPeriod,
        EffectiveDate = serviceChargeEntity.EffectiveDate,
        EndDate = serviceChargeEntity.EndDate,
        BranchName = serviceChargeEntity.BranchMedium.Branch.BranchName,
        InstituteName = serviceChargeEntity.BranchMedium.Branch.Institute.OrganisationName,
        BranchMedium = new ShowServiceChargeListResponseMessage.BranchMedium
        {
          BranchMediumId = serviceChargeEntity.BranchMedium.Id,
          BranchMediumName = serviceChargeEntity.BranchMedium.Medium.MediumName
        }
      };
    }
  }
}
