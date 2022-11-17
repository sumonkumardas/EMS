using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public interface IImportCOAFromMasterUseCase
  {
    void ImportCOAFromMaster(ImportCOAFromMasterRequestMessage message);
  }
}