using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddOrChangeStudentImageRequestMessageValidator : AbstractValidator<AddOrChangeStudentImageRequestMessage>
  {
    public AddOrChangeStudentImageRequestMessageValidator()
    {
    }
  }
}