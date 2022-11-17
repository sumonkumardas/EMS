using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class ClassTestStartedMessageSender : IClassTestStartedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly IClassTestPriorNotificationSmsGenerator _classTestPriorNotificationSmsGenerator;

    public ClassTestStartedMessageSender(IRepository repository, IContactPersonProvider contactPersonProvider,
      IClassTestPriorNotificationSmsGenerator classTestPriorNotificationSmsGenerator)
    {
      _repository = repository;
      _contactPersonProvider = contactPersonProvider;
      _classTestPriorNotificationSmsGenerator = classTestPriorNotificationSmsGenerator;
    }

    public async Task SendClassTestStartedMessage(long classTestId)
    {
      var classTest = await _repository.GetByIdAsync<ClassTest, Subject, Institute>(
        classTestId,
        x => x.ExamConfiguration.Subject,
        x => x.ExamConfiguration.Term.Session.BranchMedium.Branch.Institute);
      var branchMediumId = classTest.ExamConfiguration.Term.Session.BranchMediumId;

      var students = await _repository.Filter<Student>(x => x.BranchMedium.Id == branchMediumId).ToListAsync();
      foreach (var student in students)
      {
        foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
        {
          await _classTestPriorNotificationSmsGenerator.GenerateClassTestNotificationSms(contactPerson, student,
            classTest.ExamConfiguration.Term.TermName, classTest.ExamConfiguration.Term.Session.SessionName,
            classTest.ExamConfiguration.Subject.SubjectName, classTest.TestName, classTest.StartTimestamp,
            classTest.ExamConfiguration.Term.Session.BranchMedium.Branch.Institute.OrganisationName);
        }
      }
    }
  }
}