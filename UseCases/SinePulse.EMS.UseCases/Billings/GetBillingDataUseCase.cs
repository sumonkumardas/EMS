using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.ServiceCharges;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.Billings
{
  public class GetBillingDataUseCase : IGetBillingDataUseCase
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public GetBillingDataUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public GetBillingDataResponseMessage.Billing GetBillingData(GetBillingDataRequestMessage message)
    {
      var currentSession = _currentSessionProvider.GetCurrentSession(message.BranchMediumId);
      var years = _repository.Table<BillingData>()
        .Where(b => currentSession != null       
                    && b.BranchMedium.Id == message.BranchMediumId)
        .OrderBy(b => b.Year)
        .Select(b => b.Year)
        .Distinct()
        .ToList(); 
      var billingData = _repository.Table<BillingData>()
        .Where(b => currentSession != null       
                    && b.BranchMedium.Id == message.BranchMediumId 
                    && b.Year == currentSession.StartDate.Year)
        .OrderByDescending(b => b.Month)
        .ToList();
      var totalStudent = _repository.GetByIdWithInclude<BranchMedium>(message.BranchMediumId,
          new []{nameof(BranchMedium.Students)})?
        .Students?
        .Where(s => s.Status == StatusEnum.Active)
        .Count();
      
      var perStudentCharge = _repository.Table<ServiceCharge>()
        .FirstOrDefault(s => s.BranchMedium.Id == message.BranchMediumId 
                             && s.EffectiveDate <= DateTime.Now)?
        .PerStudentCharge;
      var numberOfPendingMonths = 0;
      var payments = _repository.Table<BillingData>()
        .Where(b => b.BranchMedium.Id == message.BranchMediumId)
        .OrderByDescending(b => b.Year)
        .ThenByDescending(x => x.Month)
        .ToList();
      if (payments.Any())
      {
        numberOfPendingMonths = (int) (DateTime.Now.Month -1 - payments.First().Month);
        if (numberOfPendingMonths < 0)
        {
          numberOfPendingMonths += 12;
        }
      }
      else
      {
        var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
        var branchMediumCreateDate = branchMedium.AuditFields.InsertedDateTime;
        numberOfPendingMonths = DateTime.Now.Month  - branchMediumCreateDate.Month;
        if (numberOfPendingMonths < 0)
        {
          numberOfPendingMonths += 12;
        }
      }
      decimal pendingCharge = 0;
      if (totalStudent != null && perStudentCharge != null)
      {
        pendingCharge = totalStudent.Value * perStudentCharge.Value * numberOfPendingMonths;
      }
      return new GetBillingDataResponseMessage.Billing
      {
        BillingInfos = GetBillingInfo(billingData),
        Years = years,
        PendingAmount = pendingCharge
      };
    }

    private List<GetBillingDataResponseMessage.BillingInfo> GetBillingInfo(List<BillingData> billingData)
    {
      var billingInfo = new List<GetBillingDataResponseMessage.BillingInfo>();
      foreach (var data in billingData)
      {
        billingInfo.Add(new GetBillingDataResponseMessage.BillingInfo
        {
          TransactionNo = data.TransactionId,
          UserCode = data.UserCode,
          Year = data.Year,
          Month = data.Month,
          PaymentDate = data.PaymentDate,
          PerStudentCharge = data.PerStudentCharge,
          TotalStudents = data.TotalStudents,
          Amount = data.Amount,
          Vat = data.Vat,
          PaymentMethod = data.PaymentMethod,
          IsProcessed = data.IsProcessed
        });
      }

      return billingInfo;
    }
  }
}