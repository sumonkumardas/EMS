using SinePulse.EMS.Messages.MarkMessages;

namespace SinePulse.EMS.UseCases.Marks
{
  public interface IShowMarkUseCase
  {
    ShowMarkResponseMessage ShowMark(ShowMarkRequestMessage message);
  }
}