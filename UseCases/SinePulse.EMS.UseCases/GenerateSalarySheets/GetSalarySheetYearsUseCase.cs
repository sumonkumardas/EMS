using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.GenerateSalarySheets
{
  public class GetSalarySheetYearsUseCase : IGetSalarySheetYearsUseCase
  {
    private readonly IRepository _repository;

    public GetSalarySheetYearsUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<int> GetSalarySheetYears(GetSalarySheetYearsRequestMessage message)
    {
      var branchMediumCreateDate = _repository
        .GetByIdWithInclude<BranchMedium>(message.BranchMediumId, new[] {nameof(AuditFields)})
        .AuditFields
        .InsertedDateTime;
      var years = new List<int>();
      
      for (var date = branchMediumCreateDate; date <= DateTime.Now; date = date.AddYears(1))
        years.Add(date.Year);
      return years;
    }
  }
}