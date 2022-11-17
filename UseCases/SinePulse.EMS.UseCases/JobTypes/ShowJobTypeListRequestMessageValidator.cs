using FluentValidation;
using SinePulse.EMS.Messages.JobTypeMessages;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeListRequestMessageValidator : AbstractValidator<ShowJobTypeListRequestMessage>
  {

    public ShowJobTypeListRequestMessageValidator()
    {
    }
    
  }
}