using SinePulse.EMS.Messages.AutoPostingConfigurationMessages;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.UseCases.AutoPostingConfigurations
{
  public interface IShowAutoPostingConfigurationListUseCase
  {
    List<AutoPostingConfigurationMessageModel> ShowAutoPostingConfigurationList(ShowAutoPostingConfigurationListRequestMessage message);
  }
}