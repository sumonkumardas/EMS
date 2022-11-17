using System.Collections.Generic;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.UseCases.Mediums
{
  public interface IShowMediumListUseCase
  {
    IEnumerable<MediumMessageModel> ShowMediumList(ShowMediumListRequestMessage message);
  }
}