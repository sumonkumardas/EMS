using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Billing;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BillingMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Billings
{
  public class AddBillingDataUseCase : IAddBillingDataUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddBillingDataUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddBillingData(AddBillingDataRequestMessage message)
    {
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId); 
      var billingData = new BillingData
      {
        TransactionId = message.TransactionId,
        UserCode = message.UserCode,
        Year = message.Year,
        Month = message.Month,
        PaymentDate = message.PaymentDate,
        PerStudentCharge = message.PerStudentCharge,
        TotalStudents = message.TotalStudents,
        Amount = message.Amount,
        Vat = message.Vat,
        PaymentMethod = message.PaymentMethod,
        IsProcessed = message.IsProcessed,
        AuditFields = new AuditFields
        {
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now,
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now
        },
        BranchMedium = branchMedium
      };
      _repository.Insert(billingData);
      _dbContext.SaveChanges();
    }
  }
}