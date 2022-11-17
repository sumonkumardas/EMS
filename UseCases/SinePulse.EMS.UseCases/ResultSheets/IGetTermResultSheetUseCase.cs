using SinePulse.EMS.Messages.ResultSheetMessages;

namespace SinePulse.EMS.UseCases.ResultSheets
{
  public interface IGetTermResultSheetUseCase
  {
    GetTermResultSheetResponseMessage.ExamTermResult GetTermResultSheet(GetTermResultSheetRequestMessage message);
  }
}