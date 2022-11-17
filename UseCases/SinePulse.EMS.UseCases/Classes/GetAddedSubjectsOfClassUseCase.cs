using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class GetAddedSubjectsOfClassUseCase : IGetAddedSubjectsOfClassUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetAddedSubjectsOfClassUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Subject, SubjectMessageModel>());
    }

    public IEnumerable<GetAddedSubjectsOfClassResponseMessage.SubjectWithClass> GetSubjects(GetAddedSubjectsOfClassRequestMessage message)
    {
      var subjectInClasses = _repository.Table<ClassSubject>()
        .Include(nameof(Class))
        .Include(nameof(Subject))
        .Where(s => s.Class.Id == message.ClassId)
        .ToList();
      
      var subjectWithClass = new List<GetAddedSubjectsOfClassResponseMessage.SubjectWithClass>();

      foreach (var subjectInClass in subjectInClasses)
      {
        var sb = new GetAddedSubjectsOfClassResponseMessage.SubjectWithClass();

        sb.SubjectName = subjectInClass.Subject.SubjectName;
        sb.SubjectCode = subjectInClass.Subject.SubjectCode;
        sb.IsOptional = subjectInClass.IsOptional;
        subjectWithClass.Add(sb);
      }
      return subjectWithClass;
    }
  }
}