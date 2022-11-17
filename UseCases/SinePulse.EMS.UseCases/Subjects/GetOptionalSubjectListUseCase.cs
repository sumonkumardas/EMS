using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class GetOptionalSubjectListUseCase : IGetOptionalSubjectListUseCase
  {
    private readonly IRepository _repository;

    public GetOptionalSubjectListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<GetOptionalSubjectListResponseMessage.Subject> GetOptionalSubjectList(
      GetOptionalSubjectListRequestMessage message)
    {
      var optionalSubjects = _repository.Table<ClassSubject>()
        .Include(nameof(Subject))
        .Where(cs => cs.Class.Id == message.ClassId &&
                     cs.Group == message.Group &&
                     cs.IsOptional)
        .Select(cs => cs.Subject)
        .ToList();

      return ToRequestSubjects(optionalSubjects);
    }

    private IEnumerable<GetOptionalSubjectListResponseMessage.Subject> ToRequestSubjects(List<Subject> subjects)
    {
      var requestSubjects = new List<GetOptionalSubjectListResponseMessage.Subject>();
      foreach (var subject in subjects)
      {
        requestSubjects.Add(new GetOptionalSubjectListResponseMessage.Subject
        {
          SubjectId = subject.Id,
          SubjectName = subject.SubjectName
        });
      }

      return requestSubjects;
    }
  }
}