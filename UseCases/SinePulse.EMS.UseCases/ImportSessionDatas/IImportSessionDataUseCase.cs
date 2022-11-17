using SinePulse.EMS.Messages.ImportSessionDataMessages;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public interface IImportSessionDataUseCase
  {
    ImportSessionDataResponseMessage ImportSessionData(ImportSessionDataRequestMessage requestMessage);
  }
}