using FluentValidation;
using SinePulse.EMS.Messages.BranchMessages;

namespace SinePulse.EMS.UseCases.Branches
{
  public class ShowBranchListRequestMessageValidator : AbstractValidator<ShowBranchListRequestMessage>
  {
    public ShowBranchListRequestMessageValidator()
    {
    }
  }
}