using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Mediums
{
  public interface IShowMediumUseCase
  {
    MediumMessageModel ShowMedium(ShowMediumRequestMessage message);
  }
}