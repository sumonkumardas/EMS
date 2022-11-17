using System.Collections.Generic;
using System.Threading.Tasks;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class StudentResultSheetPreparedMessageSender : IStudentResultSheetPreparedMessageSender
  {
    private readonly IMultipleStudentResultSheetPreparedMessageSender _multipleStudentResultSheetPreparedMessageSender;

    public StudentResultSheetPreparedMessageSender(
      IMultipleStudentResultSheetPreparedMessageSender multipleStudentResultSheetPreparedMessageSender)
    {
      _multipleStudentResultSheetPreparedMessageSender = multipleStudentResultSheetPreparedMessageSender;
    }

    public async Task SendStudentResultSheetPreparedMessage(long studentSectionId, long termId)
    {
      await _multipleStudentResultSheetPreparedMessageSender.SendStudentResultSheetPreparedMessage(
        new List<long> {studentSectionId}, termId);
    }
  }
}