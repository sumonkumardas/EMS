using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Accounts;

namespace SinePulse.EMS.Messages.AutoPostingConfigurationMessages
{
  public class ShowAutoPostingConfigurationListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<AutoPostingConfigurationMessageModel> AutoPostingConfigurations { get; }

    public ShowAutoPostingConfigurationListResponseMessage(ValidationResult validationResult,
      IEnumerable<AutoPostingConfigurationMessageModel> autoPostingConfigurations)
    {
      ValidationResult = validationResult;
      AutoPostingConfigurations = autoPostingConfigurations;
    }
  }
}