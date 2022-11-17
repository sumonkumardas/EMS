using FluentValidation;
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class
    GetStudentContactPersonsRequestMessageValidator : AbstractValidator<GetStudentContactPersonsRequestMessage>
  {
    public GetStudentContactPersonsRequestMessageValidator()
    {
    }
  }
}