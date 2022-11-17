using FluentValidation;
using SinePulse.EMS.Messages.WaiverMessages;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class GetStudentWaiversRequestMessageValidator : AbstractValidator<GetStudentWaiversRequestMessage>
  {
    public GetStudentWaiversRequestMessageValidator()
    {
    }
  }
}