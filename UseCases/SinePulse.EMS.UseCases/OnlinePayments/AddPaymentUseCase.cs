using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.PaymentGateway;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class AddPaymentUseCase : IAddPaymentUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddPaymentUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddPayment(AddPaymentRequestMessage message)
    {
      var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
      var payment = new SslPaymentGateway
      {
        TransactionId = message.TransactionId,
        TransactionAmount = message.DueAmount,
        Month = message.Month,
        UserCode = message.UserCode,
        Year = message.Year,
        TotalStudents = message.TotalStudents,
        PerStudentCharge = message.PerStudentCharge,
        AuditFields = new AuditFields
        {
          LastUpdatedBy = message.CurrentUserName,
          InsertedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now,
          InsertedDateTime = DateTime.Now
        },
        BranchMedium = branchMedium
      };
      _repository.Insert(payment);
      _dbContext.SaveChanges();
    }
  }
}