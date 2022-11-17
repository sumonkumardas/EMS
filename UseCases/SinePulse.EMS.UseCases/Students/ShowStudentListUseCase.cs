using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentListUseCase : IShowStudentListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowStudentListUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentMessageModel>());
    }

    public IEnumerable<StudentMessageModel> ShowStudentsList(ShowStudentListRequestMessage message)
    {
      var students = _repository.Table<Student>(new []{nameof(Student.BranchMedium)}).Where(x=>x.BranchMedium.Id == message.BranchMediumId).ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(students, new List<StudentMessageModel>());
    }
  }
}