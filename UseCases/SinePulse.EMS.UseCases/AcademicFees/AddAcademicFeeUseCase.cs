using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class AddAcademicFeeUseCase : IAddAcademicFeeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddAcademicFeeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddAcademicFee(AddAcademicFeeRequestMessage message)
    {
      var @class = _repository.GetById<Class>(message.ClassId);
      var index = -1;
      foreach (var accountHead in message.AccountHeads)
      {
        var academicFee = _repository.Table<AcademicFee>()
          .Include(nameof(AcademicFee.AccountHead))
          .Include(nameof(Class))
          .FirstOrDefault(a => a.AccountHead.AccountCode == accountHead.AccountCode 
                               && a.Class.Id == message.ClassId 
                               && a.AccountHead.Session.Id == message.SessionId
                               && a.AccountHead.BranchMedium.Id == message.BranchMediumId);
        var fees = message.FeesCollection[++index];
        if (academicFee == null)
        {
          var branchMediumAccountHead = _repository.Table<BranchMediumAccountHead>()
            .FirstOrDefault(b => b.AccountCode == accountHead.AccountCode);
          var newAcademicFee = new AcademicFee
          {
            Fees = fees,
            Class = @class,
            AccountHead = branchMediumAccountHead,
            FeePeriod = message.AcademicFeePeriod,
            AuditFields = new AuditFields
            {
              InsertedBy = message.CurrentUserName,
              InsertedDateTime = DateTime.Now,
              LastUpdatedBy = message.CurrentUserName,
              LastUpdatedDateTime = DateTime.Now
            }
          };
          _repository.Insert(newAcademicFee);
        }
        else
        {
          academicFee.Fees = fees;
          academicFee.AuditFields.LastUpdatedBy = message.CurrentUserName;
          academicFee.AuditFields.LastUpdatedDateTime = DateTime.Now;
        }
      }

      _dbContext.SaveChanges();
    }
  }
}