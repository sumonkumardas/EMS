using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Repositories;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class ShowEmployeeGradeListUseCase : IShowEmployeeGradeListUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowEmployeeGradeListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Grade, GradeMessageModel>());
    }

    public List<GradeMessageModel> ShowEmployeeGradeList(ShowEmployeeGradeListRequestMessage request)
    {
      
      var dbEmployeeGradeList =  _repository.Table<Grade>().ToList();
      var messageModelEmployeeGradeList = new List<GradeMessageModel>();
      
      var mapper = _automapperConfig.CreateMapper();
      List <GradeMessageModel> list = mapper.Map(dbEmployeeGradeList, messageModelEmployeeGradeList);
      return list;
      
    }
  }
}