using SinePulse.EMS.Messages.ImportSessionDataMessages;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public interface IDisplayImportSessionDataPageUseCase
  {
    DisplayImportSessionDataPageResponseMessage DisplayImportSessionDataPage(DisplayImportSessionDataPageRequestMessage requestMessage);
  }
}