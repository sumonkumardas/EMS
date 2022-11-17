using FluentValidation;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public class
    ShowBranchMediumSectionListRequestMessageValidator : AbstractValidator<ShowBranchMediumSectionListRequestMessage>
  {
    public ShowBranchMediumSectionListRequestMessageValidator()
    {
    }
  }
}