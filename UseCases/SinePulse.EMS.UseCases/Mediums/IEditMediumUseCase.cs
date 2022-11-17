using SinePulse.EMS.Messages.MediumMessages;

namespace SinePulse.EMS.UseCases.Mediums
{
  public interface IEditMediumUseCase
  {
    void UpdateMedium(EditMediumRequestMessage message);
  }
}