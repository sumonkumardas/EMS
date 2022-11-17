using FluentValidation;
using SinePulse.EMS.Messages.JobTypeMessages;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeRequestMessageValidator : AbstractValidator<ShowJobTypeRequestMessage>
  {
    public ShowJobTypeRequestMessageValidator()
    {
    }
  }
}