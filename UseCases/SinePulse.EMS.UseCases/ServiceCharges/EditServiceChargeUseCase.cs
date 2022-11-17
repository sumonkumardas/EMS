using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using System;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class EditServiceChargeUseCase : IEditServiceChargeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditServiceChargeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public EditServiceChargeResponseMessage EditServiceCharge(EditServiceChargeRequestMessage requestMessage)
    {
      var serviceCharge = _repository.GetById<ServiceCharge>(requestMessage.ServiceChargeId);

      serviceCharge.PerStudentCharge = requestMessage.PerStudentCharge;
      serviceCharge.PaymentBufferPeriod = requestMessage.PaymentBufferPeriod;
      serviceCharge.EffectiveDate = requestMessage.EffectiveDate;
      serviceCharge.EndDate = requestMessage.EndDate;
      serviceCharge.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      serviceCharge.AuditFields.LastUpdatedDateTime = DateTime.Now;
      serviceCharge.BranchMedium = requestMessage.BranchMedium;

      _dbContext.SaveChanges();

      return new EditServiceChargeResponseMessage(serviceCharge.Id);
    }
  }
}
