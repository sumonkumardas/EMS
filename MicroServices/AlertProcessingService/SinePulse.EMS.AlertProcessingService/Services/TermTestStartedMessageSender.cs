using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class TermTestStartedMessageSender : ITermTestStartedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly ITermTestPriorNotificationSmsGenerator _termTestPriorNotificationSmsGenerator;

    public TermTestStartedMessageSender(IRepository repository, IContactPersonProvider contactPersonProvider,
      ITermTestPriorNotificationSmsGenerator termTestPriorNotificationSmsGenerator)
    {
      _repository = repository;
      _contactPersonProvider = contactPersonProvider;
      _termTestPriorNotificationSmsGenerator = termTestPriorNotificationSmsGenerator;
    }

    public async Task SendTermTestStartedMessage(long termTestId)
    {
      var termTest = await _repository.GetByIdAsync<TermTest, Subject, Institute>(
        termTestId,
        x => x.ExamConfiguration.Subject,
        x => x.ExamConfiguration.Term.Session.BranchMedium.Branch.Institute);
      var branchMediumId = termTest.ExamConfiguration.Term.Session.BranchMediumId;

      var students = await _repository.Filter<Student>(x => x.BranchMedium.Id == branchMediumId).ToListAsync();
      foreach (var student in students)
      {
        foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
        {
          await _termTestPriorNotificationSmsGenerator.GenerateTermTestNotificationSms(contactPerson, student,
            termTest.ExamConfiguration.Term.TermName, termTest.ExamConfiguration.Term.Session.SessionName,
            termTest.ExamConfiguration.Subject.SubjectName, termTest.StartTimestamp,
            termTest.ExamConfiguration.Term.Session.BranchMedium.Branch.Institute.OrganisationName);
        }
      }
    }
  }
}