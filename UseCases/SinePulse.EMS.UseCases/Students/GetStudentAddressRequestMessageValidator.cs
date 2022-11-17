using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;

namespace SinePulse.EMS.UseCases.Students
{
  public class GetStudentAddressRequestMessageValidator : AbstractValidator<GetStudentAddressRequestMessage>
  {
    public GetStudentAddressRequestMessageValidator()
    {
    }
  }
}