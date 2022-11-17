using System;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetDueAmountUseCase : IGetDueAmountUseCase
  {
    private readonly IRepository _repository;

    public GetDueAmountUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetDueAmountResponseMessage.PendingInfo GetDueAmount(GetDueAmountRequestMessage message)
    {
      var totalStudent = _repository.GetByIdWithInclude<BranchMedium>(message.BranchMediumId,
          new[] {nameof(BranchMedium.Students)})?
        .Students?
        .Where(s => s.Status == StatusEnum.Active)
        .Count();

      var perStudentCharge = _repository.Table<ServiceCharge>()
        .FirstOrDefault(s => s.BranchMedium.Id == message.BranchMediumId
                             && s.EffectiveDate <= DateTime.Now)?
        .PerStudentCharge;

      var pendingInfo = new GetDueAmountResponseMessage.PendingInfo
      {
        ActiveStudents = "0",
        PerStudentCharge = "0",
        PendingAmount = 0
      };

      if (totalStudent != null)
      {
        pendingInfo.ActiveStudents = totalStudent.Value.ToString();
      }

      if (perStudentCharge != null)
      {
        pendingInfo.PerStudentCharge = perStudentCharge.Value.ToString();
      }
      if (totalStudent != null && perStudentCharge != null)
      {
        var dueAmount = totalStudent.Value * perStudentCharge.Value;
        pendingInfo.PendingAmount = dueAmount;
      }

      return pendingInfo;
    }
  }
}