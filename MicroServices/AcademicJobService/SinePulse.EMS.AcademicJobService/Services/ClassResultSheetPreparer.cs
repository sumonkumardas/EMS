using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.AcademicJobMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.AcademicJobService.Services
{
  public class ClassResultSheetPreparer : IClassResultSheetPreparer
  {
    private readonly IRepository _repository;
    private readonly IMessageSender _messageSender;

    public ClassResultSheetPreparer(IRepository repository, IMessageSender messageSender)
    {
      _repository = repository;
      _messageSender = messageSender;
    }

    public async Task PrepareClassResultSheet(long classId, long termId)
    {
      await GetAllSectionId(classId).ForEachAsync(async sectionId =>
      {
        var message = new PrepareSectionResultSheetMessage
        {
          SectionId = sectionId,
          TermId = termId
        };
        await _messageSender.Send(message, MicroServiceAddresses.AcademicJobService);
      });
    }

    private IAsyncEnumerable<long> GetAllSectionId(long classId)
    {
      var sections = _repository.Filter<Section>(x => x.Class.Id == classId).AsAsyncEnumerable();
      return sections.Select(x => x.Id);
    }
  }
}