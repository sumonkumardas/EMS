using FluentValidation;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class ShowStudentSectionListRequestMessageValidator : AbstractValidator<ShowStudentSectionListRequestMessage>
  {
    public ShowStudentSectionListRequestMessageValidator()
    {
    }
  }
}