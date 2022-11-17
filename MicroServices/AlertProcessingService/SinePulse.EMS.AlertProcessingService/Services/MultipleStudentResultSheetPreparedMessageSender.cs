using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.SmsGenerators;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class MultipleStudentResultSheetPreparedMessageSender : IMultipleStudentResultSheetPreparedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IContactPersonProvider _contactPersonProvider;
    private readonly IMessageSender _messageSender;
    private readonly IResultSheetPreparedNotificationSmsGenerator _resultSheetPreparedNotificationSmsGenerator;

    public MultipleStudentResultSheetPreparedMessageSender(IRepository repository,
      IContactPersonProvider contactPersonProvider, IMessageSender messageSender,
      IResultSheetPreparedNotificationSmsGenerator resultSheetPreparedNotificationSmsGenerator)
    {
      _repository = repository;
      _contactPersonProvider = contactPersonProvider;
      _messageSender = messageSender;
      _resultSheetPreparedNotificationSmsGenerator = resultSheetPreparedNotificationSmsGenerator;
    }

    public async Task SendStudentResultSheetPreparedMessage(ICollection<long> studentSectionIds, long termId)
    {
      var term = await _repository.GetByIdAsync<ExamTerm, Institute>(termId,
        x => x.Session.BranchMedium.Branch.Institute);
      foreach (var studentSectionId in studentSectionIds)
      {
        var studentSection = await _repository.GetByIdAsync<StudentSection, Student>(studentSectionId, x => x.Student);
        var resultSheet = await _repository
          .Filter<ResultSheet>(x => x.StudentSectionId == studentSectionId && x.TermId == termId).FirstAsync();
        var student = studentSection.Student;
        foreach (var contactPerson in await _contactPersonProvider.GetContactPersons(student.Id))
        {
          var smsMessage = await _resultSheetPreparedNotificationSmsGenerator.GenerateResultPreparedNotificationSms(
            contactPerson, student, resultSheet.Gpa, term.TermName, term.Session.SessionName,
            term.Session.BranchMedium.Branch.Institute.OrganisationName);
          await _messageSender.Send(smsMessage, MicroServiceAddresses.SmsSendingService);
        }
      }
    }
  }
}