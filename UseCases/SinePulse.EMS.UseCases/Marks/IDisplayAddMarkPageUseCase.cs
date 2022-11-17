using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IDisplayAddMarkPageUseCase
  {
    DisplayAddMarkPageResponseMessage DisplayAddMarkPage(DisplayAddMarkPageRequestMessage requestMessage);
  }
}