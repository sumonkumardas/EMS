using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeListUseCase : IShowResultGradeListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowResultGradeListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ResultGrade, ResultGradeMessageModel>());
    }

    public ShowResultGradeListResponseMessage ShowResultGradeList(ShowResultGradeListRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ResultGrade.BranchMedium),
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Branch)}",
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Medium)}",
        $"{nameof(ResultGrade.BranchMedium)}.{nameof(BranchMedium.Shift)}"
      };
      var dbResultGradeList = _repository.Table<ResultGrade>(includes).Where(x=>x.BranchMedium.Id == message.BranchMediumId).ToList();
      var mapper = _automapperConfig.CreateMapper();
      return new ShowResultGradeListResponseMessage(mapper.Map<List<ResultGradeMessageModel>>(dbResultGradeList));
    }
    
  }
}