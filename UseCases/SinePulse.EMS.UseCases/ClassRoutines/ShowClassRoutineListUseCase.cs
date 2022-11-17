using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Repositories;
using AutoMapper;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineListUseCase : IShowClassRoutineListUseCase
  {
    private readonly IRepository _repository;
    private  MapperConfiguration _automapperConfig;

    public ShowClassRoutineListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClassRoutine, ClassRoutineMessageModel>());
    }

    public List<ClassRoutineMessageModel> ShowClassRoutineList(ShowClassRoutineListRequestMessage request)
    {
      var includes = new string[] { nameof(Teacher)};
      var dbClassRoutineList =  _repository.Table<ClassRoutine>(includes).Where(x=>x.Section.Id == request.SectionId).ToList();
      var messageModelClassRoutineList = new List<ClassRoutineMessageModel>();
      var mapper = _automapperConfig.CreateMapper();
      List <ClassRoutineMessageModel> list = mapper.Map(dbClassRoutineList, messageModelClassRoutineList);
      return list;
      
    }
  }
}