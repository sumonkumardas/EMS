using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteListUseCase : IShowInstituteListUseCase
  {
    private readonly IRepository _repository;

    public ShowInstituteListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<Institute> ShowInstituteList(ShowInstituteListRequestMessage request)
    {

      return _repository.Table<Institute>().OrderByDescending(o=>o.AuditFields.InsertedDateTime).Take(50).ToList(); 
    }
  }
}