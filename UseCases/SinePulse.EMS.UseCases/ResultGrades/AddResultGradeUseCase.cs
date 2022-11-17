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
  public class AddResultGradeUseCase : IAddResultGradeUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddResultGradeUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public AddResultGradeResponseMessage AddResultGrade(AddResultGradeRequestMessage requestMessage)
    {
      var branchMedium = _repository.GetById<BranchMedium>(requestMessage.BranchMediumId);
      var resultGrade = new ResultGrade
      {
        GradeLetter = requestMessage.GradeLetter,
        GradePoint = requestMessage.GradePoint,
        StartMark = requestMessage.StartMark,
        EndMark = requestMessage.EndMark,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        },

        BranchMedium = branchMedium
      };

      _repository.Insert(resultGrade);
      
      _dbContext.SaveChanges();

      return new AddResultGradeResponseMessage(resultGrade.Id);
    }
  }
}