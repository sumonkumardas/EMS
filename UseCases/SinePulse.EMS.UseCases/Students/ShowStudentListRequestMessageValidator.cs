using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentListRequestMessageValidator : AbstractValidator<ShowStudentListRequestMessage>
  {
    public ShowStudentListRequestMessageValidator()
    {
    }
  }
}