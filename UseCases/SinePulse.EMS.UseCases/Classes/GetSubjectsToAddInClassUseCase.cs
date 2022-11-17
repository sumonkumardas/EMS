using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetSubjectsToAddInClassUseCase : IGetSubjectsToAddInClassUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetSubjectsToAddInClassUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Subject, SubjectMessageModel>());
    }

    public IEnumerable<SubjectMessageModel> GetSubjects(GetSubjectsToAddInClassRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var addedSubjects = _repository.Table<ClassSubject>()
        .Where(c => c.Class.Id == message.ClassId)
        .Select(x => x.Subject).ToList();
      var subjects = _repository.Table<Subject>().ToList();
      foreach (var addedSubject in addedSubjects)
      {
        subjects.Remove(addedSubject);
      }
      return mapper.Map(subjects, new List<SubjectMessageModel>());
    }
  }
}