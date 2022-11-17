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
  public class ShowEmployeeGradeUseCase : IShowEmployeeGradeUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowEmployeeGradeUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Grade, GradeMessageModel>());
    }

    public GradeMessageModel ShowEmployeeGrade(ShowEmployeeGradeRequestMessage request)
    {
      
      var dbGrade =  _repository.GetById<Grade>(request.GradeId);
      var messageModelGrade = new GradeMessageModel();
      
      var mapper = _automapperConfig.CreateMapper();
      GradeMessageModel model = mapper.Map(dbGrade, messageModelGrade);
      return model;
      
    }
  }
}