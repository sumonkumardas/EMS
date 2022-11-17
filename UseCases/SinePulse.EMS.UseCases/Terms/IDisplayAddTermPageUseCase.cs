using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public interface IDisplayAddTermPageUseCase
  {
    DisplayAddTermPageResponseMessage DisplayAddTermPage(DisplayAddTermPageRequestMessage requestMessage);
  }
}