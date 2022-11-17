using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationListUseCase : IShowDesignationListUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowDesignationListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Designation, DesignationMessageModel>());
    }

    public List<DesignationMessageModel> ShowDesignationList(ShowDesignationListRequestMessage request)
    {
      List<Designation> dbDesignationList;
      if (request.EmployeeType != 0)
      {
        dbDesignationList =  _repository.Table<Designation>()
          .Where(d => d.EmployeeType == request.EmployeeType)
          .ToList();
      }
      else
      {
        dbDesignationList =  _repository.Table<Designation>().ToList();
      }
      var mapper = _automapperConfig.CreateMapper();
      return mapper.Map(dbDesignationList, new List<DesignationMessageModel>());
      
    }
  }
}