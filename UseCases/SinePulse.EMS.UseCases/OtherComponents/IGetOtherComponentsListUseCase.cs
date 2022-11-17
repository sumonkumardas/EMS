using System.Collections.Generic;
using SinePulse.EMS.Messages.OtherComponentMessages;

namespace SinePulse.EMS.UseCases.OtherComponents
{
  public interface IGetOtherComponentsListUseCase
  {
    List<GetOtherComponentsListResponseMessage.OtherComponent> GetOtherComponentsList(
      GetOtherComponentsListRequestMessage message);
  }
}