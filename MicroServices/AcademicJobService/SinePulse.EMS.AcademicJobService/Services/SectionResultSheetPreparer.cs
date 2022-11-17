using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.AcademicJobMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public class SectionResultSheetPreparer : ISectionResultSheetPreparer
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public SectionResultSheetPreparer(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task PrepareSectionResultSheet(long sectionId, long termId)
    {
      await GetAllStudentSectionId(sectionId).ForEachAsync(async studentSectionId =>
      {
        var message = new PrepareResultSheetMessage
        {
          StudentSectionId = studentSectionId,
          TermId = termId
        };
        await _messageSender.Send(message, MicroServiceAddresses.AcademicJobService);
      });
    }

    private IAsyncEnumerable<long> GetAllStudentSectionId(long sectionId)
    {
      var studentSections = _repository.Filter<StudentSection>(x => x.Section.Id == sectionId).AsAsyncEnumerable();
      return studentSections.Select(x => x.Id);
    }
  }
}