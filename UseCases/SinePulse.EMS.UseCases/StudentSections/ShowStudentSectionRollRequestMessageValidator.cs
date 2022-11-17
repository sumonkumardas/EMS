using FluentValidation;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class ShowStudentSectionRollRequestMessageValidator : AbstractValidator<ShowStudentSectionRollRequestMessage>
  {
    public ShowStudentSectionRollRequestMessageValidator()
    {
    }
  }
}