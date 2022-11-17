using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeUseCase : IShowResultGradeUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _automapperConfig;

    public ShowResultGradeUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ResultGrade, ResultGradeMessageModel>());
    }

    public ShowResultGradeResponseMessage ShowResultGrade(ShowResultGradeRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ResultGrade.BranchMedium),
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Branch)}",
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Medium)}",
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Shift)}"
      };
      var dbResultGrade = _repository.GetByIdWithInclude<ResultGrade>(message.ResultGradeId, includes);
      var messageModelResultGrade = new ResultGradeMessageModel();

      var mapper = _automapperConfig.CreateMapper();
      ResultGradeMessageModel model = mapper.Map(dbResultGrade, messageModelResultGrade);
      return new ShowResultGradeResponseMessage(model);
    }
    
  }
}