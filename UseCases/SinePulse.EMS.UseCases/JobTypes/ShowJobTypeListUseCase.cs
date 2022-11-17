using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Persistence.Repositories;
using AutoMapper;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeListUseCase : IShowJobTypeListUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowJobTypeListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<JobType, JobTypeMessageModel>());
    }

    public List<JobTypeMessageModel> ShowJobTypeList(ShowJobTypeListRequestMessage request)
    {
      var dbJobTypeList =  _repository.Table<JobType>().ToList();
      var messageModelJobTypeList = new List<JobTypeMessageModel>();
      
      var mapper = _automapperConfig.CreateMapper();
      List <JobTypeMessageModel> list = mapper.Map(dbJobTypeList, messageModelJobTypeList);
      return list;
      
    }
  }
}