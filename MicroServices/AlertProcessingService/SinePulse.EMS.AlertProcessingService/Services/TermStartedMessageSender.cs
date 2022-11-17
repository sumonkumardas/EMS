using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class TermStartedMessageSender : ITermStartedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly ITermPriorNotificationSmsGenerator _termPriorNotificationSmsGenerator;

    public TermStartedMessageSender(IRepository repository, IContactPersonProvider contactPersonProvider,
      ITermPriorNotificationSmsGenerator termPriorNotificationSmsGenerator)
    {
      _repository = repository;
      _contactPersonProvider = contactPersonProvider;
      _termPriorNotificationSmsGenerator = termPriorNotificationSmsGenerator;
    }

    public async Task SendTermStartedMessage(long termId)
    {
      var term = await _repository.GetByIdAsync<ExamTerm, Session, Institute>(
        termId,
        x => x.Session,
        x => x.Session.BranchMedium.Branch.Institute);
      var branchMediumId = term.Session.BranchMediumId;

      var students = await _repository.Filter<Student>(x => x.BranchMedium.Id == branchMediumId).ToListAsync();
      foreach (var student in students)
      {
        foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
        {
          await _termPriorNotificationSmsGenerator.GenerateTermPriorNotificationSms(contactPerson, student,
            term.TermName, term.Session.SessionName, term.StartDate,
            term.Session.BranchMedium.Branch.Institute.OrganisationName);
        }
      }
    }
  }
}