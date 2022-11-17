using FluentValidation;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAcademicAccountHeadListRequestMessageValidator : AbstractValidator<ShowAcademicAccountHeadListRequestMessage>
  {
    public ShowAcademicAccountHeadListRequestMessageValidator()
    {
    }
  }
}