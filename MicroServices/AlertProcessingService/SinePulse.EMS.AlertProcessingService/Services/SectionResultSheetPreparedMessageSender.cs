using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class SectionResultSheetPreparedMessageSender : ISectionResultSheetPreparedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IMultipleStudentResultSheetPreparedMessageSender _multipleStudentResultSheetPreparedMessageSender;

    public SectionResultSheetPreparedMessageSender(IRepository repository,
      IMultipleStudentResultSheetPreparedMessageSender multipleStudentResultSheetPreparedMessageSender)
    {
      _repository = repository;
      _multipleStudentResultSheetPreparedMessageSender = multipleStudentResultSheetPreparedMessageSender;
    }

    public async Task SendSectionResultSheetPreparedMessage(long sectionId, long termId)
    {
      var studentSections = await _repository.Filter<StudentSection>(x => x.Section.Id == sectionId).ToListAsync();
      var studentIds = studentSections.Select(x => x.Id).ToList();
      await _multipleStudentResultSheetPreparedMessageSender.SendStudentResultSheetPreparedMessage(studentIds, termId);
    }
  }
}