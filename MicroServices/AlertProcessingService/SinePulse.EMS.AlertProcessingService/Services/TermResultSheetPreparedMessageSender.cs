using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class TermResultSheetPreparedMessageSender : ITermResultSheetPreparedMessageSender
  {
    private readonly IRepository _repository;
    private readonly IMultipleStudentResultSheetPreparedMessageSender _multipleStudentResultSheetPreparedMessageSender;

    public TermResultSheetPreparedMessageSender(IRepository repository,
      IMultipleStudentResultSheetPreparedMessageSender multipleStudentResultSheetPreparedMessageSender)
    {
      _repository = repository;
      _multipleStudentResultSheetPreparedMessageSender = multipleStudentResultSheetPreparedMessageSender;
    }

    public async Task SendTermResultSheetPreparedMessage(long termId)
    {
      var studentIds = new List<long>();
      var classes = await _repository.Table<Class>().ToListAsync();
      foreach (var @class in classes)
      {
        var sections = await _repository.Filter<Section>(x => x.Class.Id == @class.Id).ToListAsync();
        foreach (var section in sections)
        {
          var studentSections = await _repository.Filter<StudentSection>(x => x.Section.Id == section.Id).ToListAsync();
          studentIds.AddRange(studentSections.Select(x => x.Id));
        }
      }

      await _multipleStudentResultSheetPreparedMessageSender.SendStudentResultSheetPreparedMessage(studentIds, termId);
    }
  }
}