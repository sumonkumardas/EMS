using FluentValidation;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteListRequestMessageValidator : AbstractValidator<ShowInstituteListRequestMessage>
  {

    public ShowInstituteListRequestMessageValidator()
    {
    }
    
  }
}