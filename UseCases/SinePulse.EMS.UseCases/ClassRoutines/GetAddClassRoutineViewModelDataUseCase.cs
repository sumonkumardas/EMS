using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class GetAddClassRoutineViewModelDataUseCase : IGetAddClassRoutineViewModelDataUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _roomMapper;
    private readonly MapperConfiguration _subjectsMapper;
    private readonly MapperConfiguration _teachersMapper;

    public GetAddClassRoutineViewModelDataUseCase(IRepository repository)
    {
      _repository = repository;
      _roomMapper = new MapperConfiguration(c => c.CreateMap<Room, RoomMessageModel>());
      _subjectsMapper = new MapperConfiguration(c => c.CreateMap<Subject, SubjectMessageModel>());
      _teachersMapper = new MapperConfiguration(c => c.CreateMap<Domain.Employees.Employee, EmployeeMessageModel>());
    }

    public void GetAddClassRoutineViewModelData(GetAddClassRoutineViewModelDataRequestMessage message)
    {
      var roomMapper = _roomMapper.CreateMapper();                                
      var subjectsMapper = _subjectsMapper.CreateMapper();
      var teachersMapper = _teachersMapper.CreateMapper();
      
      var section = _repository.Table<Section>()
        .Include(nameof(Class))
        .Include(nameof(BranchMedium) +"."+ nameof(BranchMedium.Branch))
        .Include(nameof(BranchMedium) + "." + nameof(BranchMedium.Branch) + "." + nameof(Institute))
        .FirstOrDefault(s => s.Id == message.SectionId);
      
      var teachers = _repository.Table<Domain.Employees.Employee>().Where(w=>w.Institute.Id == section.BranchMedium.Branch.Institute.Id).ToList();

      var subjects = _repository.Table<ClassSubject>()
        .Include(nameof(Class) + "." + nameof(Subject))
        .Where(gs => gs.Class.Id == section.Class.Id && gs.Group == section.Group)
        .Select(s => s.Subject);

      var rooms = _repository.Table<BranchMedium>()
        .Include(nameof(Branch)+"."+ nameof(Branch.Rooms))
        .First(b => b.Id == section.BranchMedium.Id)
        .Branch.Rooms;

      message.Subjects = subjectsMapper.Map(subjects, new List<SubjectMessageModel>());
      message.Rooms = roomMapper.Map(rooms, new List<RoomMessageModel>());
      message.Teachers = teachersMapper.Map(teachers, new List<EmployeeMessageModel>());
    }
  }
}