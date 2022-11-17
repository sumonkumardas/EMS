using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public interface IEditAccountHeadUseCase
  {
    void UpdateAccountHead(EditAccountHeadRequestMessage message);
  }
}