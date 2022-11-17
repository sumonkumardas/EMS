using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class EditResultGradeUseCase : IEditResultGradeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditResultGradeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public EditResultGradeResponseMessage EditResultGrade(EditResultGradeRequestMessage requestMessage)
    {
      var branchMedium = _repository.GetById<BranchMedium>(requestMessage.BranchMediumId);
      var resultGrade = _repository.GetById<ResultGrade>(requestMessage.Id);
      resultGrade.GradeLetter = requestMessage.GradeLetter;
      resultGrade.GradePoint = requestMessage.GradePoint;
      resultGrade.StartMark = requestMessage.StartMark;
      resultGrade.EndMark = requestMessage.EndMark;
      resultGrade.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      resultGrade.AuditFields.LastUpdatedDateTime = DateTime.Now;

      resultGrade.BranchMedium = branchMedium;

      _dbContext.SaveChanges();

      return new EditResultGradeResponseMessage();
    }
  }
}