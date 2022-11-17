using FluentValidation;
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonImageRequestMessageValidator : AbstractValidator<AddStudentContactPersonImageRequestMessage>
  {
    public AddStudentContactPersonImageRequestMessageValidator()
    {
    }
  }
}