using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ShowBranchMediumListRequestMessageValidator : AbstractValidator<ShowBranchMediumListRequestMessage>
  {

    public ShowBranchMediumListRequestMessageValidator()
    {
    }
    
  }
}