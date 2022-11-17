using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.WaiverMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class AddWaiverUseCase : IAddWaiverUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddWaiverUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void AddWaiver(AddWaiverRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var section = _repository.GetById<Section>(message.SectionId);
      var index = -1;
      foreach (var academicFee in message.AcademicFees)
      {
        ++index;
        var waiver = message.Waivers[index];
        var isPercentage = message.InPercentagesBooleanArray[index];
        
        var branchMediumAccountHead = _repository.Table<BranchMediumAccountHead>()
          .FirstOrDefault(b => b.AccountCode == academicFee.AccountHead.AccountCode);

        var existingWaiver = _repository.Table<StudentWaiver>()
          .Include(nameof(AcademicFee.AccountHead))
          .Include(nameof(Student))
          .Include(nameof(Section))
          .FirstOrDefault(a => a.AccountHead.AccountCode == academicFee.AccountHead.AccountCode
                               && a.AccountHead.Session.Id == message.SessionId
                               && a.AccountHead.BranchMedium.Id == message.BranchMediumId
                               && a.Student.Id == message.StudentId
                               && a.Section.Id == message.SectionId);

        if (existingWaiver != null)
        {
          existingWaiver.Waiver = waiver;
          existingWaiver.InPercentage = isPercentage;
        }
        else
        {
          var studentWaiver = new StudentWaiver
          {
            Waiver = waiver,
            InPercentage = isPercentage,
            Fees = academicFee.Fees,
            AuditFields = new AuditFields
            {
              InsertedBy = message.CurrentUserName,
              InsertedDateTime = DateTime.Now,
              LastUpdatedBy = message.CurrentUserName,
              LastUpdatedDateTime = DateTime.Now
            },
            Student = student,
            Section = section,
            AccountHead = branchMediumAccountHead
          };
          _repository.Insert(studentWaiver);
        }
      }

      _dbContext.SaveChanges();
    }
  }
}