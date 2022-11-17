using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Persistence.Repositories;
using AutoMapper;
using SinePulse.EMS.Domain.Leaves;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Leaves;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineUseCase : IShowClassRoutineUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowClassRoutineUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Domain.Employees.Employee, EmployeeMessageModel>()
          .ForMember(x => x.ObjectId, opt => opt.Ignore())
          .ForMember(x => x.AssociatedWith, opt => opt.Ignore());
        cfg.CreateMap<ClassRoutine, ClassRoutineMessageModel>();
        ;
      });

    }

    public ClassRoutineMessageModel ShowClassRoutine(ShowClassRoutineRequestMessage request)
    {
      var includes = new[] {nameof(ClassRoutine.Room),nameof(ClassRoutine.Section),nameof(ClassRoutine.Teacher),nameof(ClassRoutine.Subject)};
      var dbClassRoutine = _repository.GetByIdWithInclude<ClassRoutine>(request.ClassRoutineId,includes);
      var messageModelClassRoutine = new ClassRoutineMessageModel();
      var mapper = _automapperConfig.CreateMapper();
      return mapper.Map<ClassRoutineMessageModel>(dbClassRoutine);
    }
  }
}