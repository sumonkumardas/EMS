using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class TotalRegisteredStudentProvider : ITotalRegisteredStudentProvider
  {
    private readonly IRepository _repository;

    public TotalRegisteredStudentProvider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<int> GetTotalRegisteredStudentOfTest(long testId)
    {
      var test = _repository.GetById<TermTest, Session, Class, Subject>(testId,
        x => x.ExamConfiguration.Term.Session,
        x => x.ExamConfiguration.Class,
        x => x.ExamConfiguration.Subject);

      var session = test.ExamConfiguration.Term.Session;
      var @class = test.ExamConfiguration.Class;
      var subject = test.ExamConfiguration.Subject;

      var totalStudents = 0;
      foreach (var classSubject in await GetAllClassSubjects(@class, subject))
        totalStudents += await GetTotalRegisteredStudentOfClassSubject(session, classSubject);

      return totalStudents;
    }

    private async Task<ICollection<ClassSubject>> GetAllClassSubjects(Class @class, Subject subject)
    {
      return await _repository.Filter<ClassSubject, Class, Subject>(
          x => x.Class.Id == @class.Id && x.Subject.Id == subject.Id,
          x => x.Class,
          x => x.Subject)
        .ToListAsync();
    }

    private async Task<int> GetTotalRegisteredStudentOfClassSubject(Session session, ClassSubject classSubject)
    {
      return await (classSubject.IsOptional
        ? GetTotalRegisteredOptionalStudentOfClassSubject(session, classSubject)
        : GetTotalRegisteredCompulsoryStudentOfClassSubject(session, classSubject));
    }

    private async Task<int> GetTotalRegisteredCompulsoryStudentOfClassSubject(Session session,
      ClassSubject classSubject)
    {
      var totalStudents = 0;
      foreach (var section in await GetSections(session, classSubject))
        totalStudents += await _repository.Filter<StudentSection>(x => x.Section.Id == section.Id).CountAsync();

      return totalStudents;
    }

    private async Task<int> GetTotalRegisteredOptionalStudentOfClassSubject(Session session, ClassSubject classSubject)
    {
      return await _repository.Filter<SubjectChoice>(
          x => x.Session.Id == session.Id &&
               x.Subject.Id == classSubject.Id)
        .CountAsync();
    }

    private async Task<ICollection<Section>> GetSections(Session session, ClassSubject classSubject)
    {
      return await _repository.Filter<Section>(x =>
        x.Session.Id == session.Id &&
        x.Group == classSubject.Group &&
        x.Class.Id == classSubject.Class.Id).ToListAsync();
    }
  }
}