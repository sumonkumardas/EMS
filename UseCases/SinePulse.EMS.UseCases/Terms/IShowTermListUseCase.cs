using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public interface IShowTermListUseCase
  {
    ShowTermListResponseMessage ShowTermList(ShowTermListRequestMessage message);
  }
}