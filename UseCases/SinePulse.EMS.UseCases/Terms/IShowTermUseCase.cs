using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public interface IShowTermUseCase
  {
    ShowTermResponseMessage ShowTerm(ShowTermRequestMessage message);
  }
}